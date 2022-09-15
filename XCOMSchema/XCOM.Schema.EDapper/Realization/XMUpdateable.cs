using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.DBClient;
using XCOM.Schema.EDapper.Model;

namespace XCOM.Schema.EDapper.Realization
{
    internal class XMUpdateable<T> : IXMUpdateable<T>, IXMUpdate<T> where T : class, new()
    {

        /// <summary>
        /// 数据库连接配置对象
        /// </summary>
        protected DBConnection _dbConfig;

        protected IXMSQLMapper XMDBSql { get; set; }

        internal XMUpdateModel<T> Model { get; set; }

        public XMUpdateable(DBConnection dbContext)
        {
            this._dbConfig = dbContext;
            this.XMDBSql = new XMSQLMapper(dbContext);
            this.Model = XMRealization.GetUpdateEntity<T>(dbContext.DBProviderType);
        }


        public DynamicParameters DebugParam()
        {
            throw new NotImplementedException();
        }

        public string DebugSql()
        {
            throw new NotImplementedException();
        }

        public IXMUpdateable<T> Set(Expression<Func<T, T>> expression)
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public IXMUpdate<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
