namespace AGL.People.Services
{
    using AGL.People.Models;
    using AGL.People.Models.Configuration;
    using AGL.People.Services.Extensions;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// People Respository
    /// </summary>
    public class PeopleRepository : IPeopleRepository
    {
        private readonly Settings _settings;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="httpClient"></param>
        public PeopleRepository(Settings settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Return List of Pets By Pet Type
        /// </summary>
        /// <param name="petTypeEnum"></param>
        /// <returns></returns>
        public async Task<List<Person>> GetListAsync()
        {
            var requestUri = this._settings.AglSettings.PersonAPIEndPoint;
            var people = new List<Person>();

            // Call EndPoint to return list of People with owned pets
            using (var response = await _httpClient.GetAsync(requestUri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    // set serializer to ignore nulls
                    var settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;

                    people = await response.Content.ReadAsJsonAsync<List<Person>>(settings);
                }
            }
            return people;
        }
    }
}