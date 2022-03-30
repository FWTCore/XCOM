using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.DataAccess
{
    internal class FieldDimension
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length { get; set; }

        public FieldDimension(string name, int length)
        {
            Name = name;
            Length = length;
        }
    }
}
