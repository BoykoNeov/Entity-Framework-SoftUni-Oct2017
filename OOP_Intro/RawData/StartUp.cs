namespace RawData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            int numberOfCars = int.Parse(Console.ReadLine());
            Car[] cars = new Car[numberOfCars];

            for (int i = 0; i < numberOfCars; i++)
            {
                string[] inputs = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string carModel = inputs[0];
                int engineSpeed = int.Parse(inputs[1]);
                int enginePower = int.Parse(inputs[2]);
                int cargoWeight = int.Parse(inputs[3]);
                string cargoType = inputs[4];

                // this is intentionally done crappy...
                Tire[] tires = new Tire[4];
                for (int j = 0; j < 8; j += 2)
                {
                    Tire newTire = new Tire()
                    {
                        Pressure = double.Parse(inputs[5 + j]),
                        Age = int.Parse(inputs[6 + j]),
                    };

                    tires[j / 2] = newTire;
                }

                Cargo currentCargo = new Cargo(cargoWeight, cargoType);
                Engine currentEngine = new Engine(engineSpeed, enginePower);

                Car currentCar = new Car(carModel, currentCargo, currentEngine, tires);
                cars[i] = currentCar;
            }

            //this too is intentionally done crappy...
            string consoleCommand = Console.ReadLine();
            if (consoleCommand == "fragile")
            {
                IEnumerable<Car> carsWithFragileCargoAndLowPressureTires = cars.Where(x =>
                {
                    return x.Cargo.CargoType == "fragile" &&
                  // Also hard to understand task definition, inititally I thought the idea was to check if the sum of tire pressures is bellow 1
                  //  (((x.Tires.Select(t => t.Pressure).Sum()) / (double)x.Tires.Length) < 1);
                  (x.Tires.Where(p => p.Pressure < 1).Count() > 0);
                }
                );

                foreach (Car car in carsWithFragileCargoAndLowPressureTires)
                {
                    Console.WriteLine(car.Model);
                }
            }
            else if (consoleCommand == "flammable")
            {
                IEnumerable<Car> carsWithFlammableCargoAndPowerfullEngine = cars.Where(x =>
               {
                   return (x.Cargo.CargoType == "flammable" &&
                        (x.Engine.Power > 250));
               }
            );

                foreach (Car car in carsWithFlammableCargoAndPowerfullEngine)
                {
                    Console.WriteLine(car.Model);
                }
            }
        }
    }
}