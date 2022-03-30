using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Extension;

namespace XCOM.Schema.EDapper.Model
{
    internal class XMQueryModel<T> : BaseModel<T> where T : class, new()
    {
        public XMQueryModel()
        {
            this.AllFields = this.MappedProperties.Select(mpe => $"{mpe.GetColumnName()} AS {mpe.Name}").ToList();
            this.Where = new StringBuilder();
            this.OrderBy = new List<string>();
        }
        /// <summary>
        /// 所有字段
        /// </summary>
        protected List<string> AllFields { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public StringBuilder Where { get; set; }

        public List<string> OrderBy { get; set; }

        /// <summary>
        /// 跳过数量
        /// </summary>
        public int? SkipCount { get; set; }

        /// <summary>
        /// 查询数量
        /// </summary>
        public int? FetchCount { get; set; }


        /// <summary>
        /// 默认是MsSqlServer
        /// </summary>
        public virtual string SelectSql
        {
            get
            {
                return $"select {this.TopSql} {this.FieldsSql} from {GetTableName()} {this.WhereSql} {this.OrderBySql}";
            }
        }


        public virtual string SqlFetchSql_2005_2008
        {
            get
            {
                if (SkipCount == null || SkipCount == 0)
                {
                    return $"select {this.TopSql} {this.FieldsSql} from {GetTableName()} {this.WhereSql} {this.OrderBySql}";
                }
                else
                {
                    return $"select {this.FieldsSql} from (select row_number() over({this.OrderBySql}) AS 'RowNumber',{this.FieldsSql} from {GetTableName()} {this.WhereSql}) as xm_temp_table where RowNumber > {SkipCount} and RowNumber <= {SkipCount + FetchCount}";
                }
            }
        }

        public virtual string SqlFetchSql
        {
            get
            {
                if (SkipCount == null || SkipCount == 0)
                {
                    return $"select {this.TopSql} {this.FieldsSql} from {GetTableName()} {this.WhereSql} {this.OrderBySql}";
                }
                else
                {
                    return $"select {this.FieldsSql} from {GetTableName()} {this.WhereSql} {this.OrderBySql} offset {SkipCount} row fetch next {FetchCount} rows only";
                }
            }
        }

        public virtual string CountSql
        {
            get
            {
                return $"select Count(1) from {GetTableName()} {this.WhereSql}";
            }
        }

        internal virtual string TopSql
        {
            get
            {
                if (this.FetchCount != null && this.FetchCount > 0)
                {
                    return $"top {this.FetchCount}";
                }
                return "";
            }
        }

        internal virtual string FieldsSql
        {
            get
            {
                return string.Join(",", this.AllFields);

            }
        }

        internal virtual string WhereSql
        {
            get
            {
                var result = "";
                if (!string.IsNullOrEmpty(this.Where.ToString()))
                {
                    result = " where " + this.Where;
                }
                return result;
            }
        }

        internal virtual string OrderBySql
        {
            get
            {
                if (this.OrderBy == null || this.OrderBy.Count == 0)
                    return DefaultOrderBySql;
                else
                    return $"ORDER BY {string.Join(",", this.OrderBy)}";
            }
        }


        internal virtual string GetCountSql(string sql)
        {
            return $"select count(1) from ({sql}) as a";
        }

        protected string DefaultOrderBySql
        {
            get
            {
                if (this.PrimaryKeys == null || this.PrimaryKeys.Count == 0)
                {
                    throw new Exception($"表{GetTableName()}没有主键，不能默认分页");
                }
                return $"ORDER BY {string.Join(",", this.PrimaryKeys.Select(e => e.GetColumnName()))}";
            }
        }

    }
}

