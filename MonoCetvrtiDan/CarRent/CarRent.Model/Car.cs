using CarRent.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Model
{
    public class Car : ICar
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? Mileage { get; set ; }
        public bool InsuranceStatus { get; set; }
        public bool Available { get; set; }
        public int? ManafactureDate { get; set  ; }
    }
}
