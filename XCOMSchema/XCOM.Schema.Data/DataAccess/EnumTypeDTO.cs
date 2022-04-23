using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Data.DataAccess
{
    public class EnumTypeDTO
    {
        /// <summary>  
        /// 枚举对象的值  
        /// </summary> 
        public int Value { set; get; }

        /// <summary>  
        /// 名称  
        /// </summary>  
        public string Name { set; get; }

        /// <summary>  
        /// 描述  
        /// </summary> 
        public string Desction { set; get; }

    }
}
