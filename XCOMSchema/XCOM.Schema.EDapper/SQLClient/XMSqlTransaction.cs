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

namespace XCOM.Schema.EDapper.SQLClient
{
    public class XMSqlTransaction : XMSqlBase, IXMSqlTransaction
    {
        private readonly IDbConnection conn = null;
        private readonly IDbTransaction tran = null;

        public XMSqlTransaction(IDbConnection conn, IDbTransaction tran, XMSqlBase @base)
        {
            this.conn = conn;
            this.tran = tran;
            this._dbConfig = @base._dbConfig;
            this.sqlNode = @base.sqlNode;
            this.Parameters = @base.Parameters;
            this._queryConditionString.Append(@base.QueryConditionString);
            this._commandText.Append(@base.CommandText);
            this._endScriptString.Append(@base.EndScriptString);
        }

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.conn?.Dispose();
                this.tran?.Dispose();
            }
        }

        public void SetSqlKey(string sqlKey)
        {
            if (string.IsNullOrWhiteSpace(sqlKey))
            {
                throw new Exception("SqlKey 不能为空!");
            }
            sqlNode = XMSQLScript.GetSQLList.Find(f => f.SQLKey.Trim().ToUpper() == sqlKey.Trim().ToUpper());
            if (sqlNode == null)
            {
                throw new Exception($"SqlKey:{sqlKey} 无效!");
            }
            if (sqlNode.ConnectionKey != this._dbConfig.Key)
            {
                throw new Exception("事务不能设置多数据源");
            }
            _commandText.Clear().Append(sqlNode.Text);
            Parameters = new DynamicParameters();
            _queryConditionString.Clear();
            _endScriptString.Clear();
        }


        #region 数据库方法

        public int Execute(CommandType? commandType = null)
        {
            try
            {
                return this.conn.Execute(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 Execute 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<int> ExecuteAsync(CommandType? commandType = null)
        {
            try
            {
                return this.conn.ExecuteAsync(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 ExecuteAsync 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public IDataReader ExecuteReader(CommandType? commandType = null)
        {
            try
            {
                return this.conn.ExecuteReader(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 ExecuteReader 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<IDataReader> ExecuteReaderAsync(CommandType? commandType = null)
        {
            try
            {
                return this.conn.ExecuteReaderAsync(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 ExecuteReaderAsync 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public T ExecuteScalar<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.ExecuteScalar<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 ExecuteScalar<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> ExecuteScalarAsync<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.ExecuteScalarAsync<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 ExecuteScalarAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public IEnumerable<T> Query<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.Query<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 Query<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<IEnumerable<T>> QueryAsync<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.QueryAsync<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public T QueryFirst<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.QueryFirst<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryFirst<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QueryFirstAsync<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.QueryFirstAsync<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryFirstAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public T QueryFirstOrDefault<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.QueryFirstOrDefault<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryFirstOrDefault<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.QueryFirstOrDefaultAsync<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryFirstOrDefaultAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public T QuerySingle<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.QuerySingle<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QuerySingle<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QuerySingleAsync<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.QuerySingleAsync<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QuerySingleAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public T QuerySingleOrDefault<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.QuerySingleOrDefault<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QuerySingleOrDefault<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(CommandType? commandType = null)
        {
            try
            {
                return this.conn.QuerySingleOrDefaultAsync<T>(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QuerySingleOrDefaultAsync<T> 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public void QueryMultiple(Action<GridReader> readerCallback, CommandType? commandType = null)
        {
            try
            {
                using var reader = this.conn.QueryMultiple(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
                readerCallback?.Invoke(reader);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryMultiple 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }

        public async Task QueryMultipleAsync(Action<GridReader> readerCallback, CommandType? commandType = null)
        {
            try
            {
                using var reader = await this.conn.QueryMultipleAsync(this.GetExecuteScript(), this.Parameters, this.tran, commandTimeout: this.GetTimeout(), commandType: commandType);
                readerCallback?.Invoke(reader);
            }
            catch (Exception ex)
            {
                XMLog.Error($"事务 QueryMultipleAsync 执行异常：{ex.Message}\nSQL语句：{this.GetExecuteScript()}\n 参数：{XMJson.Serailze(this.Parameters.GetDapperParameter())}");
                throw;
            }
        }



        #endregion

    }
}
