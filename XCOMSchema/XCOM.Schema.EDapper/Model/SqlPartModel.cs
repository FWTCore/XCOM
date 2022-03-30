using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Model
{
    public struct SqlPartModel
    {
        /// <summary>
        /// 原始sql语句
        /// </summary>
        public string Sql;
        /// <summary>
        /// 统计数量sql语句
        /// </summary>
        public string SqlCount;
        /// <summary>
        /// sql语句中select字段部分
        /// </summary>
        public string SqlSelectField;
        /// <summary>
        /// sql语句中from部分
        /// </summary>
        public string SqlFromCondition;
        /// <summary>
        /// order by 部分
        /// </summary>
        public string SqlOrderBy;

    }
}
