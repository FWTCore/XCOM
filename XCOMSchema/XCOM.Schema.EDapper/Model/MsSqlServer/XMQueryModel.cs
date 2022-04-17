using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Model.MsSqlServer
{
    internal class XMQueryModel<T> : Model.XMQueryModel<T> where T : class, new()
    {
        public XMQueryModel()
        {

        }



    }
}
