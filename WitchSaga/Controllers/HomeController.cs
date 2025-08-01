using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WitchSaga.Models;
using WitchSaga.ServicesContract;

namespace WitchSaga.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly IFibonacciCalculatorService _fibonacciCalculatorService;

        #endregion

        #region Constructor

        public HomeController(
            IFibonacciCalculatorService fibonacciCalculatorService)
        {
            _fibonacciCalculatorService = fibonacciCalculatorService;
        }

        #endregion

        #region Public Methods

        public IActionResult Index()
        {
            var model = new VillagerModel();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CalculateKill(List<VillagerModel> villagers)
        {
            if (!villagers.Any())
            {
                return this.Json(new
                {
                    IsSuccess = false,
                    ErrorMessage = "Empty Request",
                });
            }

            if (villagers.Any(item => item.YearOfDeath <= 0 
                || item.AgeOfDeath <= 0 
                || item.YearOfDeath - item.AgeOfDeath <= 0))
            {
                return this.Json(new
                {
                    IsSuccess = true,
                    Value = "-1",
                });
            }

            var killResult = string.Empty;
            var countTotalKill = 0;
            var resultFormat = "Person {0} born on Year {1}, number of people killed on year {2} is {3}. ";

            foreach (var villager in villagers) {
                var selectedYear = villager.YearOfDeath - villager.AgeOfDeath;
                var totalKilledPerson = selectedYear > 0 ? this.TotalKilledPersonInYear(selectedYear) : -1;

                countTotalKill += totalKilledPerson;
                killResult += string.Format(resultFormat, villager.Name, selectedYear, selectedYear, totalKilledPerson);
            }

            var averageResult = villagers.Count > 1 
                ? string.Format("So the average is {0}", (double)countTotalKill/villagers.Count) 
                : string.Empty;

            return this.Json(new
            {
                IsSuccess = true,
                Value = string.Format("{0}{1}", killResult, averageResult),
            });
        }

        #endregion

        #region Private Methods

        private int TotalKilledPersonInYear(int selectedYear)
        {
            if (selectedYear <= 0)
            {
                return -1;
            }

            return this._fibonacciCalculatorService.GetTotalFibonacciKill(selectedYear);
        }

        #endregion
    }
}
