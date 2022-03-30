using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XCOM.Schema.EDapper.DataAccess
{
    [Serializable]
    public class DBConnection
    {
        [XmlAttribute]
        public string Key
        {
            get;
            set;
        }
        //
        // 摘要:
        //     数据库类型：SqlServer，Access，MySQL
        [XmlAttribute]
        public string DBType
        {
            get;
            set;
        }

        //
        // 摘要:
        //     当前的数据提供者对象
        [XmlIgnore]
        public XMProviderType DBProviderType
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DBType) || DBType.Trim().ToUpper() == "MYSQL")
                {
                    return XMProviderType.MySql;
                }
                if (DBType.Trim().ToUpper() == "SQLSERVER")
                {
                    return XMProviderType.MsSqlServer;
                }
                if (DBType.Trim().ToUpper() == "MYSQL")
                {
                    return XMProviderType.MySql;
                }
                if (DBType.Trim().ToUpper() == "ORACLE")
                {
                    return XMProviderType.Oracle;
                }
                if (DBType.Trim().ToUpper() == "SQLITE")
                {
                    return XMProviderType.SQLite;
                }

                return XMProviderType.MySql;
            }
        }

        //
        // 摘要:
        //     是否加密，只有为Y或者YES时，才表示要加密
        [XmlAttribute]
        public string IsEncrypt
        {
            get;
            set;
        }

        [XmlAttribute]
        public string IsWrite { get; set; }
        [XmlAttribute]
        public int TimeOut { get; set; }
        [XmlElement]
        public string ConnectionString
        {
            get;
            set;
        }
    }
}
