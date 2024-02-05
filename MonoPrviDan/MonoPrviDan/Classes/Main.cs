using MonoPrviDan.Classes;
using MonoPrviDan.Interfaces;
internal class Program
{
    private static void Main(string[] args)
    {
        Dictionary<int,Car> Cars = new Dictionary<int,Car>();
        Dictionary<int,Client> Clients = new Dictionary<int,Client>();
        Dictionary<int, IContract> Contracts = new Dictionary<int, IContract>();
        int CarIDCounter = 0;
        int ClientIDCounter = 0;
        int ContractIDCounter = 0;
        Menu menu = new Menu();

    while(true) {
            menu.printMenu();
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int option))
            {
                switch(option)
                {
                    //New car DONE
                    case 1:
                        Console.Write("Unesite marku automobila: ");
                        string brand = Console.ReadLine();
                        Console.Write("Unesite model automobila: ");
                        string model = Console.ReadLine();
                        Console.Write("Unesite godinu proizvodnje: ");
                        int manufactureDate = int.Parse(Console.ReadLine());
                        Console.Write("Unesite kilometražu: ");
                        int mileage = int.Parse(Console.ReadLine());
                        Console.Write("Je li automobil osiguran (true/false): ");
                        bool insuranceStatus = bool.Parse(Console.ReadLine());
                        Console.Write("Je li automobil dostupan (true/false): ");
                        bool available = bool.Parse(Console.ReadLine());
                        
                        CarIDCounter += 1;
                        Car newCar = new Car(brand, model, manufactureDate, mileage, insuranceStatus, available);
                        Cars.Add(CarIDCounter,newCar);
                        break;
                    
                    //New client DONE
                    case 2:
                        Console.WriteLine("Unesi ime klijenta: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Unesi prezime klijenta: ");
                        string surname = Console.ReadLine();
                        Client newClient = new Client(name, surname);
                        
                        ClientIDCounter += 1;
                        Clients.Add(ClientIDCounter,newClient);
                        break;

                    //Cars showcase DONE
                    case 3:
                        if (Cars.Count > 0)
                        {
                            foreach (var kvp in Cars)
                            {
                                int carId = kvp.Key;
                                Car car = kvp.Value;
                                Console.WriteLine($"Car ID: {carId}");
                                car.showDetails();
                                Console.WriteLine("---------------");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nema automobila u bazi!");
                        }
                            break;
                    
                    //Clients showcase DONE
                    case 4:
                        if (Clients.Count > 0)
                        {
                            foreach (var kvp in Clients)
                            {
                                int clientId = kvp.Key;
                                Client client = kvp.Value;
                                Console.WriteLine($"{clientId}: Ime: {client.Name}, Prezime: {client.Surname}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nema klijenata u bazi!");
                        }
                            break;

                    // Contracts showcase DONE
                    case 5:
                        if (Contracts.Count == 0)
                        {
                            Console.WriteLine("Nema dostupnih ugovora.");
                        }
                        else
                        {
                            Console.WriteLine("Prikaz svih ugovora:");
                            foreach (var contractKvp in Contracts)
                            {
                                int contractId = contractKvp.Key;
                                IContract contract = contractKvp.Value;

                                Console.WriteLine($"ID ugovora: {contractId}");
                                contract.DisplayContractDetails();
                                Console.WriteLine(); // Razmak između ugovora
                            }
                        }
                        break;

                    // Contract maker DONE
                    case 6:
                        // Prikaz dostupnih automobila
                        Console.WriteLine("Dostupna vozila:");
                        foreach (var kvp in Cars)
                        {
                            int carId = kvp.Key;
                            Car car = kvp.Value;

                            if (car.Available)
                            {
                                Console.WriteLine($"{carId}: Marka: {car.Brand}, Model: {car.Model}");
                            }
                        }

                        Console.Write("Odaberite ID vozila za najam: ");
                        int selectedCarId = int.Parse(Console.ReadLine());

                        // Dohvati vozilo na temelju odabranog ID-a
                        if (Cars.TryGetValue(selectedCarId, out Car selectedCar) && selectedCar.Available)
                        {
                            // Prikaz dostupnih klijenata
                            Console.WriteLine("Dostupni klijenti:");
                            foreach (var clientKvp in Clients)
                            {
                                int clientId = clientKvp.Key;
                                Client client = clientKvp.Value;

                                Console.WriteLine($"{clientId}: Ime: {client.Name}, Prezime: {client.Surname}");
                            }

                            Console.Write("Odaberite ID klijenta za najam: ");
                            int selectedClientId = int.Parse(Console.ReadLine());

                            // Dohvati klijenta na temelju odabranog ID-a
                            if (Clients.TryGetValue(selectedClientId, out Client selectedClient))
                            {
                                Console.Write("Unesite datum početka najma (mm/dd/yyyy): ");
                                DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);

                                Console.Write("Unesite datum završetka najma (mm/dd/yyyy): ");
                                DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);

                                Console.WriteLine("Odaberite vrstu ugovora:");
                                Console.WriteLine("1. Basic ugovor");
                                Console.WriteLine("2. Premium ugovor");
                                Console.Write("Vaš odabir: ");
                                int contractTypeChoice = int.Parse(Console.ReadLine());

                                IContract rentalContract;
                                switch (contractTypeChoice)
                                {
                                    case 1:
                                        rentalContract = new BasicContract();
                                        break;
                                    case 2:
                                        rentalContract = new PremiumContract();
                                        break;
                                    default:
                                        Console.WriteLine("Neispravan odabir.");
                                        return;
                                }

                                rentalContract.StartDate = startDate;
                                rentalContract.EndDate = endDate;
                                rentalContract.CalculateTotalCost();

                                // Postavljanje odabranih vozila i klijenta u ugovor
                                rentalContract.CarID = selectedCarId;
                                rentalContract.ClientID = selectedClientId;

                                // Dodavanje ugovora u Dictionary
                                ContractIDCounter += 1;
                                int contractId = ContractIDCounter;
                                Contracts.Add(contractId, rentalContract);
                                selectedCar.Available = false;
                                Console.WriteLine("Ugovor je uspješno kreiran i spremljen.");
                            }
                            else
                            {
                                Console.WriteLine($"Klijent s ID-om {selectedClientId} nije pronađen.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Vozilo s ID-om {selectedCarId} nije pronađeno ili nije dostupno.");
                        }
                        break;

                    // Contract remover DONE
                    case 7:
                        if (Contracts.Count == 0)
                        {
                            Console.WriteLine("Nema dostupnih ugovora za raskidanje.");
                            break;
                        }

                        Console.WriteLine("Prikaz svih ugovora:");
                        foreach (var contractKvp in Contracts)
                        {
                            int contractId = contractKvp.Key;
                            IContract contract = contractKvp.Value;

                            Console.WriteLine($"ID ugovora: {contractId}");
                            contract.DisplayContractDetails();
                            Console.WriteLine(); // Razmak između ugovora
                        }

                        Console.Write("Unesite ID ugovora koji želite raskinuti: ");
                        int contractIdToDelete = int.Parse(Console.ReadLine());

                        if (Contracts.TryGetValue(contractIdToDelete, out IContract contractToDelete))
                        {
                            Console.Write("Unesi novu kilometražu nakon vraćanja automobila: ");
                            int newMileage = int.Parse(Console.ReadLine());

                            // Pronađi povezani automobil prema CarID ugovora
                            if (Cars.TryGetValue(contractToDelete.CarID, out Car associatedCar))
                            {
                                // Ažuriraj kilometražu automobila i dostupnost vozila
                                associatedCar.Mileage = newMileage;
                                Console.WriteLine($"Kilometraža automobila je uspješno ažurirana na {newMileage} km.");
                                associatedCar.Available = true;

                                Contracts.Remove(contractIdToDelete);
                                Console.WriteLine($"Ugovor s ID-om {contractIdToDelete} je uspješno raskinut.");
                            }
                            else
                            {
                                Console.WriteLine($"Automobil s ID-om {contractToDelete.CarID} nije pronađen.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Ugovor s ID-om {contractIdToDelete} nije pronađen.");
                        }
                        break;

                    // Exit
                    case 8:
                        Console.WriteLine("Izasli ste iz aplikacije");
                        return;

                    default:
                        Console.WriteLine("Neispravan unos. Molimo odaberite opciju od 1 do 9.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Neispravan unos!");
            }
            Console.WriteLine("Pritisnite Enter za nastavak...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}