using MonoPrviDan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPrviDan.Classes
{
    public class PremiumContract : IContract
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalCost { get; private set; }

        public int ClientID { get; set; }
        public int CarID { get; set; }

        public void CalculateTotalCost()
        {
            double dailyCost = 18.0; 
            int numberOfDays = (int)(EndDate - StartDate).TotalDays;

            TotalCost = dailyCost * numberOfDays;
        }

        public void DisplayContractDetails()
        {
            Console.WriteLine("Year Contract Details:");
            Console.WriteLine($"Start Date: {StartDate}");
            Console.WriteLine($"End Date: {EndDate}");
            Console.WriteLine($"Total Cost: {TotalCost}");
            Console.WriteLine($"Client ID: {ClientID}");
            Console.WriteLine($"Car ID: {CarID}");
        }
    }
}
