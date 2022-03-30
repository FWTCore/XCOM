using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Model
{
    public class TableColumnNameEntity
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 字符长度
        /// </summary>
        public int? CharacterMaximumLength { get; set; }
        /// <summary>
        /// 数字长度
        /// </summary>
        public int? NumericPrecision { get; set; }
        /// <summary>
        /// 小数位数
        /// </summary>
        public int? NumericScale { get; set; }
        /// <summary>
        /// 是否允许非空
        /// </summary>
        public string IsNullable { get; set; }
        /// <summary>
        /// 是否自增
        /// </summary>
        public bool? IsAuto { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ColumnComment { get; set; }


    }
}
