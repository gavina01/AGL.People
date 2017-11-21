namespace AGL.People.Services.Tests
{
    using AGL.People.Models;
    using AGL.People.Models.Configuration;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class PeopleServiceTest
    {
        private Settings _mockSettings;
        private Mock<IPeopleRepository> _mockPeopleRepository;

        public PeopleServiceTest()
        {
            _mockSettings = new Settings();
            _mockPeopleRepository = new Mock<IPeopleRepository>();
        }

        [Fact(DisplayName = "Can_Get_ListOfPersonsWithPetByTypeCat")]
        public void Can_Get_ListOfPersonsWithPetByTypeCat()
        {
            // Arange

            List<Pet> listOfPets_test = GetDefaultListOfPets();

            List<Person> listOfPerson = GetDefaultListOfPeople(listOfPets_test);

            _mockPeopleRepository.Setup(m => m.GetListAsync()).Returns(Task.FromResult(listOfPerson));

            // Act
            var service = new PeopleService(_mockSettings, _mockPeopleRepository.Object);
            var persons = service.GetListGroupByAsync(PetTypeEnum.Cat).GetAwaiter().GetResult();

            //// Assert
            Assert.NotNull(persons);
            // all genders have cats
            Assert.Equal(persons.Items.Count, 3);
        }

        [Fact(DisplayName = "Can_Get_ListOfPersonsWithPetIsAlphabeticalLists")]
        public void Can_Get_ListOfPersonsWithPetIsAlphabeticalLists()
        {
            // Arange
            List<Pet> listOfPets_test = GetDefaultListOfPets();

            List<Person> listOfPerson = GetDefaultListOfPeople(listOfPets_test);

            _mockPeopleRepository.Setup(m => m.GetListAsync()).Returns(Task.FromResult(listOfPerson));

            // Act
            var service = new PeopleService(_mockSettings, _mockPeopleRepository.Object);
            var persons = service.GetListGroupByAsync(PetTypeEnum.Cat).GetAwaiter().GetResult();

            //// Assert
            Assert.NotNull(persons);
            // alphabetical gender
            Assert.Equal(persons.Items[0].Gender, "Female");

            // alphabetical petnames
            Assert.Equal(persons.Items[0].PetNames[0], "Cat");
        }

        #region private
        private static List<Person> GetDefaultListOfPeople(List<Pet> listOfPets_test)
        {
            return new List<Person>()
            {
                new Person(){Name = "test_male_2",Age = 20,Gender =GenderTypeEnum.Male.ToString(),Pets = listOfPets_test},
                new Person(){Name = "test_other_3",Age = 20,Gender =GenderTypeEnum.Other.ToString(),Pets = listOfPets_test},
                new Person(){Name = "test_female_1",Age = 20,Gender =GenderTypeEnum.Female.ToString(),Pets = listOfPets_test}
            };
        }

        private static List<Pet> GetDefaultListOfPets()
        {
            return new List<Pet>()
            {
                new Pet(){Name = "Dog", Type = PetTypeEnum.Dog},
                new Pet(){Name = "Cat", Type = PetTypeEnum.Cat},
                new Pet(){Name = "Fish", Type = PetTypeEnum.Fish}
            };
        }
        #endregion
    }
}