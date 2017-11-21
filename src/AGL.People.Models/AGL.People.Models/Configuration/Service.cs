namespace AGL.People.Models.Configuration
{
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// DI Service Definition
    /// </summary>
    public class Service
    {
        /// <summary>
        /// interface
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// implementation
        /// </summary>
        public string ImplementationType { get; set; }

        /// <summary>
        /// lifetime
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ServiceLifetime Lifetime { get; set; }
    }
}
