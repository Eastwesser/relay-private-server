using System.Data;
using Npgsql;
using Microsoft.Extensions.Logging;

namespace Relay.DBUtility
{
    /// <summary>
    /// Класс для операций с базой данных PostgreSQL.
    /// </summary>
    public class DbCommon
    {
        private readonly string _connectionString;
        private readonly ILogger<DbCommon> _logger;

        public DbCommon(string connectionString, ILogger<DbCommon> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        /// <summary> Метод для создания подключения к базе данных. </summary>
        private IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        /// <summary> Получение данных по запросу. </summary>
        public async Task<object> GetDataAsync(string query)
        {
            await using var connection = CreateConnection() as NpgsqlConnection;
            if (connection is null) throw new InvalidOperationException("Ошибка при создании подключения.");

            await connection.OpenAsync();
            await using var command = new NpgsqlCommand(query, connection);
            var result = await command.ExecuteReaderAsync();
            return result;
        }

        /// <summary> Метод для обновления данных в БД. </summary>
        public Task UpdateDataAsync(object data)
        {
            throw new NotImplementedException();
        }
    }
}
