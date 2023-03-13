namespace zhukobeep;

internal class Program
{
    class Car
    {
        private string Brand    { get; set; }
        private int Horses   { get; set; }
        private int YearMade { get; set; }

        public Car(string brand, int horses, int yearmade)
        {
            Brand    = brand;
            Horses   = horses;
            YearMade = yearmade;
        }

        public override string ToString() =>
            $"{Brand} {Horses} л.c, {YearMade} года сборки";
    }

    class PassengerCar : Car
    {
        private  int                     Passengers { get; set; }
        private Dictionary<string, int> _repairBook;
        
        public PassengerCar(string brand, int horses, int yearmade, int passengers) : base(brand, horses, yearmade)
        {
            Passengers  = passengers;
            _repairBook = new Dictionary<string, int>();
        }
        
        public void AddRepair(string part, int year)
        {
            _repairBook[part] = year;
        }
        
        public int GetRepairYear(string part) =>
            _repairBook.ContainsKey(part)
                ? _repairBook[part]
                : -1;
        
        public void PrintRepairBook()
        {
            if (_repairBook.Count == 0)
                Console.WriteLine("Замен не производилось.");
            else
            {
                Console.WriteLine("Ремонтная книжка: ");
                foreach (KeyValuePair<string, int> partyear in _repairBook)
                    Console.WriteLine($"\tЧасть {partyear.Key} заменялась в {partyear.Value} году;");
            }
        }
        
        public override string ToString() =>
            $"{base.ToString()}, вместительностью {Passengers} чел.";
        
    }

    class Truck : Car
    {
        private  int                     HowManyHotLoads { get; set; }
        private  string                     DriverName   { get; set; }
        private Dictionary<string, int> _cargo;

        public Truck(
            string brand,
            int horses,
            int    yearmade,
            int    howManyHotLoads,
            string driverName
        ) : base(brand, horses, yearmade)
        {
            HowManyHotLoads = howManyHotLoads;
            DriverName   = driverName;
            _cargo       = new Dictionary<string, int>();
        }
        
        public void SwitchDriver(string driverName)
        {
            DriverName = driverName;
        }

        public int AddHotLoads(string load, int wgt)
        {
            if (HowManyHotLoads < _cargo.Values.Sum() + wgt) return -1;
            if (_cargo.ContainsKey(load))
                _cargo[load] += wgt;
            else
                _cargo.Add(load, wgt);
            return 1;
        }

        public int WipeHotLoads(string load, int wgt)
        {
            if (!_cargo.ContainsKey(load)) return -1;
            if (_cargo[load] > wgt)
                return -1;
            if (_cargo[load] == wgt)
                _cargo.Remove(load);
            else
                _cargo[load] -= wgt;
            return 1;
        }

        public void PrintCargo()
        {
            if (_cargo.Count == 0)
                Console.WriteLine("Груза нет.");
            else
            {
                Console.WriteLine("Груз: ");
                foreach (KeyValuePair<string, int> loadwgt in _cargo)
                    Console.WriteLine($"\t{loadwgt.Key} весом {loadwgt.Value} ед.;");
            }
        }
        
        public override string ToString() =>
            $"{base.ToString()}, грузоподъёмность {HowManyHotLoads} ед., за рулём {DriverName}.";
    }

    class Autopark
    {
        private string                  Name     { get; set; }
        private Dictionary<string, Car> CarPark { get; set; }

        public Autopark(string name)
        {
            Name     = name;
            CarPark = new Dictionary<string, Car>();
        }

        public int AddCar(string id, Car car)
        {
            if (CarPark.ContainsKey(id)) return -1;
            CarPark[id] = car;
            return 1;
        }

        public int RemoveCar(string id)
        {
            if (!CarPark.ContainsKey(id)) return -1;
            CarPark.Remove(id);
            return 1;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"Автопарк \"{Name}\":");
            if (CarPark.Count == 0)
                sb.AppendLine("\tНет машин.");
            else
                foreach (KeyValuePair<string, Car> idcar in CarPark)
                    sb.AppendLine($"\tID: {idcar.Key}; Машина: {idcar.Value}");
            return sb.ToString();
        }
    }


    public static void Main(string[] args)
    {
        Autopark     ap = new Autopark("WASP");
        Car          c1 = new Car("Chevvy Vintage", 69420, 1337);
        Truck        c2 = new Truck("MAN", 5000, 2022, 10000, "Joe Biden");
        PassengerCar c3 = new PassengerCar("Volkswagen", 1400, 2003, 4);
        ap.AddCar("H148YO", c1);
        ap.AddCar("K228EK", c2);
        ap.AddCar("A777YE", c3);
        Console.Write($"Добро пожаловать в наш автопарк \"WASP\"!\n{ap}");
        //TODO: Интерактивность
//        int choice = 0;
//        while (true)
//        {
//            Console.Write("Вы даёте строку: ");
//            string? input = Console.ReadLine();
//            if (input == null)
//            {
//                Console.Write("\x1B[1F\x1B[2K");
//                continue;
//            }
//
//            input;
//
//            break;
//        }
    }
}
