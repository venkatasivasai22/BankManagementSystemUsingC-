using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.DAO
{
    public class TransactionManager
    {
        private readonly SemaphoreSlim _transactionSemaphore = new SemaphoreSlim(1, 1);
        private readonly object _lockObject = new object();

        public async Task<T> ExecuteInTransactionAsync<T>(Func<SqlConnection, SqlTransaction, Task<T>> operation)
        {
            await _transactionSemaphore.WaitAsync().ConfigureAwait(false);
            
            try
            {
                using (var connection = await DatabaseConnectionManager.Instance.GetConnectionAsync().ConfigureAwait(false))
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var result = await operation(connection, transaction).ConfigureAwait(false);
                            transaction.Commit();
                            return result;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                        finally
                        {
                            DatabaseConnectionManager.Instance.ReleaseConnection();
                        }
                    }
                }
            }
            finally
            {
                _transactionSemaphore.Release();
            }
        }
    }
}