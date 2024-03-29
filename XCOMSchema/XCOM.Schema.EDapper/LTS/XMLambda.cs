﻿using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.Utility;

namespace XCOM.Schema.EDapper.LTS
{
    public class XMLambda
    {
        public DynamicParameters Parameters { get; set; }
        /// <summary>
        /// 数据库连接配置对象
        /// </summary>
        private readonly DBConnection _dbConfig;

        public XMLambda(string connectionKey, DynamicParameters parameters = null)
        {
            if (string.IsNullOrWhiteSpace(connectionKey))
            {
                throw new Exception("connectionKey 不能不空!");
            }
            _dbConfig = XMDBConfig.ConfigSetting.DBConnectionList.FirstOrDefault(f => f.Key.Trim().ToUpper() == connectionKey.Trim().ToUpper());
            if (_dbConfig == null)
            {
                throw new Exception(string.Format("ConnectionKey:{0} 无效", connectionKey));
            }
            if (parameters != null)
            {
                Parameters = parameters;
            }
            else
            {
                Parameters = new DynamicParameters();
            }
        }
        public XMLambda(DBConnection dbConfig, DynamicParameters parameters = null)
        {
            _dbConfig = dbConfig ?? throw new Exception("DBConnection 不能为空!");
            if (parameters != null)
            {
                Parameters = parameters;
            }
            else
            {
                Parameters = new DynamicParameters();
            }
        }

        public string VisitXMLambda(Expression exp)
        {
            var visitor = new XMParserVisitor(this._dbConfig.DBProviderType, this.Parameters);
            visitor.Visit(exp);
            this.Parameters = visitor.Parameters;
            return visitor.GetConditionSql();
        }


    }
}
