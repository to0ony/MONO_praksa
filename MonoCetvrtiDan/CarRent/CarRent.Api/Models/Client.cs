using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRent.Api.Models
{
    public class Client
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool AcquiredCar { get; set; }
        public int ContractDuration { get; set; }
    }
}