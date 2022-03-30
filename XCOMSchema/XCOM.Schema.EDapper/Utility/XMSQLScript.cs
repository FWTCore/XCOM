using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.Standard.Cache;
using XCOM.Schema.Standard.Utility;

namespace XCOM.Schema.EDapper.Utility
{
    /// <summary>
    /// 获取数据库脚本类
    /// </summary>
    internal static class XMSQLScript
    {

        private static readonly object _obj = new();

        private static readonly object _obj2 = new();

        public static List<SQLScript> GetSQLList => XMMemoryCache.SetCache("XCOM_DataAccess_SQLConfig", LoadConfig());

        private static List<SQLScript> LoadConfig()
        {
            var sqlScriptlist = new List<SQLScript>();
            DBConfig configSetting = XMDBConfig.ConfigSetting;
            if (configSetting != null && configSetting.SQLFileList != null)
            {
                lock (_obj)
                {
                    foreach (string sqlFile in configSetting.SQLFileList)
                    {
                        lock (_obj2)
                        {
                            var pathField = sqlFile.Split('\\');
                            var filePath = Path.Combine(pathField);
                            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "Data", filePath);
                            if (!File.Exists(path))
                            {
                                throw new Exception($"无效文件路径sqlFile：{path}  ");
                            }
                            var sqlConfig = XMSerialize.LoadFromXml<SQLConfig>(path);
                            if (sqlConfig.SQLList != null)
                            {
                                foreach (SQLScript sql in sqlConfig.SQLList)
                                {
                                    if (sqlScriptlist.Exists(f => f.SQLKey.Trim().ToUpper() == sql.SQLKey.Trim().ToUpper()))
                                    {
                                        throw new Exception($"SQLKey \"{sql.SQLKey.Trim()}\" 有重复，请检查你的配置文件！");
                                    }
                                    sql.ParameterNameList = new List<string>();
                                    Regex regex = new("@\\w*", RegexOptions.IgnoreCase);
                                    MatchCollection matchCollection = regex.Matches(sql.Text.Trim());
                                    if (matchCollection != null && matchCollection.Count > 0)
                                    {
                                        foreach (Match match in matchCollection)
                                        {
                                            if (!sql.ParameterNameList.Exists(f => f.Trim().ToLower() == match.Value.Trim().ToLower()))
                                            {
                                                sql.ParameterNameList.Add(match.Value);
                                            }
                                        }
                                    }
                                    sqlScriptlist.Add(sql);
                                }
                            }
                        }
                    }
                    return sqlScriptlist;
                }
            }
            return sqlScriptlist;
        }

    }
}


