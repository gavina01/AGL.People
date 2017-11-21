namespace AGL_People.Controllers
{
    using AGL.People.Extensions.Results;
    using AGL.People.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    /// <summary>
    /// Person Controller for Summary Details
    /// </summary>
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        // people service
        private readonly IPeopleService _peopleService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="peopleService"></param>
        public PersonController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        /// <summary>
        /// Return List of People Gender with Summary of Pet Names by type of pet
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSummaryByPetType()
        {
            try
            {
                var summary = await _peopleService.GetListGroupByAsync(AGL.People.Models.PetTypeEnum.Cat);
                return Ok(summary);
            }
            catch (System.Exception ex)
            {
                return new InternalServerActionResult(ex);
            }
        }
    }
}