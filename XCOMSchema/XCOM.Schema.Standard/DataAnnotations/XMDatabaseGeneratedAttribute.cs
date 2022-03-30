using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.DataAnnotations
{
    /// <summary>
    /// 字段自增
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class XMDatabaseGeneratedAttribute : Attribute
    {
    }
}
