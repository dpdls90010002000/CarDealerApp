using System;
using System.Reflection;
using static Yein_A2.Program;

namespace Yein_A2;
//Yein An 301316062
class Program
{
    static void Main(string[] args)
    {
        TestDealership();
    }
    static void TestDealership()
    {
        Console.WriteLine("Assignment 2 Output");

        Dealership dealership1 = new Dealership("D1_22_T501", "The Six Cars", "1 Main Street, Toronto");
        Console.WriteLine(dealership1.ToString());

        Dealership dealership2 = new Dealership("D2_22_B321", "Car Street", "5th avenue, Brampton");
        Console.WriteLine(dealership2.ToString());

        Console.WriteLine("\nToyota Cars available in Dealership 1\n");
        
        dealership1.ShowCars("Toyota");

        Console.WriteLine("\nToyota Cars available in Dealership 2\n");
        
        dealership2.ShowCars("Toyota");

        Car favCar = new Car("Hyundai", 2020, "Elantra", 30000.00, CarType.Sedan);
        Console.WriteLine($"\nCar to match :\n{favCar.ToString()}\n");

        Console.WriteLine("\nMatching car(s) from Dealership 1 : ");
        dealership1.ShowCars(favCar);

        Console.WriteLine("\nMatching car(s) from Dealership 2 : ");
        dealership2.ShowCars(favCar);

        //favCar = new Car("Honda", 2018, "Civic", 20000.00, CarType.SUV, CarSpecifications.FogLights | CarSpecifications.TintendGlasses);
        favCar = new Car("Honda", 2018, "Civic", 20000.00, CarType.SUV);

        Console.WriteLine($"\nCar to match :\n{favCar.ToString()}");

        Console.WriteLine("\nMatching car(s) from Dealership 1 :");
        dealership1.ShowCars(favCar);

        Console.WriteLine("\nMatching car(s) from Dealership 2 :");
        dealership2.ShowCars(favCar);

        Console.WriteLine("\nList of similiar car models available in both dealership : ");

        foreach (Car firstCar in dealership1.CarList)
        {
            foreach (Car secondsCar in dealership2.CarList)
            {
                if (firstCar == secondsCar)
                {
                    Console.WriteLine($"Dealership 1 :\n{firstCar.ToString()}");
                    Console.WriteLine($"Dealership 2 :\n{secondsCar.ToString()}");
                }
            }
        }
    }

    public enum CarType
    {
        SUV, Hatchback, Sedan, Truck
    }

    public class Car
    {
        public string Manufacturer { get; }
        public int Make { get; }
        public string Model { get; }
        private static int VI_NUMBER = 1021;
        private int VIN;
        public double BasePrice { get; }
        public CarType Type { get; }




        public Car(string manufacturer, int make, string model, double basePrice, CarType type)
        {
            Manufacturer = manufacturer;
            Make = make;
            Model = model;
            BasePrice = basePrice;
            Type = type;


            VI_NUMBER += 100;
            VIN = VI_NUMBER;

        }

        public override string ToString()
        {
            return $"{this.VIN} : {this.Manufacturer}, {this.Make}, {this.Model},{this.BasePrice} ,{this.Type}";
        }

        public static bool operator ==(Car firstCar, Car secondsCar)
        {
            bool result = false;
            if ((firstCar.Manufacturer == secondsCar.Manufacturer) && (firstCar.Model == secondsCar.Model) && (firstCar.Type == secondsCar.Type))

            {
                result = true;
               

            }
            return result;
           


        }

        public static bool operator !=(Car firstCar, Car secondsCar)
        {
            bool result = false;

            if ((firstCar.Manufacturer != secondsCar.Manufacturer) || (firstCar.Model != secondsCar.Model) || (firstCar.Type != secondsCar.Type))
            {
                result = true;
                
            }
            return result;


        }

        




    }

    public class Dealership
        {
            public static string FILE = "Dealership_Cars.txt";
            public List<Car> CarList; 
            public string ID { get; }
            public string Name { get; }
            public string Address { get; }

            

            public Dealership(string iD, string name, string address)
            {
                this.ID = iD;
                this.Name = name;
                this.Address = address;
                this.CarList = new List<Car>();
                this.readDealership();
                

        }
            public void readDealership()
            {
                try
                {
                    using (StreamReader reader = new StreamReader(FILE))
                    {
                        string recordLine;
                        Car tempCar;
                       
                        
                        while ((recordLine = reader.ReadLine()) != null)
                        {
                            //Console.WriteLine($"{recordLine}");
                            string[] values = recordLine.Split(',');

                        string G = values[0];

                        if (ID.ToString() == G) {
                        
                            string manufacturer = values[1];
                            int make = Convert.ToInt32(values[2]);
                            string model = values[3];
                            double basePrice = Convert.ToDouble(values[4]);
                            CarType carType = (CarType)Enum.Parse(typeof(CarType), values[5]);
                            tempCar = new Car(manufacturer, make, model, basePrice, carType);
                            Console.WriteLine($"{tempCar}");
                            this.CarList.Add(tempCar);

                            

                        }

                    }
                    reader.Close();
                }
                }
                catch (FileNotFoundException fne)
                {
                    Console.WriteLine($"File {FILE} not found at mentioned location. " +
                        $"please check the file name and location");
                    Console.WriteLine($"{fne.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong while reading from file {FILE}");
                    Console.WriteLine($"{ex}");

                }


        }

        public override string ToString()
        {
            return $"\n{this.ID}, {this.Name}, {this.Address},\n";
        }

        public void ShowCars(string manufacturer)
            {

            bool found = false;

            foreach(Car car in this.CarList)
            {
                if (car.Manufacturer.ToUpper() == manufacturer.ToUpper())
                {
                    Console.WriteLine($"{car}\n");


                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("what");
            }


            }

            public void ShowCars(Car fav)
            {
            bool found = false;
                foreach(Car car in this.CarList)
            {
                if ((car.Manufacturer.ToUpper() == fav.Manufacturer.ToUpper()) && (car.Make == fav.Make))
                {
                    Console.WriteLine($"{car} ");

                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("None");
            }


            }
    }


       

 }


    




