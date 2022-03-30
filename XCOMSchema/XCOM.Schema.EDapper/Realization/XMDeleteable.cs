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
using XCOM.Schema.Standard.Extension;

namespace XCOM.Schema.EDapper.Realization
{
    internal class XMDeleteable<T> : IXMDeleteable<T> where T : class, new()
    {

        /// <summary>
        /// 数据库连接配置对象
        /// </summary>
        protected DBConnection _dbConfig;

        protected IXMSQLMapper XMDBSql { get; set; }

        internal XMDeleteModel<T> Model { get; set; }

        public XMDeleteable(DBConnection dbContext)
        {
            this._dbConfig = dbContext;
            this.XMDBSql = new XMSQLMapper(dbContext);
            this.Model = XMRealization.GetDeleteEntity<T>(dbContext.DBProviderType);
        }
        /// <summary>
        /// 删除指定主键的实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public virtual int Delete(object keyValue)
        {
            if (this.Model.PrimaryKeys == null || this.Model.PrimaryKeys.Count == 0)
            {
                throw new Exception($"表{BaseModel<T>.GetTableName()}不存在主键，不能通过主键值删除");
            }
            if (this.Model.PrimaryKeys.Count > 1)
            {
                throw new Exception($"表{BaseModel<T>.GetTableName()}主键存在多个，不能通过一个主键值删除");
            }

            var primaryKey = this.Model.PrimaryKeys.FirstOrDefault();
            this.Model.Where.Append($"{primaryKey.GetColumnName()} = @{primaryKey.Name}");
            this.Model.Parameters.Add($"@{primaryKey.Name}", keyValue);
            return this.XMDBSql.Execute(this.Model.DeleteSql, this.Model.Parameters);

        }
        /// <summary>
        /// 异步删除指定主键的实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(object keyValue)
        {
            if (this.Model.PrimaryKeys.Count == 0)
            {
                throw new Exception($"表{BaseModel<T>.GetTableName()}不存在主键，不能通过主键值删除");
            }
            if (this.Model.PrimaryKeys.Count > 1)
            {
                throw new Exception($"表{BaseModel<T>.GetTableName()}主键存在多个，不能通过一个主键值删除");
            }

            var primaryKey = this.Model.PrimaryKeys.FirstOrDefault();
            this.Model.Where.Append($"{primaryKey.GetColumnName()} = @{primaryKey.Name}");
            this.Model.Parameters.Add($"@{primaryKey.Name}", keyValue);
            return await this.XMDBSql.ExecuteAsync(this.Model.DeleteSql, this.Model.Parameters);
        }
        /// <summary>
        /// 删除lambda匹配的实体
        /// </summary>
        /// <param name="expression">lambda</param>
        /// <returns></returns>
        public virtual int Delete(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
            {
                Expression exp = expression.Body as Expression;

                var obj = new XMLambda(this._dbConfig);
                var resultSql = obj.VisitXMLambda(exp);
                this.Model.Parameters = obj.Parameters;
                if (this.Model.Where.Length > 0)
                {
                    this.Model.Where.Append(" and ");
                }
                this.Model.Where.Append(resultSql);
                return this.XMDBSql.Execute(this.Model.DeleteSql, this.Model.Parameters);
            }
            else
            {
                throw new Exception("表达式不能为空");
            }
        }
        /// <summary>
        /// 异步删除lambda匹配的实体
        /// </summary>
        /// <param name="expression">lambda</param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
            {
                Expression exp = expression.Body as Expression;

                var obj = new XMLambda(this._dbConfig);
                var resultSql = obj.VisitXMLambda(exp);
                this.Model.Parameters = obj.Parameters;
                if (this.Model.Where.Length > 0)
                {
                    this.Model.Where.Append(" and ");
                }
                this.Model.Where.Append(resultSql);
                return await this.XMDBSql.ExecuteAsync(this.Model.DeleteSql, this.Model.Parameters);
            }
            else
            {
                throw new Exception("表达式不能为空");
            }
        }

        public string DebugSql()
        {
            return this.Model.DeleteSql;
        }
    }
}

