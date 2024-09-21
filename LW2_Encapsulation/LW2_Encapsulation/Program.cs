using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
        private int fuelLevel; // рівень палива

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
            set { if (value >= 0)
                odometr = value; }
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
            for (int i = 1; i <= kilometers / 10; i++) // цикл, який буде ділити шлях до тих пір, поки число не буде нуль 
            {
                fuelLevel -= 1; // рівень палива знижуеться на 1% (1 літр) кожні 10 км
            }
            
            Console.WriteLine("\n Odometr: {0} km\n" +
                " Fuel level: {1}%", odometr, fuelLevel);        
        }

        public void Refuel(int percentage) // публічний метод із параметром, що реалізує підвищення рівня палива
        {
            double checkingFuelLevel;
            checkingFuelLevel = fuelLevel + percentage;

            switch (checkingFuelLevel <= 100) // перевірка на рівень палива, щоб не був більше 100
            {
                case 0 <= 100: 
                    fuelLevel += percentage; // до існуючого рівня палива додається певне число (%)
                    PrintInfo(); // метод, що виводить інформацію в консоль
                    break;

                default:
                    Console.WriteLine(" Goes over the limit of the allowable number of liters of fuel."); // повідомлення про помилку
                    break;
            }
        }

        public void Service() // метод без параметрів, що обнуляє значення 
        {
            odometr = 0;
        }

        public void PrintInfo() // метод без параметрів, що виводить інформацію в консоль
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
            int traveledKilometers; // кілометри, які були пройдені
            int refuelLevel; // кількість для відновлення рівня палива (у %)
            int percentageFuelLevel = 100; // рівень палива (у %)

            try
            {
                // перші три значення (марка, модель та рік виготовлення), які вводить користувач
                Console.WriteLine("\n Enter the make of the machine:\n " +
                    "[Tesla | Toyota | Ford | Honda | BMW]");
                Console.Write(" Make: ");
                string make = Console.ReadLine();

                Console.WriteLine("\n Enter the model of the machine:\n " +
                    "[Coupe | Sport Car | Sedan | Station Wagon | Minivan]");
                Console.Write(" Model: ");
                string model = Console.ReadLine();

                Console.WriteLine("\n Enter the year of manufacture of the machine.");
                Console.Write(" Year: ");
                int year = Convert.ToInt32(Console.ReadLine());

                // створення об'єкту (машини)
                Car car = new Car(make, model, year);
                car.FuelLevel = percentageFuelLevel;
                car.PrintInfo();

                // користувач вводить кількість кілометрів, які проїде машина
                Console.Write(" ---------------------------------\n" +
                    " You have driven (km): ");
                traveledKilometers = Convert.ToInt32(Console.ReadLine());

                switch (traveledKilometers > 0) // перевірка, щоб значення не було від'ємним
                {
                    case 1 > 0:
                        car.Drive(traveledKilometers); // звернення до метода, що додає значення до пробігу та зменшує рівень палива

                        switch (car.FuelLevel <= 0) // перевірка рівня палива
                        {
                            case 1 >= 0:
                                Console.WriteLine("\n Fuel level has dropped 0%");
                                break;

                            case 1 <= 0:
                                Console.Write(" ---------------------------------\n" +
                                    " Trying to refuel (%): ");
                                refuelLevel = Convert.ToInt32(Console.ReadLine()); // ввід значення для відновлення рівня палива

                                switch (refuelLevel > 0) // перевірка рівня палива, щоб значення не було негативним
                                {
                                    case 1 > 0:
                                        car.Refuel(refuelLevel); // звернення до метода, що відновлює рівень палива
                                       
                                        Console.Write(" ---------------------------------\n" +
                                            " To be serviced.\n");
                                        car.Service(); // звернення до метода, що оновлює стан машини (скидає лічильник пробігу)
                                        car.PrintInfo(); // звернення до метода, що виводить інформацію в консоль
                                        break;

                                    case 1 < 0:
                                        Console.WriteLine(" The value cannot be negative!");
                                        break;
                                }
                                break;
                        }
                        break;

                case 1 < 0:
                    Console.WriteLine(" The value cannot be negative!");
                    break;
                }
            } 
            catch
            {
                Console.WriteLine("\n Enter data incorrectly! :3");
            }

            Console.ReadLine();
        }
    }
}
