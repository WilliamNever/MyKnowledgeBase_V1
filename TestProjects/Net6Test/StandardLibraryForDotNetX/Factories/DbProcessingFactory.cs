using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StandardLibraryForDotNetX.Interfaces;
using System.Data;

namespace StandardLibraryForDotNetX.Factories
{
    public class DbProcessingFactory : IDbProcessingFactory
    {
        private ConnectionStrings _cStrings;
        private ILogger<DbProcessingFactory> _logger;
        public DbProcessingFactory(
            ILogger<DbProcessingFactory> logger
            , IOptions<ConnectionStrings> connectionStrings)
        {
            _logger = logger;
            _cStrings = connectionStrings.Value;
        }
        public SqlConnection CreateSqlConnection()
        {
            return new SqlConnection(_cStrings.ConnString);
        }

        public async Task<SqlDataReader> CreateCloseConnectionReaderAsync(
            SqlCommand command,
            CancellationToken token = default
            )
        {
            return await CreateReaderAsync(command, CommandBehavior.CloseConnection, token);
        }

        public async Task<SqlDataReader> CreateReaderAsync(
            SqlCommand command, CommandBehavior cmdBehavior,
            CancellationToken token = default
            )
        {
            return await command.ExecuteReaderAsync(cmdBehavior, token);
        }

        public async Task<DataReaderOptions<TM>> ReadDataBaseOnSqlReaderAsync<TM>
            (SqlCommand com, DataReaderOptions<TM> rOp,
            Func<SqlDataReader, TM, DataReaderOptions<TM>?, TM> readFunc,
            string? TotalColumnName = null, CancellationToken token = default)
            where TM : new()
        {
            try
            {
                rOp ??= new DataReaderOptions<TM>();
                using var conn = CreateSqlConnection();
                com.Connection = conn;
                await com.Connection.OpenAsync(token);
                using var reader = await CreateCloseConnectionReaderAsync(com, token);
                rOp.ReaderColumns = reader.GetColumnSchema().Select(x => x.ColumnName).ToList();
                rOp.ResetHasAddedTotal();
                while (await reader.ReadAsync(token))
                {
                    if (!rOp.HasAddedTotal && !string.IsNullOrEmpty(TotalColumnName)) // Only need to get this once, all rows are the same
                        rOp.AddTotal(reader.GetInt32(reader.GetOrdinal(TotalColumnName)));
                    rOp.Data.Add(readFunc(reader, new TM(), rOp));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errors in query command - {com.CommandText}");
                throw;
            }
            return rOp;
        }

        public async Task<object> ExecuteScalarAsync(SqlCommand com, CancellationToken token = default)
        {
            try {
                using var conn = CreateSqlConnection();
                com.Connection = conn;
                await com.Connection.OpenAsync(token);
                return await com.ExecuteScalarAsync(token);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errors in query command - {com.CommandText}");
                throw;
            }
        }

        public async Task<int> ExecuteNonQueryAsync(SqlCommand com, CancellationToken token = default)
        {
            try {
                using var conn = CreateSqlConnection();
                com.Connection = conn;
                await com.Connection.OpenAsync(token);
                return await com.ExecuteNonQueryAsync(token);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errors in query command - {com.CommandText}");
                throw;
            }
        }
    }

    public class DataReaderOptions<T>
    {
        public List<string>? ReaderColumns { get; set; }
        public int TotalCount { get => PerTotal.Sum(); }
        public List<T> Data { get; set; }
        private List<int> PerTotal { get; }
        public bool HasAddedTotal { get; private set; } = false;

        public DataReaderOptions()
        {
            Data = new List<T>();
            PerTotal = new List<int>();
        }
        public bool AddTotal(int total)
        {
            if (!HasAddedTotal)
            {
                PerTotal.Add(total);
                HasAddedTotal = true;
            }
            return HasAddedTotal;
        }
        public void ResetHasAddedTotal()
        {
            HasAddedTotal = false;
        }
    }

    public class ConnectionStrings
    {
        public string ConnString { get; set; }
    }
}
