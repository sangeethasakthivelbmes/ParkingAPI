namespace Parking.WebAPI.CoreHelper.Extensions
{
    public static class HttpRequestExtensions
    {
        public static T GetHttpContextItem<T>(this HttpRequest httpRequest, string key)
        {
            var context = httpRequest.HttpContext;
            if (context?.Items.ContainsKey(key) == true)
            {
                return (T)context.Items[key];
            }

            return default(T);
        }
    }
}

