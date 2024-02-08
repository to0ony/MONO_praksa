using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRent.Api.Models
{
    public class CarView
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ManafactureDate { get; set; }
        public CarView(Car carView)
        {
            Brand = carView.Brand;
            Model = carView.Model;
            ManafactureDate = carView.ManafactureDate;
        }
    }
}