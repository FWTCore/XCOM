using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Data.DataAccess;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.DBClient;
using XCOM.Schema.EDapper.LTS;
using XCOM.Schema.EDapper.Model;
using XCOM.Schema.Standard.Linq;

namespace XCOM.Schema.EDapper.Realization
{
    internal class XMQueryable<T> : IXMQueryable<T> where T : class, new()
    {
        /// <summary>
        /// 数据库连接配置对象
        /// </summary>
        protected DBConnection _dbConfig;

        protected IXMSQLMapper XMDBSql { get; set; }

        internal XMQueryModel<T> Model { get; set; }


        public XMQueryable(DBConnection dbContext)
        {
            this._dbConfig = dbContext;
            this.XMDBSql = new XMSQLMapper(dbContext);
            this.Model = XMRealization.GetQueryEntity<T>(dbContext.DBProviderType);
        }

        #region Where

        public virtual IXMQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            this.VisitCondition(expression);
            return this;
        }
        #endregion

        #region Skip Take

        public virtual IXMQueryable<T> Skip(int count)
        {
            this.Model.SkipCount = count;
            return this;
        }

        public virtual IXMQueryable<T> Take(int count)
        {
            this.Model.FetchCount = count;
            return this;
        }
        #endregion

        #region

        public virtual IXMQueryable<T> OrderBy<T1>(Expression<Func<T, T1>> expression)
        {
            Order(expression);
            return this;
        }

        public virtual IXMQueryable<T> OrderByDescending<T1>(Expression<Func<T, T1>> expression)
        {
            Order(expression, "desc");
            return this;
        }

        public virtual IXMQueryable<T> OrderBy(string name)
        {
            Order(name);
            return this;
        }

        public virtual IXMQueryable<T> OrderByDescending(string name)
        {
            Order(name, "desc");
            return this;
        }

        #endregion

        #region Count

        public virtual int Count()
        {
            return this.XMDBSql.ExecuteScalar<int>(this.GetCountSql(), this.Model.Parameters);

        }
        public virtual async Task<int> CountAsync()
        {
            return await this.XMDBSql.ExecuteScalarAsync<int>(this.GetCountSql(), this.Model.Parameters);
        }

        public virtual int Count(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
            {
                this.VisitCondition(expression);
            }
            return this.Count();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
            {
                this.VisitCondition(expression);
            }
            return await this.CountAsync();
        }
        #endregion

        #region Find First FirstOrDefault

        public virtual T Find(object KeyValue)
        {
            if (this.Model.PrimaryKeys == null || this.Model.PrimaryKeys.Count == 0)
            {
                throw new Exception($"表{BaseModel<T>.GetTableName()}不存在主键，不能通过主键值查询");
            }
            if (this.Model.PrimaryKeys.Count > 1)
            {
                throw new Exception($"表{BaseModel<T>.GetTableName()}主键存在多个，不能通过一个主键值查询");
            }
            var primaryKey = this.Model.PrimaryKeys.FirstOrDefault();
            var type = primaryKey.PropertyType;
            var parameter = Expression.Parameter(BaseModel<T>.GetModelType(), "ex");
            MemberExpression left = Expression.PropertyOrField(parameter, primaryKey.Name);
            ConstantExpression right = Expression.Constant(KeyValue, type);//创建常数
            var query = Expression.Equal(left, right);
            var where = Expression.Lambda<Func<T, bool>>(query, parameter);
            this.Where(where);
            return this.XMDBSql.QueryFirstOrDefault<T>(this.Model.SelectSql, this.Model.Parameters);


        }
        public virtual async Task<T> FindAsync(object KeyValue)
        {
            if (this.Model.PrimaryKeys == null || this.Model.PrimaryKeys.Count == 0)
            {
                throw new Exception($"表{BaseModel<T>.GetTableName()}不存在主键，不能通过主键值查询");
            }
            if (this.Model.PrimaryKeys.Count > 1)
            {
                throw new Exception($"表{BaseModel<T>.GetModelType()}主键存在多个，不能通过一个主键值查询");
            }
            var primaryKey = this.Model.PrimaryKeys.FirstOrDefault();
            var type = primaryKey.PropertyType;
            var parameter = Expression.Parameter(BaseModel<T>.GetModelType(), "ex");
            MemberExpression left = Expression.PropertyOrField(parameter, primaryKey.Name);
            ConstantExpression right = Expression.Constant(KeyValue, type);//创建常数
            var query = Expression.Equal(left, right);
            var where = Expression.Lambda<Func<T, bool>>(query, parameter);
            this.Where(where);
            return await this.XMDBSql.QueryFirstOrDefaultAsync<T>(this.Model.SelectSql, this.Model.Parameters);
        }

        public virtual T FirstOrDefault()
        {
            return this.XMDBSql.QueryFirstOrDefault<T>(this.Model.SelectSql, this.Model.Parameters);
        }

        public virtual async Task<T> FirstOrDefaultAsync()
        {
            return await this.XMDBSql.QueryFirstOrDefaultAsync<T>(this.Model.SelectSql, this.Model.Parameters);
        }


        public virtual T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
                this.VisitCondition(expression);
            return this.XMDBSql.QueryFirstOrDefault<T>(this.Model.SelectSql, this.Model.Parameters);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
                this.VisitCondition(expression);
            return await this.XMDBSql.QueryFirstOrDefaultAsync<T>(this.Model.SelectSql, this.Model.Parameters);
        }
        #endregion



        #region ToList ToTopList ToPageList

        public virtual List<T> ToList()
        {
            return this.XMDBSql.Query<T>(this.GetResultSql(), this.Model.Parameters).ToList();
        }

        public virtual async Task<IEnumerable<T>> ToListAsync()
        {
            return await this.XMDBSql.QueryAsync<T>(this.GetResultSql(), this.Model.Parameters);
        }

        public virtual List<T> ToTopList()
        {
            this.DefaultCount();
            return this.ToList();
        }

        public virtual async Task<IEnumerable<T>> ToTopListAsync()
        {
            this.DefaultCount();
            return await this.ToListAsync();
        }

        public virtual PageVOBase<T> ToPageList(PageROBase request)
        {
            var reuslt = new PageVOBase<T>();
            var skipCount = (request.PageIndex - 1) * request.PageSize;
            var takeCount = request.PageSize;
            Skip(skipCount).Take(takeCount);

            reuslt.PageIndex = request.PageIndex;
            reuslt.PageSize = request.PageSize;

            reuslt.TotalCount = Count();
            reuslt.Items = ToList();

            return reuslt;
        }

        #endregion

        #region  私有函数

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
        /// Lambda排序
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="expression"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        private IXMQueryable<T> Order<T1>(Expression<Func<T, T1>> expression, string sort = "asc")
        {
            if (expression != null)
            {
                var obj = new XMFieldVisitor();
                obj.Visit(expression);
                this.Model.OrderBy.AddRange(obj.ResultFields.Split(','));

                var index = this.Model.OrderBy.Count - 1;
                this.Model.OrderBy[index] += " " + sort;
            }
            return this;
        }

        /// <summary>
        /// 根据字符串排序
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        private IXMQueryable<T> Order(string name, string sort = "asc")
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.Model.OrderBy.Add(name);

                var index = this.Model.OrderBy.Count - 1;
                this.Model.OrderBy[index] += " " + sort;
            }
            return this;
        }

        /// <summary>
        /// 获取执行sql
        /// </summary>
        /// <returns></returns>
        private string GetResultSql()
        {
            string resultSql = this.Model.SqlFetchSql;
            if (this._dbConfig.DBProviderType == XMProviderType.MsSqlServer)
            {
                var version = this.XMDBSql.ExecuteScalar<string>("SELECT @@VERSION AS Expr1", null);
                if (version.StartsWith("Microsoft SQL Server 2005") || version.StartsWith("Microsoft SQL Server 2008"))
                {
                    resultSql = this.Model.SqlFetchSql_2005_2008;
                }
            }
            return resultSql;
        }

        /// <summary>
        /// 获取数量sql
        /// </summary>
        /// <returns></returns>
        private string GetCountSql()
        {
            return this.Model.CountSql;
        }

        /// <summary>
        /// 默认数量（避免查询全库）
        /// </summary>
        private void DefaultCount()
        {
            if (this.Model.SkipCount != null || this.Model.FetchCount != null)//走分页处理
            {
                if (this.Model.SkipCount == null)
                {
                    this.Model.SkipCount = 0;
                }
                if (this.Model.FetchCount == null)
                {
                    this.Model.FetchCount = 15;
                }
            }
        }

        public string DebugSql()
        {
            return this.GetResultSql();
        }

        public DynamicParameters DebugParam()
        {
            return this.Model.Parameters;
        }



        #endregion

    }
}
