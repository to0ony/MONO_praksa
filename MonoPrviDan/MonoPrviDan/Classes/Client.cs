namespace MonoPrviDan.Classes
{
    internal class Client
    {
        //entities
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool AcquiredCar { get; set; }
        public int ContractDuration { get; set; }

        //constructor
        public Client(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}

