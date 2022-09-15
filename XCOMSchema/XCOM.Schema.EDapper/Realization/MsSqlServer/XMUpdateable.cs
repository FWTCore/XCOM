using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;

namespace XCOM.Schema.EDapper.Realization.MsSqlServer
{
    internal class XMUpdateable<T> : Realization.XMUpdateable<T> where T : class, new()
    {
        public XMUpdateable(DBConnection dbContext) : base(dbContext)
        {
        }




    }
}
