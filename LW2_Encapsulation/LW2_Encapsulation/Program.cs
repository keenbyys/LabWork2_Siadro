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
        private string make;
        private string model;
        private int year;
        private int odometr;
        private int fuelLevel;

        public string Make
        {
            get { return make; }
            set { make = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public int Odometr
        {
            set { year = value; }
        }

        public int FuelLevel
        {
            get { return fuelLevel; }
            set { if (value >=0 && value <= 100)
                    fuelLevel = value; }
        }

        public Car()
        {
        }

        public Car(string make, string model, int year)
        {
            this.make = make;
            this.model = model;
            this.year = year;
        }

        public Car(Car car)
        {
            Make = car.Make;
            Model = car.Model;
            Year = car.Year;
        }

        public void Drive(int kilometers)
        {
            odometr += kilometers;
            if (kilometers >= 0)
            {
                for (int i = 1; i <= kilometers/10; i++)
                {
                    fuelLevel -= 1;
                }
            }

            Console.WriteLine("\n Odometr: {0} km\n" +
                " Fuel level: {1}%", odometr, fuelLevel);
        }

        public void Refuel(int percentage)
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
            int traveledKilometers = 46;
            int refuelLevel = 20;
            int percentageFuelLevel = 100;

            Car car = new Car("Tesla", "Coupe", 1999);
            car.FuelLevel = percentageFuelLevel;
            car.PrintInfo();

            Console.WriteLine(" ---------------------------------\n" +
                " The owner drove {0} kilometers", traveledKilometers);
            car.Drive(traveledKilometers);

            Console.WriteLine(" ---------------------------------\n" +
                " Trying to refuel: {0}%\n" +
                " ---------------------------------", refuelLevel);
            car.Refuel(refuelLevel);

            Console.ReadLine();
        }
    }
}
