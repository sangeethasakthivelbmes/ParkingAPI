using System.Data;

namespace Parking.WebAPI.CoreHelper.Dapper.Interfaces
{
    public interface IDapperWrapper
    {
        Task<int> ExecuteAsync(
            string query, object? parameters = null, CommandType commandType = CommandType.Text            
        );
        Task<T> QuerySingleAsync<T>(
            string query, object? parameters = null, CommandType commandType = CommandType.Text
            
        );
        Task<T> FirstAsync<T>(
            string query, object? parameters = null, CommandType commandType = CommandType.Text
        );
        Task<IEnumerable<T>> QueryAsync<T>(
            string query, object? parameters = null, CommandType commandType = CommandType.Text
        );
    }
}

