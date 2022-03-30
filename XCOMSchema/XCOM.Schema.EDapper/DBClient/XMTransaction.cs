using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.Utility;
using XCOM.Schema.Standard.System;
using XCOM.Schema.Standard.Utility;
using static Dapper.SqlMapper;

namespace XCOM.Schema.EDapper.DBClient
{
    public class XMTransaction : XMDisposableResource, IXMSQLMapper
    {
        private readonly IDbConnection conn = null;
        private readonly IDbTransaction tran = null;
        public XMTransaction(IDbConnection conn, IDbTransaction tran)
        {
            this.conn = conn;
            this.tran = tran;
        }

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.conn?.Dispose();
                this.tran?.Dispose();
            }
        }

        public int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.Execute(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 Execute 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.ExecuteAsync(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 ExecuteAsync 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public IDataReader ExecuteReader(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.ExecuteReader(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 ExecuteReader 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.ExecuteReaderAsync(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 ExecuteReaderAsync 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public T ExecuteScalar<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.ExecuteScalar<T>(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 ExecuteScalar<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.ExecuteScalarAsync<T>(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 ExecuteScalarAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.Query<T>(sql, param, this.tran, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 Query<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }


        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.QueryAsync<T>(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }


        public T QueryFirst<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.QueryFirst<T>(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryFirst<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QueryFirstAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.QueryFirstAsync<T>(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryFirstAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.QueryFirstOrDefault<T>(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryFirstOrDefault<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.QueryFirstOrDefaultAsync<T>(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryFirstOrDefaultAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public void QueryMultiple(string sql, object param, Action<GridReader> readerCallback, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using var reader = this.conn.QueryMultiple(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
                readerCallback?.Invoke(reader);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryMultiple 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public async Task QueryMultipleAsync(string sql, object param, Action<GridReader> readerCallback, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using var reader = await this.conn.QueryMultipleAsync(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
                readerCallback?.Invoke(reader);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryMultipleAsync 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public T QuerySingle<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.QuerySingle<T>(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QuerySingle<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QuerySingleAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.QuerySingleAsync<T>(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QuerySingleAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public T QuerySingleOrDefault<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.QuerySingleOrDefault<T>(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QuerySingleOrDefault<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.conn.QuerySingleOrDefaultAsync<T>(sql, param, this.tran, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QuerySingleOrDefaultAsync<T> 执行异常：{ex.Message}\nSQL语句：{sql}\n 参数：{XMJson.Serailze(param.GetDapperParameter())}");
                throw;
            }
        }
    }
}
