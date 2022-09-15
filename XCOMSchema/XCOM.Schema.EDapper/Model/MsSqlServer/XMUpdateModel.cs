using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Model.MsSqlServer
{
    internal class XMUpdateModel<T> : Model.XMUpdateModel<T> where T : class, new()
    {
        public XMUpdateModel()
        {

        }

    }
}
