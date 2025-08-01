using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WitchSaga.Controllers;
using WitchSaga.Models;
using WitchSaga.ServicesContract;

namespace WitchSaga.Test
{
    public class HomeControllerTest
    {
        #region Fields

        private Mock<IFibonacciCalculatorService> _mockFibonacciCalculatorService;
        private HomeController _controller;

        #endregion

        #region Constructor

        public HomeControllerTest()
        {
            this._mockFibonacciCalculatorService = new Mock<IFibonacciCalculatorService>();
            this._controller = new HomeController(_mockFibonacciCalculatorService.Object);
        }

        #endregion

        #region Test Method

        [Fact]
        public void CalculateKill_IncorrectParameter_ReturnDefaultValue()
        {
            var villagerList = new List<VillagerModel>
            {
                new VillagerModel
                {
                    Name = "name",
                    AgeOfDeath = -1,
                    YearOfDeath = -1
                }
            };

            var result = this._controller.CalculateKill(villagerList);

            var jsonResult = Assert.IsType<JsonResult>(result);
            string expectedJson = JsonSerializer.Serialize(new { IsSuccess = true, Value = "-1" });
            string actualJson = JsonSerializer.Serialize(jsonResult.Value);

            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void CalculateKill_CorrectParameter_ReturnCorrectValue()
        {
            var villagerList = new List<VillagerModel>
            {
                new VillagerModel
                {
                    Name = "A",
                    AgeOfDeath = 3,
                    YearOfDeath = 5
                }
            };

            this._mockFibonacciCalculatorService
                .Setup(item => item.GetTotalFibonacciKill(It.IsAny<int>()))
                .Returns(2);

            var result = this._controller.CalculateKill(villagerList);

            var jsonResult = Assert.IsType<JsonResult>(result);
            string expectedJson = JsonSerializer.Serialize(new { IsSuccess = true, Value = "Person A born on Year 2, number of people killed on year 2 is 2. " });
            string actualJson = JsonSerializer.Serialize(jsonResult.Value);

            Assert.Equal(expectedJson, actualJson);
        }

        #endregion
    }
}