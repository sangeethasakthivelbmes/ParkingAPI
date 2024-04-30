namespace Parking.WebAPI.CoreHelper.Extensions
{
    public static class StringExtensions
    {
        public static string Replaces(this string stringValue, Dictionary<string, string> oldAndNew)
        {
            oldAndNew.ToList().ForEach(obj =>
            {
                stringValue = stringValue.Replace(obj.Key, obj.Value);
            });
            return stringValue;
        }
    }
}

