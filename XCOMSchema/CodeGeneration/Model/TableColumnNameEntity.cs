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
        /// 表字段
        /// </summary>
        public string TableName { get; set; }
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
        public bool IsNullable { get; set; }
        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsAuto { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ColumnComment { get; set; }
        /// <summary>
        /// 字符长度字符串
        /// MYSQL获取准的数字长度
        /// </summary>
        public string NumericPrecisionStr { get; set; }


    }
}
