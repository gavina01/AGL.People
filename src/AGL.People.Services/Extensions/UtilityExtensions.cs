namespace AGL.People.Services.Extensions
{
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class UtilityExtensions
    {
        /// <summary>
        /// Replacement for ReadAsAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content, JsonSerializerSettings settings)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json, settings);
            return value;
        }
    }
}