using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Model.MySql
{
    internal class XMQueryModel<T> : Model.XMQueryModel<T> where T : class, new()
    {
        public XMQueryModel()
        {

        }


        public override string SelectSql
        {
            get
            {
                return $"select {this.FieldsSql} from {GetTableName()} {this.WhereSql} {this.OrderBySql} {this.TopSql}";
            }
        }

        public override string SqlFetchSql_2005_2008
        {
            get
            {
                if (SkipCount == null || SkipCount == 0)
                {
                    return $"select {this.FieldsSql} from {GetTableName()} {this.WhereSql} {this.OrderBySql} {this.TopSql}";
                }
                else
                {
                    return $"select {this.FieldsSql} from {GetTableName()} {this.WhereSql} {this.OrderBySql} LIMIT {SkipCount},{FetchCount}";
                }
            }
        }

        public override string SqlFetchSql
        {
            get
            {
                if (SkipCount == null || SkipCount == 0)
                {
                    return $"select {this.FieldsSql} from {GetTableName()} {this.WhereSql} {this.OrderBySql} {this.TopSql}";
                }
                else
                {
                    return $"select {this.FieldsSql} from {GetTableName()} {this.WhereSql} {this.OrderBySql} LIMIT {SkipCount},{FetchCount}";
                }
            }
        }

        public override string CountSql
        {
            get
            {
                return $"select Count(1) from {GetTableName()} {this.WhereSql}";
            }
        }

        internal override string TopSql
        {
            get
            {
                if (this.FetchCount != null && this.FetchCount > 0)
                {
                    return $"LIMIT {this.FetchCount}";
                }
                return "";
            }
        }



    }
}
