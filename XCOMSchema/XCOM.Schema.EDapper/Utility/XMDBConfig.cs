using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.Standard;
using XCOM.Schema.Standard.Cache;
using XCOM.Schema.Standard.Security;
using XCOM.Schema.Standard.Utility;

namespace XCOM.Schema.EDapper.Utility
{
    /// <summary>
    /// 获取数据库配置类
    /// </summary>
    public class XMDBConfig
    {
        private static readonly object _obj = new();

        public static DBConfig ConfigSetting => XMMemoryCache.SetCache("XCOM_DataAccess_DBConfig", LoadConfig());

        private static DBConfig LoadConfig()
        {
            lock (_obj)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "Data", "DB.config");
                if (File.Exists(path))
                {
                    DBConfig dbConfig = XMSerialize.LoadFromXml<DBConfig>(path);
                    dbConfig.DBConnectionList.ForEach(dBConnection =>
                    {
                        if (!string.IsNullOrWhiteSpace(dBConnection.IsEncrypt) && (dBConnection.IsEncrypt.Trim().ToUpper() == "Y" || dBConnection.IsEncrypt.Trim().ToUpper() == "YES"))
                        {
                            dBConnection.ConnectionString = XMCrypto.Decrypt(dBConnection.ConnectionString);
                        }

                    });
                    dbConfig.SQLFileList.ForEach(file =>
                    {
                        file = file.Trim();
                    });
                    var repeatDataList = dbConfig.SQLFileList.GroupBy(file => file.Trim()).Where(e => e.Count() > 1).ToList();
                    if (repeatDataList.Count > 0)
                    {
                        throw new Exception($"DB.config文件中，SQLFile \"{path}\"有重复！");
                    }
                    return dbConfig;
                }
                throw new Exception($"DB.config文件中，SQLFile \"{path}\" 不存在！");
            }
        }
    }
}
