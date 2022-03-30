using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.Utility;
using XCOM.Schema.Standard.System;
using XCOM.Schema.Standard.Utility;
using static Dapper.SqlMapper;
using XCOM.Schema.EDapper.Utility;

namespace XCOM.Schema.EDapper.DBClient
{
    public class XMSQLMapper : XMDisposableResource, IXMSQLMapper
    {
        public XMSQLMapper(DBConnection dbConnection)
        {
            this._dbConfig = dbConnection ?? throw new Exception("DBConnection 不能为空");
        }
        public XMSQLMapper(string connectionKey)
        {
            if (string.IsNullOrWhiteSpace(connectionKey))
            {
                throw new Exception("ConnectionKey 不能为空!");
            }
            this._dbConfig = XMDBConfig.ConfigSetting.DBConnectionList.FirstOrDefault(f => f.Key.Trim().ToUpper() == connectionKey.Trim().ToUpper());
            if (_dbConfig == null)
            {
                throw new Exception(string.Format("SqlKey:{0} 无效", connectionKey));
            }
        }

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 待释放资源
            }
        }

        /// <summary>
        /// 数据库连接配置对象
        /// </summary>
        protected DBConnection _dbConfig;
        public int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.Execute(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"Execute 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.ExecuteAsync(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteAsync 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public IDataReader ExecuteReader(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.ExecuteReader(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteReader 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.ExecuteReaderAsync(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteReaderAsync 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public T ExecuteScalar<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.ExecuteScalar<T>(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteScalar<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }
        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.ExecuteScalarAsync<T>(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteScalarAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.Query<T>(sql, param, buffered: buffered, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"Query<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QueryAsync<T>(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public T QueryFirst<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QueryFirst<T>(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryFirst<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }
        public Task<T> QueryFirstAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QueryFirstAsync<T>(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryFirstAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QueryFirstOrDefault<T>(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryFirstOrDefault<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryFirstOrDefaultAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public T QuerySingle<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QuerySingle<T>(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QuerySingle<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QuerySingleAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QuerySingleAsync<T>(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QuerySingleAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public T QuerySingleOrDefault<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QuerySingleOrDefault<T>(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QuerySingleOrDefault<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                return conn.QuerySingleOrDefaultAsync<T>(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QuerySingleOrDefaultAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public void QueryMultiple(string sql, object param, Action<GridReader> readerCallback, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                using var reader = conn.QueryMultiple(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
                readerCallback?.Invoke(reader);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryMultiple 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public async Task QueryMultipleAsync(string sql, object param, Action<GridReader> readerCallback, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                using var reader = await conn.QueryMultipleAsync(sql, param, commandTimeout: commandTimeout ?? this._dbConfig.TimeOut, commandType: commandType);
                readerCallback?.Invoke(reader);
            }
            catch (Exception ex)
            {
                XMLog.Error($"QueryMultipleAsync 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        #region 执行事务

        /*调用方式
         * DBSqlHelper.ExecuteTransaction((trans) =>
         * {
         *     trans.Execute(sqlinsertct, selectedClass);
         *     trans.Execute(sqlinsertict, selectedClass);
         * });
         */
        /// <summary>
        /// 事务方法
        /// </summary>
        /// <param name="tranAction"></param>
        /// <param name="level"></param>
        public void ExecuteTransaction(Action<IXMSQLMapper> tranAction, IsolationLevel? level = null)
        {
            try
            {
                using IDbConnection conn = XMDBProvider.DBConnectionProvider(_dbConfig);
                conn.Open();
                using IDbTransaction tran = XMDBProvider.DBTransactionProvider(conn, level);
                using IXMSQLMapper tranObj = new XMTransaction(conn, tran);
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
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                XMLog.Error($"ExecuteTransaction 执行异常：{ex.Message}");
                throw;
            }
            finally
            {
                this.Dispose();
            }
        }


        #endregion


    }
}
