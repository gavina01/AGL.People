namespace AGL.People.Web
{
    using AGL.People.Models;
    using AGL.People.Models.ViewModel;
    using AGL.People.Services;
    using AGL_People.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class PersonControllerTest
    {
        Mock<IPeopleService> _mockPeopleService;

        public PersonControllerTest()
        {
            _mockPeopleService = new Mock<IPeopleService>();
        }

        [Fact(DisplayName = "Can_Get_SummaryByPetType_GetList")]
        public void Can_Get_SummaryByPetType_GetList()
        {
            // Arange

            SummaryByPetTypeVM summary = new SummaryByPetTypeVM()
            {
                Items = new System.Collections.Generic.List<SummaryItemVM>()
                {
                    new SummaryItemVM(){Gender = "Female",PetNames = new List<string>(){"Cat"}},
                    new SummaryItemVM(){Gender = "Male",PetNames = new List<string>(){"Cat"} },
                }
            };

            _mockPeopleService.Setup(m => m.GetListGroupByAsync(It.IsAny<PetTypeEnum>())).Returns(Task.FromResult(summary));

            var controller = new PersonController(_mockPeopleService.Object);

            // Act

            var result = controller.GetSummaryByPetType();

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.GetAwaiter().GetResult());
            Assert.IsType<SummaryByPetTypeVM>(okResult.Value);
        }
    }
}
