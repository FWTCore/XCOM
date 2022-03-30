using Dapper;
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
    internal class XMLambda
    {
        public DynamicParameters Parameters { get; set; }
        /// <summary>
        /// 数据库连接配置对象
        /// </summary>
        private readonly DBConnection _dbConfig;

        public XMLambda(string connectionKey)
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
            Parameters = new DynamicParameters();
        }
        public XMLambda(DBConnection dbConfig)
        {
            _dbConfig = dbConfig ?? throw new Exception("DBConnection 不能为空!");
            Parameters = new DynamicParameters();
        }

        public string VisitXMLambda(Expression exp)
        {
            var visitor = new XMParserVisitor(this._dbConfig.DBProviderType);
            visitor.Visit(exp);
            this.Parameters = visitor.Parameters;
            return visitor.GetConditionSql();
        }


    }
}
