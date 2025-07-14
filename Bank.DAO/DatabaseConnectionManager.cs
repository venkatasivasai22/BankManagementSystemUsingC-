using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.DAO
{
    public sealed class DatabaseConnectionManager
    {
        private static readonly Lazy<DatabaseConnectionManager> _instance = 
            new Lazy<DatabaseConnectionManager>(() => new DatabaseConnectionManager());
        
        private readonly string _connectionString;
        private readonly SemaphoreSlim _connectionSemaphore;
        private readonly object _lockObject = new object();

        private DatabaseConnectionManager()
        {
            _connectionString = "Server=INHYNVMUSAM01;Database=BankManagement;Trusted_Connection=True;";
            _connectionSemaphore = new SemaphoreSlim(10, 10); // Max 10 concurrent connections
        }

        public static DatabaseConnectionManager Instance => _instance.Value;

        public async Task<SqlConnection> GetConnectionAsync()
        {
            await _connectionSemaphore.WaitAsync().ConfigureAwait(false);
            
            try
            {
                var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync().ConfigureAwait(false);
                return connection;
            }
            catch
            {
                _connectionSemaphore.Release();
                throw;
            }
        }

        public void ReleaseConnection()
        {
            _connectionSemaphore.Release();
        }
    }
}