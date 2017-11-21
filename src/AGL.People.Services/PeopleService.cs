namespace AGL.People.Services
{
    using AGL.People.Models;
    using AGL.People.Models.Configuration;
    using AGL.People.Models.ViewModel;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// People Service
    /// </summary>
    public class PeopleService : IPeopleService
    {
        /// <summary>
        /// Setting dependencies
        /// </summary>
        private readonly Settings _settings;
        /// <summary>
        /// People repository
        /// </summary>
        private readonly IPeopleRepository _peopleRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="httpClient"></param>
        public PeopleService(Settings settings, IPeopleRepository peopleRepository)
        {
            _settings = settings;
            _peopleRepository = peopleRepository;
        }

        /// <summary>
        /// List of Owner Gender with Pet Names By Type
        /// </summary>
        /// <param name="petTypeEnum"></param>
        /// <returns></returns>
        public async Task<SummaryByPetTypeVM> GetListGroupByAsync(PetTypeEnum petTypeEnum)
        {
            try
            {
                // return list of person data with all pets
                var people = await _peopleRepository.GetListAsync();

                // return group by gender and list pet names alphabetically
                var petSummary = people.Where(x => x.Pets.Any(q => q.Type == petTypeEnum))
                    .OrderBy(x => x.Gender)
                    .Select(x => new
                    {
                        Gender = x.Gender,
                        Pets = x.Pets.Where(y => y.Type == petTypeEnum)
                    })
                     .GroupBy(x => x.Gender, x => x.Pets,
                        (key, group) => new
                        {
                            Gender = key,
                            Pets = group.SelectMany(q => q)
                        }
                     )
                     .Select(x => new SummaryItemVM()
                     {
                         Gender = x.Gender,
                         PetNames = x.Pets.OrderBy(q => q.Name).Select(q => q.Name).ToList()
                     }).ToList();

                return new SummaryByPetTypeVM() { Items = petSummary };
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}