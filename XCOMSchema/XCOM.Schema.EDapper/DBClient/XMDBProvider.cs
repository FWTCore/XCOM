using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.Utility;

namespace XCOM.Schema.EDapper.DBClient
{
    public static class XMDBProvider
    {
        /// <summary>
        /// 获取对应数据数据库解析对象
        /// </summary>
        /// <param name="connectionKey"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public static IDbConnection DBConnectionProvider(string connectionKey, out DBConnection dbConnection)
        {
            if (string.IsNullOrWhiteSpace(connectionKey))
            {
                throw new Exception(string.Format("ConnectionKey 不能为空!", connectionKey));
            }
            dbConnection = XMDBConfig.ConfigSetting.DBConnectionList.FirstOrDefault(f => f.Key.Trim().ToUpper() == connectionKey.Trim().ToUpper());
            if (dbConnection == null)
            {
                throw new Exception($"ConnectionKey:{connectionKey} 无效!");
            }
            return DBConnectionProvider(dbConnection);
        }

        /// <summary>
        /// 获取对应数据数据库解析对象
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public static IDbConnection DBConnectionProvider(DBConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new Exception($"DBConnection 不能为空");
            }
            if (dbConnection.DBProviderType == XMProviderType.NONE)
            {
                throw new Exception($"DBConnection 数据库类型未配置");
            }
            if (string.IsNullOrWhiteSpace(dbConnection.ConnectionString))
            {
                throw new Exception($"DBConnection 数据库连接未配置");
            }
            return dbConnection.DBProviderType switch
            {
                XMProviderType.MsSqlServer => MsSqlServerConnection(dbConnection.ConnectionString),
                XMProviderType.MySql => MySqlConnection(dbConnection.ConnectionString),
                XMProviderType.Oracle => OracleConnection(dbConnection.ConnectionString),
                XMProviderType.SQLite => SQLiteConnection(dbConnection.ConnectionString),
                _ => throw new Exception($"DBConnection 数据库类型不支持")
            };
        }

        /// <summary>
        /// 事务对象操作
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static IDbTransaction DBTransactionProvider(IDbConnection conn, IsolationLevel? level = null)
        {
            IDbTransaction tran;
            if (level == null)
            {
                tran = conn.BeginTransaction();
            }
            else
            {
                tran = conn.BeginTransaction(level.Value);
            }
            return tran;
        }

        #region 生成

        /// <summary>
        /// 获取Mysql连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static IDbConnection MySqlConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
        /// <summary>
        /// 获取mssql连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static IDbConnection MsSqlServerConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
        /// <summary>
        /// 获取sqlite连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static IDbConnection SQLiteConnection(string connectionString)
        {
            return new SqliteConnection(connectionString);
        }
        /// <summary>
        /// 获取Oracle连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static IDbConnection OracleConnection(string connectionString)
        {
            return new OracleConnection(connectionString);
        }


        #endregion

    }
}
