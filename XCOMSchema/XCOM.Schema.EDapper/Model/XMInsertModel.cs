using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Extension;

namespace XCOM.Schema.EDapper.Model
{
    internal class XMInsertModel<T> : BaseModel<T> where T : class, new()
    {
        public XMInsertModel()
        {
        }

        /// <summary>
        /// 获取参数名称
        /// </summary>
        protected List<string> InsertFields
        {
            get
            {
                var insertFields = this.MappedProperties.Where(insertitem => !insertitem.IsDefinedAttribute()).ToList();
                return insertFields.Select(insertitem => insertitem.Name).ToList();
            }
        }

        /// <summary>
        /// 数据库字段名称
        /// </summary>
        protected List<string> InsertColumns
        {
            get
            {
                var insertFields = this.MappedProperties.Where(insertitem => !insertitem.IsDefinedAttribute()).ToList();
                return insertFields.Select(insertitem => insertitem.GetColumnName()).ToList();
            }
        }


        /// <summary>
        /// 默认是MsSqlServer(获取自增id)
        /// </summary>
        public virtual string InsertSql_AutoId
        {
            get
            {
                return $"insert into {GetTableName()} ({string.Join(",", this.InsertColumns)}) value(@{string.Join(",@", this.InsertFields)});SELECT @@IDENTITY";
            }
        }
        /// <summary>
        /// 默认是MsSqlServer
        /// </summary>
        public virtual string InsertSql
        {
            get
            {
                return $"insert into {GetTableName()} ({string.Join(",", this.InsertColumns)}) value(@{string.Join(",@", this.InsertFields)})";
            }
        }
    }
}