namespace AGL.People.Services.Tests
{
    using AGL.People.Models;
    using AGL.People.Models.Configuration;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class PeopleRepositoryTest
    {
        private Settings _mockSettings;

        public PeopleRepositoryTest()
        {
            _mockSettings = new Settings()
            {
                AglSettings = new AglSettings() { PersonAPIEndPoint = "http://test" },
                Version = "test_version"
            };
        }

        [Fact(DisplayName = "Can_Get_ListOfPersonsWithHttpClient_WithOK")]
        public void Can_Get_ListOfPersonsWithHttpClient_WithOK()
        {
            // Arange
            string testContent = JsonConvert.SerializeObject(GetDefaultListOfPeople());
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            // Act
            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(testContent)
                }));

            var service = new PeopleRepository(_mockSettings, new HttpClient(mockMessageHandler.Object));
            var persons = service.GetListAsync().GetAwaiter().GetResult();

            // Assert
            Assert.NotNull(persons);
            // Returns 3 Persons
            Assert.Equal(persons.Count, 3);
        }

        [Fact(DisplayName = "Can_Get_ListOfPersonsWithHttpClient_WithError")]
        public void Can_Get_ListOfPersonsWithHttpClient_WithError()
        {
            // Arange
            string testContent = JsonConvert.SerializeObject(GetDefaultListOfPeople());
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            // Act
            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(testContent)
                }));

            var service = new PeopleRepository(_mockSettings, new HttpClient(mockMessageHandler.Object));
            var persons = service.GetListAsync().GetAwaiter().GetResult();

            // Assert
            Assert.NotNull(persons);
            // Returns 3 Persons
            Assert.Equal(persons.Count, 0);
        }

        #region private

        private static List<Person> GetDefaultListOfPeople()
        {
            List<Pet> listOfPets_test = GetDefaultListOfPets();

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

        #endregion private
    }
}