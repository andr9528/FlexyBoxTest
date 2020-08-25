using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexyBoxTest.Domain.Core;
using FlexyBoxTest.Utility;
using FlexyBoxTest.Utility.Extensions;

namespace FlexyBoxTest.ConsoleApplication
{
    class Program
    {
        List<IVehicle> Vehicles;

        static void Main(string[] args)
        {
            var program = new Program();

            program.Run();
        }

        public Program()
        {
            Vehicles = (List<IVehicle>)new InstanceService().GetInstances<IVehicle>();
            Vehicles = Vehicles.OrderBy(x => x.GetType().Name).ToList();
        }

        private void Run()
        {
            Console.WriteLine("Hello World!");

            WriteVehicles(Vehicles);

            Menu();
        }

        private void WriteVehicles(List<IVehicle> vehicles)
        {
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }

            Console.WriteLine();
        }

        private void WriteMenu()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"0 = Exit Program");
            builder.AppendLine($"1 = Write all Vehicles");
            builder.AppendLine($"2 = Search for Vehicle");
            builder.AppendLine();
            builder.AppendLine($"Write your choice and hit enter.");

            Console.WriteLine(builder.ToString());
        }

        private void Menu()
        {
            var selection = -1;

            while (selection != 0)
            {
               WriteMenu(); 

               var read = Console.ReadLine();
               var succes = int.TryParse(read, out selection);

               if (!succes)
               {
                   Console.WriteLine($"Input was not a number, Try Again...");
                   Console.WriteLine();
                   continue;
               }

               switch (selection)
               {
                    case 0:
                        Console.WriteLine();
                        Console.WriteLine($"Goodby :)");
                        break;
                    case 1:
                        Console.WriteLine();
                        Console.WriteLine($"Writing all Vehicles again...");
                        WriteVehicles(Vehicles);
                        break;
                    case 2:
                        Console.WriteLine();

                        break;
                        
               }
            }
        }

        private void HandleSearch()
        {
            Console.WriteLine($"Write part or the whole name of the vehicle, and click enter.");
            var text = Console.ReadLine();
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine($"Please write something before hitting enter next time...");
                return;
            }

            var result = Vehicles.FindElements((inputs) =>
            {
                var output = new List<IVehicle>();
                
                foreach (var input in inputs)
                {
                    var type = input.GetType();

                    if (type.Name.Contains(text)) output.Add(input);
                }

                return output;
            });

            if (result.Count != 0) WriteVehicles(result);
            else Console.WriteLine($"No results was found from the search :(");
        }
    }
}
