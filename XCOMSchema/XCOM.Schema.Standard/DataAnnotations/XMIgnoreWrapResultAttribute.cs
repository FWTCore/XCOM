using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class XMIgnoreWrapResultAttribute : Attribute
    {
    }
}
