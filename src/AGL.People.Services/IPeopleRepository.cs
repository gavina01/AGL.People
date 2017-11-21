namespace AGL.People.Services
{
    using AGL.People.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Person Repository
    /// </summary>
    public interface IPeopleRepository
    {
        /// <summary>
        /// Return List of Persons with Pets
        /// </summary>
        /// <returns></returns>
        Task<List<Person>> GetListAsync();
    }
}