using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Common
{
    public static class Connection
    {
        public const string ConnectionString = "host=localhost ;port=5432 ;Database=CarRent ;User ID=postgres ;Password=postgres";
    }

    public class CarFilter
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? ManafactureDate { get; set; }
        public int? Mileage { get; set; }
        public bool? InsuranceStatus { get; set; }
        public bool? Available { get; set; }
    }
}
