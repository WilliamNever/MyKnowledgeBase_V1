using Microsoft.Data.SqlClient;
using StandardLibraryForDotNetX.Factories;
using System.Data;

namespace StandardLibraryForDotNetX.Interfaces
{
    public interface IDbProcessingFactory
    {
        SqlConnection CreateSqlConnection();
        Task<SqlDataReader> CreateCloseConnectionReaderAsync(SqlCommand command, CancellationToken token = default);
        Task<SqlDataReader> CreateReaderAsync(SqlCommand command, CommandBehavior cmdBehavior, CancellationToken token = default);
        Task<object> ExecuteScalarAsync(SqlCommand com, CancellationToken token = default);
        Task<int> ExecuteNonQueryAsync(SqlCommand com, CancellationToken token = default);
        Task<DataReaderOptions<TM>> ReadDataBaseOnSqlReaderAsync<TM>(SqlCommand com, DataReaderOptions<TM> rOp, Func<SqlDataReader, TM, DataReaderOptions<TM>?, TM> readFunc, string? TotalColumnName = null, CancellationToken token = default) where TM : new();
    }
}
