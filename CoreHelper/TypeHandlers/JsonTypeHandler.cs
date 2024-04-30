using System.Data;
using System.Text.Json;
using Dapper;

namespace Parking.WebAPI.CoreHelper.TypeHandlers
{
    public class JsonTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        public override T Parse(object value)
        {
            if (value is DBNull || value == null)
                return default;

            // Deserialize the JSON string to the desired type
            return JsonSerializer.Deserialize<T>((string)value);
        }

        public override void SetValue(IDbDataParameter parameter, T value)
        {
            // Serialize the object to JSON and set it as the parameter value
            parameter.Value = JsonSerializer.Serialize(value);
        }
    }
}

