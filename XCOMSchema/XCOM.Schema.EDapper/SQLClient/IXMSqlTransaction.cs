using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.SQLClient
{
    public interface IXMSqlTransaction : IXMSqlCommand
    {
        /// <summary>
        /// 设置sqlkey
        /// </summary>
        /// <param name="sqlKey"></param>
        void SetSqlKey(string sqlKey);
    }
}
