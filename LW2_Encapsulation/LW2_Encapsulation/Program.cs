using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.ConstrainedExecution;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace LW2_Encapsulation
{
    class Car
    {
        // оголошення приватних полів
        private string make; // марка машини
        private string model; // модель машини
        private int year; // рік виготовлення
        private int odometr; // пробіг
        private int fuelLevel; // рівень палива (у відсотках)

        // оголошення публічних властивостей
        public string Make // властивість марки (читання та запис)
        {
            get { return make; }
            set { make = value; }
        }

        public string Model // властивість моделі (читання та запис)
        {
            get { return model; }
            set { model = value; }
        }

        public int Year // властивість року випуску (читання та запис)
        {
            get { return year; }
            set { year = value; }
        }

        public int Odometr // властивість пробігу (тільки запис)
        {
            set { year = value; }
        }

        public int FuelLevel // властивість рівню палива (читання та запис із перевіркою, щоб значення було від 0 до 100)
        {
            get { return fuelLevel; }
            set { if (value >= 0 && value <= 100)
                    fuelLevel = value; }
        }

        public Car() // конструктор за замовчуванням
        {
        }

        public Car(string make, string model, int year) // параметричний конструктор
        {
            Make = make;
            Model = model;
            Year = year;
        }

        public Car(Car car) // копійований конструктор
        {
            Make = car.Make;
            Model = car.Model;
            Year = car.Year;
        }

        public void Drive(int kilometers) // публічний метод із параметром, що реалізує рух автомобіля,
                                          //збільшуючи пробіг та зменшуючи рівень палива (у відсотках)
        {
            odometr += kilometers; // додавання пройдений шлях до пробігу
            if (kilometers >= 0) // перевірка, щоб шлях був не від'ємним числом
            {
                for (int i = 1; i <= kilometers/10; i++) // цикл, який буде ділити шлях до тих пір, поки число не буде нуль 
                {
                    fuelLevel -= 1; // рівень палива знижуеться на 1% (1 літр) кожні 10 км
                }
            }

            Console.WriteLine("\n Odometr: {0} km\n" +
                " Fuel level: {1}%", odometr, fuelLevel);
        }

        public void Refuel(int percentage) // публічний метод
        {
            double checkingFuelLevel;
            checkingFuelLevel = fuelLevel + percentage;

            if (checkingFuelLevel <= 100)
            {
                fuelLevel += percentage;
                PrintInfo();
            }
            else
            {
                Console.WriteLine(" Goes over the limit of the allowable number of liters of fuel.");
            }

        }

        public void Service()
        {
            odometr = 0;
        }

        public void PrintInfo()
        {
            Console.WriteLine("\n |Machine features|\n" +
                " ---------------------------------\n" +
                " Make: {0}\n" +
                " Model: {1}\n" +
                " Year: {2}\n" +
                " Odometr: {3} km\n" +
                " Fuel level: {4}%", make, model, year, odometr, fuelLevel);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int traveledKilometers;
            int refuelLevel;
            int percentageFuelLevel = 100;

            try
            {
                Console.WriteLine("\n Enter the make of the machine: " +
                "Tesla | Toyota | Ford | Honda | BMW");
                Console.Write(" Make: ");
                string make = Console.ReadLine();

                Console.WriteLine("\n Enter the model of the machine: " +
                    "Coupe | Sport Car | Sedan | Station Wagon | Minivan");
                Console.Write(" Model: ");
                string model = Console.ReadLine();

                Console.WriteLine("\n Enter the year of manufacture of the machine.");
                Console.Write(" Year: ");
                int year = Convert.ToInt32(Console.ReadLine());

                Car car = new Car(make, model, year);
                car.FuelLevel = percentageFuelLevel;
                car.PrintInfo();

                Console.Write(" ---------------------------------\n" +
                    " You have driven (km): ");
                traveledKilometers = Convert.ToInt32(Console.ReadLine());

                car.Drive(traveledKilometers);

                Console.Write(" ---------------------------------\n" +
                    " Trying to refuel (%): ");
                refuelLevel = Convert.ToInt32(Console.ReadLine());
                car.Refuel(refuelLevel);

                Console.Write(" ---------------------------------\n" +
                    " To be serviced.\n" +
                    " ---------------------------------\n");
                car.Service();
                car.PrintInfo();
            } 
            catch
            {
                Console.WriteLine("\n Enter data incorrectly! :3");
            }

            Console.ReadLine();
        }
    }
}
