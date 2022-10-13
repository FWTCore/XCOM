using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.DBClient;
using XCOM.Schema.EDapper.LTS;
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
            return this.Model.Parameters;
        }

        public string DebugSql()
        {
            return this.Model.UpdateSql;
        }

        public IXMUpdateable<T> Set(Expression<Func<T, T>> expression)
        {
            this.VisitSet(expression);
            return this;
        }

        public int Execute()
        {
            return this.XMDBSql.Execute(this.Model.UpdateSql, this.Model.Parameters);
        }

        public async Task<int> ExecuteAsync()
        {
            return await this.XMDBSql.ExecuteAsync(this.Model.UpdateSql, this.Model.Parameters);
        }

        public IXMUpdate<T> Where(Expression<Func<T, bool>> expression)
        {
            this.VisitCondition(expression);
            return this;
        }

        /// <summary>
        /// 处理lambda查询条件
        /// </summary>
        /// <param name="expression"></param>
        private void VisitCondition(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
            {
                Expression exp = expression.Body;
                var obj = new XMLambda(this._dbConfig, this.Model.Parameters);
                var resultSql = obj.VisitXMLambda(exp);
                this.Model.Parameters = obj.Parameters;
                if (this.Model.Where.Length > 0)
                {
                    this.Model.Where.Append(" and ");
                }
                this.Model.Where.Append(resultSql);
            }
        }

        /// <summary>
        /// 解析更新字段
        /// </summary>
        /// <param name="expression"></param>
        private void VisitSet(Expression<Func<T, T>> expression)
        {
            if (expression != null)
            {
                Expression exp = expression.Body;
                var obj = new XMLambda(this._dbConfig, this.Model.Parameters);
                var resultSql = obj.VisitXMLambda(exp);
                this.Model.Parameters = obj.Parameters;
                if (this.Model.Set.Length > 0)
                {
                    this.Model.Set.Append(" , ");
                }
                this.Model.Set.Append(resultSql);
            }
        }

    }
}
