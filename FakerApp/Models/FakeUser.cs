using System;

namespace FakerApp.Models
{
    public class FakeUser
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Phone { get; set; }
    }
}
