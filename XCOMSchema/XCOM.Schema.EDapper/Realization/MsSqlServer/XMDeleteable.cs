
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;

namespace XCOM.Schema.EDapper.Realization.MsSqlServer
{
    internal class XMDeleteable<T> : Realization.XMDeleteable<T> where T : class, new()
    {
        public XMDeleteable(DBConnection dbContext) : base(dbContext)
        {
        }




    }
}
