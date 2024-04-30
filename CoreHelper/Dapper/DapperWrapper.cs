using Npgsql;
using Dapper;
using System.Data;
using Parking.WebAPI.CoreHelper.Dapper.Interfaces;

namespace Parking.WebAPI.CoreHelper.Dapper
{
    public class DapperWrapper : IDapperWrapper
    {
        private readonly IConfiguration _config;

        public DapperWrapper(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> ExecuteAsync(string query, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            using var sqlConnection = new NpgsqlConnection(GetConnectionString());
            var rowsAffected = await sqlConnection.ExecuteAsync(query, parameters, commandType: commandType);
            return rowsAffected;
        }

        public async Task<T> QuerySingleAsync<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            using var sqlConnection = new NpgsqlConnection(GetConnectionString());
            var result = await sqlConnection.QuerySingleAsync<T>(query, parameters, commandType: commandType);
            return result;
        }

        public async Task<T> FirstAsync<T>(string query, object? parameters = null,  CommandType commandType = CommandType.Text)
        {
            using var sqlConnection = new NpgsqlConnection(GetConnectionString());
            var result = await sqlConnection.QueryFirstAsync<T>(query, parameters, commandType: commandType);
            return result;
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object? parameters = null,  CommandType commandType = CommandType.Text)
        {
            using var sqlConnection = new NpgsqlConnection(GetConnectionString());
            var result = await sqlConnection.QueryAsync<T>(query, parameters, commandType: commandType);
            return result;
        }
        private string GetConnectionString()
        {
            return new NpgsqlConnectionStringBuilder
            {
                Host = _config.GetConnectionString("Host"),
                Port = Convert.ToInt32(_config.GetConnectionString("Port")),
                Database =  _config.GetConnectionString("Database"),
                Username = _config.GetConnectionString("Username"),
                Password = _config.GetConnectionString("Password")
            }.ConnectionString;
        }
    }
}

