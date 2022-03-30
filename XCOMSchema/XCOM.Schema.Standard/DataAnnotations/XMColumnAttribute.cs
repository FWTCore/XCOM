using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class XMColumnAttribute : Attribute
    {
        public XMColumnAttribute()
        {

        }

        public XMColumnAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
