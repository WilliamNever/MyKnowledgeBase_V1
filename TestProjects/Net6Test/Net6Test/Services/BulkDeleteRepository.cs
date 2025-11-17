using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Net6Test.Services
{
    /// <summary>
    /// this class needs EF core 7.0.0 at least
    /// </summary>
    public class BulkDeleteRepository
    {
        private readonly ILogger<BulkDeleteRepository> _logger;
        public int CancelledBlockedProcessingSeconds { get; private set; }
        public BulkDeleteRepository(ILogger<BulkDeleteRepository> logger)
        {
            _logger = logger;
            CancelledBlockedProcessingSeconds = 30;
        }

        public void SetCancellationExpiredSeconds(int seconds)
        {
            CancelledBlockedProcessingSeconds = seconds;
        }

        public async Task CleanExpiredDataAsync<TContext, TEntity>(IDbContextFactory<TContext> contextFactory,
            Expression<Func<TEntity, bool>> expression, CancellationToken token = default, int pagesize = 100000, int maxpages = 0)
            where TEntity : class
            where TContext : DbContext
        {
            using (var dbc = await contextFactory.CreateDbContextAsync(token))
            {
                await CleanExpiredDataAsync(dbc, expression, token, pagesize, maxpages);
            }
        }
        public async Task CleanExpiredDataAsync<TEntity>(DbContext dbc,
            Expression<Func<TEntity, bool>> expression, CancellationToken token = default, int pagesize = 100000, int maxpages = 0)
            where TEntity : class
        {
            //var dt = DateTime.Now;

            int pages = 1;
            int pageSize = pagesize;
            var ts = new CancellationTokenSource(CancelledBlockedProcessingSeconds * 1000);
            var query = dbc.Set<TEntity>().Where(expression).Take(pageSize);

            ts.Cancel(false);
            try
            {
                while (query.Any())
                {
                    //var dts = DateTime.Now;

                    token.ThrowIfCancellationRequested();
                    await query.ExecuteDeleteAsync(ts.Token);

                    //Console.WriteLine($"Sub cose in Rond #{pages} - {DateTime.Now.Subtract(dts).TotalSeconds} seconds");

                    pages++;
                    if (maxpages > 0 && pages > maxpages) break;
                    query = dbc.Set<TEntity>().Where(expression).Take(pageSize);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to remove {typeof(TEntity).Name} entity from data base in round {pages}, {pagesize} records per round.");
            }

            //Console.WriteLine($"Total cose - {DateTime.Now.Subtract(dt).TotalSeconds} seconds");
        }
    }
}
