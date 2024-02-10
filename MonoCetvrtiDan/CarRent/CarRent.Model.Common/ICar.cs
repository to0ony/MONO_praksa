using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Model.Common
{
    public interface ICar
    {
        Guid Id { get; set; }
        string Brand { get; set; }
        string Model { get; set; }
        int? Mileage { get; set; }
        bool InsuranceStatus { get; set; }
        bool Available { get; set; }
        int? ManafactureDate { get; set; }
    }
}
