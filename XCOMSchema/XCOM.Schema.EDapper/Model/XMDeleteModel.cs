using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Model
{
    internal class XMDeleteModel<T> : BaseModel<T> where T : class, new()
    {
        public XMDeleteModel()
        {
            this.Where = new StringBuilder();
        }

        public StringBuilder Where { get; set; }

        /// <summary>
        /// 默认是MsSqlServer
        /// </summary>
        public virtual string DeleteSql
        {
            get
            {
                if (this.Where.Length == 0)
                {
                    return "";
                }
                return $"delete from {GetTableName()} {this.WhereSql}";
            }
        }

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
    }
}
