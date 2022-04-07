using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.DBClient;
using XCOM.Schema.EDapper.Realization;
using XCOM.Schema.EDapper.Utility;
using XCOM.Schema.Standard.Extension;
using XCOM.Schema.Standard.System;
using XCOM.Schema.Standard.Utility;
using static Dapper.SqlMapper;

namespace XCOM.Schema.EDapper.SQLClient
{
    public class XMSqlCommand : XMSqlBase, IXMSqlCommand
    {
        public XMSqlCommand(string sqlKey)
        {
            if (string.IsNullOrWhiteSpace(sqlKey))
            {
                throw new Exception("sqlKey 不能为空!");
            }
            sqlNode = XMSQLScript.GetSQLList.Find(f => f.SQLKey.Trim().ToUpper() == sqlKey.Trim().ToUpper());
            if (sqlNode == null)
            {
                throw new Exception($"SQLKey:{sqlKey} 无效!");
            }
            _commandText.Clear().Append(sqlNode.Text);
            _dbConfig = XMDBConfig.ConfigSetting.DBConnectionList.FirstOrDefault(f => f.Key.Trim().ToUpper() == sqlNode.ConnectionKey.Trim().ToUpper());
            if (_dbConfig == null)
            {
                throw new Exception($"SQLKey:{sqlKey} 数据库配置无效!");
            }
        }

        public XMSqlCommand(string sqlKey, string dbKey)
        {
            if (string.IsNullOrWhiteSpace(sqlKey))
            {
                throw new Exception("sqlKey 不能为空!");
            }
            if (string.IsNullOrWhiteSpace(dbKey))
            {
                throw new Exception("dbKey 不能为空!");
            }
            sqlNode = XMSQLScript.GetSQLList.Find(f => f.SQLKey.Trim().ToUpper() == sqlKey.Trim().ToUpper());
            if (sqlNode == null)
            {
                throw new Exception($"SQLKey:{sqlKey} 无效!");
            }
            _commandText.Clear().Append(sqlNode.Text);

            _dbConfig = XMDBConfig.ConfigSetting.DBConnectionList.FirstOrDefault(f => f.Key.Trim().ToUpper() == dbKey);
            if (_dbConfig == null)
            {
                throw new Exception($"SQLKey:{dbKey} 数据库配置无效!");
            }
        }

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 资源释放
            }
        }

        #region 数据库方法

        public int Execute(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.Execute(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"Execute 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<int> ExecuteAsync(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.ExecuteAsync(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteAsync 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public IDataReader ExecuteReader(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.ExecuteReader(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteReader 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<IDataReader> ExecuteReaderAsync(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.ExecuteReaderAsync(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteReaderAsync 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public T ExecuteScalar<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.ExecuteScalar<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteScalar<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> ExecuteScalarAsync<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.ExecuteScalarAsync<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteScalarAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public IEnumerable<T> Query<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.Query<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"Query<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<IEnumerable<T>> QueryAsync<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QueryAsync<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public T QueryFirst<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QueryFirst<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryFirst<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QueryFirstAsync<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QueryFirstAsync<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryFirstAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public T QueryFirstOrDefault<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QueryFirstOrDefault<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryFirstOrDefault<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QueryFirstOrDefaultAsync<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryFirstOrDefaultAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public T QuerySingle<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QuerySingle<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QuerySingle<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QuerySingleAsync<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QuerySingleAsync<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QuerySingleAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public T QuerySingleOrDefault<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QuerySingleOrDefault<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QuerySingleOrDefault<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QuerySingleOrDefaultAsync<T>(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QuerySingleOrDefaultAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public void QueryMultiple(Action<GridReader> readerCallback, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                using var reader = conn.QueryMultiple(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
                readerCallback?.Invoke(reader);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryMultiple 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public async Task QueryMultipleAsync(Action<GridReader> readerCallback, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                using var reader = await conn.QueryMultipleAsync(this.GetExecuteScript(), this.Parameters, commandTimeout: this.GetTimeout(), commandType: commandType);
                readerCallback?.Invoke(reader);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryMultipleAsync 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        #endregion

        #region 事务

        public void ExecuteTransaction(Action<IXMSqlTransaction> tranAction, IsolationLevel? level = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                using IDbTransaction tran = XMDBProvider.DBTransactionProvider(conn, level);
                IXMSqlTransaction tranObj = new XMSqlTransaction(conn, tran, this);
                try
                {
                    tranAction(tranObj);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteTransaction 执行异常：{ex.Message}");
                throw;
            }
            finally
            {
                Dispose();
            }
        }


        #endregion

    }
}
