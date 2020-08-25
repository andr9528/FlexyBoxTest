using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using FlexyBoxTest.Domain.Core;
using FlexyBoxTest.Domain.Core.Enums;
using FlexyBoxTest.Utility;
using FlexyBoxTest.Utility.Extensions;

namespace FlexyBoxTest.ConsoleApplication
{
    class Program
    {
        delegate void MenuAction();

        List<IVehicle> Vehicles { get; set; }
        string Path { get; set; }

        Dictionary<MenuEnum, MenuAction> MenuDictionary;

        PersistanceService Persistance { get; set; }

        static void Main(string[] args)
        {
            var program = new Program();

            program.Run();
        }

        public Program()
        {
            Path = AppDomain.CurrentDomain.BaseDirectory;
            Persistance = new PersistanceService();

            Vehicles = (List<IVehicle>)new InstanceService().GetInstances<IVehicle>();
            Vehicles = Vehicles.OrderBy(x => x.GetType().Name).ToList();

            MenuDictionary = new Dictionary<MenuEnum, MenuAction>
            {
                {MenuEnum.Exit, Goodby},
                {MenuEnum.Write_Vehicles, () => WriteVehicles(Vehicles)},
                {MenuEnum.Search, HandleSearch},
                {MenuEnum.Save, () => SaveToFile(Path, Vehicles)},
                {MenuEnum.Reverse, ReverseString},
                {MenuEnum.Palindrome_Check, CheckForPalindrome}
            };

        }

        private void Run()
        {
            Console.WriteLine("Hello World!");

            WriteVehicles(Vehicles, true);

            Menu();
        }
        
        private void Menu()
        {
            var selection = MenuEnum.Null;

            while (selection != MenuEnum.Exit)
            {
               WriteMenu(); 

               var read = Console.ReadLine();
               var parseIntSucces = int.TryParse(read, out var result);
               
               if (!parseIntSucces)
               {
                   Console.WriteLine($"Input was not a number, Try Again...");
                   Console.WriteLine();
                   continue;
               }

               try
               {
                    selection = (MenuEnum) result;

                    var getActionSucces = MenuDictionary.TryGetValue(selection, out var action);
                    if (getActionSucces) action.Invoke();
                    else Console.WriteLine($"Invalid Number written, try again...");

               }
               catch (InvalidCastException ice)
               {
                   Console.WriteLine($"Invalid Number written, try again...");
               }
            }
        }

        private void WriteMenu()
        {
            var builder = new StringBuilder();

            builder.AppendLine();

            for (var i = 0; i < MenuDictionary.Count; i++)
            {
                builder.AppendLine($"{i} = {MenuDictionary.Keys.First(x => (int)x == i)}");
            }

            builder.AppendLine();
            builder.AppendLine($"Write your choice and hit enter.");

            Console.WriteLine(builder.ToString());
        }

        private void Goodby()
        {
            Console.WriteLine();
            Console.WriteLine($"Goodby :)");
        }

        private void WriteVehicles(List<IVehicle> vehicles, bool firstTime = false)
        {
            if (!firstTime)
            {
                Console.WriteLine();
                Console.WriteLine($"Writing {vehicles.Count} Vehicle(s)...");
            }

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }

            Console.WriteLine();
        }

        private void HandleSearch()
        {
            Console.WriteLine();
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

                    if (type.Name.ToLowerInvariant().Contains(text.ToLowerInvariant())) output.Add(input);
                }

                return output;
            });

            if (result.Count != 0) WriteVehicles(result);
            else Console.WriteLine($"No results was found from the search :(");
        }
        
        private void SaveToFile(string path, List<IVehicle> vehicles)
        {
            var finalPath = System.IO.Path.Combine(path, $"Data.json");

            Persistance.SaveToJson(finalPath, vehicles);

            Console.WriteLine();
            Console.WriteLine($"Saved the Vehicles to file at {finalPath}");
        }

        private void ReverseString()
        {
            Console.WriteLine();
            Console.WriteLine($"Write text to reverse, and click enter.");

            var text = Console.ReadLine();
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine($"Please write something before hitting enter next time...");
                return;
            }

            var chars = text.ToList();
            chars = chars.ReverseList();

            var reversed = new string(chars.ToArray());
            Console.WriteLine();
            Console.WriteLine($"Your reversed text is: {reversed}");
        }

        private void CheckForPalindrome()
        {
            Console.WriteLine();
            Console.WriteLine($"Write text to check, and click enter.");

            var text = Console.ReadLine();
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine($"Please write something before hitting enter next time...");
                return;
            }

            var check = text.IsPalindrome();

            Console.WriteLine();
            var message = $"The Check returned: {check}. It is ";
            if (!check) message += "not ";
            message += "a Palindrome!";
            Console.WriteLine(message);
        }
    }
}
