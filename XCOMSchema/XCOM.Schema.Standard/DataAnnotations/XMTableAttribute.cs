using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class XMTableAttribute : Attribute
    {
        public XMTableAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public string Schema { get; set; }

    }
}
