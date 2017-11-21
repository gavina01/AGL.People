namespace AGL.People.Extensions
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Allow for extention on IConfiguration
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Extend IConfiguration to bind section.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this IConfiguration config, string key) where T : new()
        {
            var instance = new T();
            config.GetSection(key).Bind(instance);
            return instance;
        }
    }
}