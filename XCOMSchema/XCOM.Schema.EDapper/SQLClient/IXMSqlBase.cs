using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace XCOM.Schema.EDapper.SQLClient
{
    public interface IXMSqlBase
    {
        int Execute(CommandType? commandType = null);
        Task<int> ExecuteAsync(CommandType? commandType = null);

        IDataReader ExecuteReader(CommandType? commandType = null);
        Task<IDataReader> ExecuteReaderAsync(CommandType? commandType = null);

        T ExecuteScalar<T>(CommandType? commandType = null);
        Task<T> ExecuteScalarAsync<T>(CommandType? commandType = null);

        IEnumerable<T> Query<T>(CommandType? commandType = null);
        Task<IEnumerable<T>> QueryAsync<T>(CommandType? commandType = null);

        T QueryFirst<T>(CommandType? commandType = null);
        Task<T> QueryFirstAsync<T>(CommandType? commandType = null);

        T QueryFirstOrDefault<T>(CommandType? commandType = null);
        Task<T> QueryFirstOrDefaultAsync<T>(CommandType? commandType = null);

        T QuerySingle<T>(CommandType? commandType = null);
        Task<T> QuerySingleAsync<T>(CommandType? commandType = null);

        T QuerySingleOrDefault<T>(CommandType? commandType = null);
        Task<T> QuerySingleOrDefaultAsync<T>(CommandType? commandType = null);

        void QueryMultiple(Action<GridReader> readerCallback, CommandType? commandType = null);
        Task QueryMultipleAsync(Action<GridReader> readerCallback, CommandType? commandType = null);

    }
}


