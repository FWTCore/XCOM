using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.DBClient;
using XCOM.Schema.EDapper.Model;

namespace XCOM.Schema.EDapper.Realization
{
    internal class XMInsertable<T> : IXMInsertable<T> where T : class, new()
    {

        /// <summary>
        /// 数据库连接配置对象
        /// </summary>
        protected DBConnection _dbConfig;

        protected IXMSQLMapper XMDBSql { get; set; }

        internal XMInsertModel<T> Model { get; set; }

        public XMInsertable(DBConnection dbContext)
        {
            this._dbConfig = dbContext;
            this.XMDBSql = new XMSQLMapper(dbContext);
            this.Model = XMRealization.GetInsertEntity<T>(dbContext.DBProviderType);
        }
        /// <summary>
        /// 插入单个实体，返回主键
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual string Insert(T obj)
        {
            return this.XMDBSql.ExecuteScalar<string>(this.Model.InsertSql_AutoId, obj);
        }
        /// <summary>
        /// 异步插入单个实体，返回主键
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task<string> InsertAsync(T obj)
        {
            return await this.XMDBSql.ExecuteScalarAsync<string>(this.Model.InsertSql_AutoId, obj);
        }
        /// <summary>
        /// 批量插入实体列表
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public virtual int Insert(List<T> objs)
        {
            return this.XMDBSql.Execute(this.Model.InsertSql, objs);
        }
        /// <summary>
        /// 异步批量插入实体列表
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(List<T> objs)
        {
            return await this.XMDBSql.ExecuteAsync(this.Model.InsertSql, objs);
        }

        public string DebugSql()
        {
            return this.Model.InsertSql;
        }
    }
}
