using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.DataAccess
{
    public enum XMProviderType
    {

        /// <summary>
        /// 指定不设置任何选项
        /// </summary>
        NONE = 0,
        /// <summary>
        /// MsSqlServer
        /// </summary>
        [Description("MsSqlServer")]
        MsSqlServer = 1,
        /// <summary>
        /// MySql
        /// </summary>
        [Description("MySql")]
        MySql = 2,
        /// <summary>
        /// Oracle
        /// </summary>
        [Description("Oracle")]
        Oracle = 3,
        /// <summary>
        /// SQLite
        /// </summary>
        [Description("SQLite")]
        SQLite = 4,

    }
}
