using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;

namespace XCOM.Schema.EDapper.Realization.MySql
{
    internal class XMInsertable<T> : Realization.XMInsertable<T> where T : class, new()
    {
        public XMInsertable(DBConnection dbContext) : base(dbContext)
        {
        }


    }
}
