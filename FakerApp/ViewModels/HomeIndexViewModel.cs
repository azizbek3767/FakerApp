using FakerApp.Models;
using System.Collections.Generic;

namespace FakerApp.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<FakeUser> FakeUsers { get; set; }
        public string CountryOne { get; set; }
        public string CountryTwo { get; set; }
        public string CountryThree { get; set; }
        public int NumberOfResults { get; set; }
        public int LoadNumber { get; set; }
        public int ErrorQuantity { get; set; }

    }
}
