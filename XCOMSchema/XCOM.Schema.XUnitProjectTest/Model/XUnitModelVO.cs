using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.XUnitProjectTest.Model
{
    public class XUnitModelVO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsExist { get; set; }

        public XUnitType Type { get; set; }


    }
    public enum XUnitType
    {
        None = 0,
        Now = 1,
    }
}
