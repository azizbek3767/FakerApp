using Bogus;
using FakerApp.Models;
using FakerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FakerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(HomeIndexViewModel viewModel)
        {
            var viewModelSend = new HomeIndexViewModel();
            var LoadNumber = Convert.ToInt32(viewModel.NumberOfResults) + Convert.ToInt32(viewModel.LoadNumber);
            if(Convert.ToInt32(viewModel.NumberOfResults) == 0)
            {
                LoadNumber = 20;                
            }
                        

            if(viewModel.CountryOne != null)
            {
                var languages = new string[] { viewModel.CountryOne, viewModel.CountryTwo, viewModel.CountryThree };
                var fakeList = SeedFakeUsers(LoadNumber, languages);
                viewModelSend.FakeUsers = fakeList;
            }

            return View(viewModelSend);
        }

        /*[HttpPost]
        public IActionResult Load()
        {

        }*/
        public List<FakeUser> SeedFakeUsers(int number, string[] languagesParam)
        {
            Randomizer.Seed = new Random(123);
            var fakeList = new List<FakeUser>();
            var counter = 0;
            var fakeUserIds = 1;
            var faker = new Faker();
            var languages = languagesParam;
            while (true)
            {
                var fakeUserGenerator = new Faker<FakeUser>(faker.PickRandom(languages))
                .RuleFor(x => x.Id, x => fakeUserIds++)
                .RuleFor(x => x.Identifier, Guid.NewGuid)
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Country, x => x.Address.Country())
                .RuleFor(x => x.City, x => x.Address.City())
                .RuleFor(x => x.Street, x => x.Address.StreetName())
                .RuleFor(x => x.Phone, x => x.Person.Phone)
                .RuleFor(x => x.House, x => x.Address.BuildingNumber());

                fakeList.Add(fakeUserGenerator.Generate());
                counter++;
                if(counter == number)
                {
                    break;
                }
            }

           /* foreach (var data in fakeUserGenerator.GenerateForever())
            {
                fakeList.Add(new FakeUser
                {
                    Id = data.Id,
                    Identifier = data.Identifier,
                    Name = data.Name,
                    Country = data.Country,
                    City = data.City,
                    Street = data.Street,
                    Phone = data.Phone,
                    House = data.House
                });
            }*/
            return fakeList;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
