namespace AGL.People.Models.Configuration
{
    using Newtonsoft.Json;

    /// <summary>
    /// Settings Defination from app.settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Client Specific Settings
        /// </summary>
        [JsonProperty("AglSettings")]
        public AglSettings AglSettings { get; set; }
        /// <summary>
        /// Build version
        /// </summary>
        public string Version { get; set; }
    }
}
