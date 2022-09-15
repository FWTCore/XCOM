using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Model
{
    internal class XMUpdateModel<T> : BaseModel<T> where T : class, new()
    {
        public XMUpdateModel()
        {
            this.Where = new StringBuilder();
            this.Set = new StringBuilder();
        }

        /// <summary>
        /// where 条件
        /// </summary>
        public StringBuilder Where { get; set; }
        /// <summary>
        /// set内容
        /// </summary>
        public StringBuilder Set { get; set; }

        /// <summary>
        /// 默认是MsSqlServer
        /// </summary>
        public virtual string WhereSql
        {
            get
            {
                if (this.Where.Length == 0)
                {
                    return "";
                }
                return $" where {this.Where}";
            }
        }

        /// <summary>
        /// 默认是MsSqlServer
        /// </summary>
        public virtual string SetSql
        {
            get
            {
                if (this.Set.Length == 0)
                {
                    return "";
                }
                return $" {this.Set} ";
            }
        }



        /// <summary>
        /// 默认是MsSqlServer
        /// </summary>
        public virtual string UpdateSql
        {
            get
            {
                if (this.Where.Length == 0)
                {
                    return "";
                }
                return $"update {GetTableName()} set {this.SetSql} {this.WhereSql}";
            }
        }

    }
}
