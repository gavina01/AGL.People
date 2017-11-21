namespace AGL.People.Services
{
    using AGL.People.Models;
    using AGL.People.Models.ViewModel;
    using System.Threading.Tasks;

    /// <summary>
    /// People Service
    /// </summary>
    public interface IPeopleService
    {
        /// <summary>
        /// Get List of Pet Types By Gender Summary
        /// </summary>
        /// <param name="petTypeEnum"></param>
        /// <returns></returns>
        Task<SummaryByPetTypeVM> GetListGroupByAsync(PetTypeEnum petTypeEnum);
    }
}