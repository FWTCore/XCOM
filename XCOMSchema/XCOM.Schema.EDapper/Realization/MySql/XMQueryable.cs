using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;

namespace XCOM.Schema.EDapper.Realization.MySql
{
    internal class XMQueryable<T> : Realization.XMQueryable<T> where T : class, new()
    {
        public XMQueryable(DBConnection dbContext) : base(dbContext)
        {
        }


    }
}