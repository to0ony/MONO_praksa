namespace MonoPrviDan.Classes
{
    internal class Menu
    {
        public void printMenu()
        {
            Console.WriteLine("----- Glavni izbornik -----");
            Console.WriteLine("1. Dodaj auto");
            Console.WriteLine("2. Dodaj klijenta");
            Console.WriteLine("3. Prikaz automobila");
            Console.WriteLine("4. Prikaz klijenata");
            Console.WriteLine("5. Prikaz ugovora");
            Console.WriteLine("6. Sklopi ugovor");
            Console.WriteLine("7. Raskini ugovor");
            Console.WriteLine("8. Izlaz");
            Console.Write("Odaberi opciju (1-7): ");
        }
    }
}
