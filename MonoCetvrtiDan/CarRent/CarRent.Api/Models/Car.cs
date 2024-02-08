using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRent.Api.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ManafactureDate { get; set; }
        public int Mileage { get; set; }
        public bool InsuranceStatus { get; set; }
        public bool Available { get; set; }
    }
}