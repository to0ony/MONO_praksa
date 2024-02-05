namespace MonoPrviDan.Classes
{
    public class Car
    {
        //entities
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ManafactureDate { get; set; }
        public int Mileage { get; set; }
        public bool InsuranceStatus { get; set; }
        public bool Available { get; set; }

        //constructor
        public Car(string brand, string model) => (Brand, Model) = (brand, model);
        public Car(string brand, string model, int manafactureDate, int mileage, bool insuranceStatus, bool available)
        {
            Brand = brand;
            Model = model;
            ManafactureDate = manafactureDate;
            Mileage = mileage;
            InsuranceStatus = insuranceStatus;
            Available = available;
        }

        //methods
        public void showDetails()
        {
            Console.WriteLine("Brand: " + Brand);
            Console.WriteLine("Model: " + Model);
            Console.WriteLine("Date of manafacture: " + ManafactureDate);
            Console.WriteLine("Mileage: " + Mileage + "km");
            Console.WriteLine("Insured: " + InsuranceStatus);
            Console.WriteLine("Available: " + Available);
        }
        public void setMileage(int mileage) => Mileage = mileage;
    } 
}
