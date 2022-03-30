using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XCOM.Schema.EDapper.DataAccess
{
    [Serializable]
    public class DBConfig
    {
        //
        // 摘要:
        //     数据库连接字符串
        public List<DBConnection> DBConnectionList
        {
            get;
            set;
        }

        //
        // 摘要:
        //     SQL语句的文件列表
        [XmlArrayItem("SQLFile")]
        public List<string> SQLFileList
        {
            get;
            set;
        }


    }
}
