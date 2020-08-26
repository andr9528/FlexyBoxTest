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
            
            MenuDictionary = new Dictionary<MenuEnum, MenuAction>
            {
                {MenuEnum.Exit, Goodby},
                {MenuEnum.Write_Vehicles, () => WriteVehicles(Vehicles)},
                {MenuEnum.Search, HandleSearch},
                {MenuEnum.Save, () => SaveToFile(Path, Vehicles)},
                {MenuEnum.Reverse, ReverseString},
                {MenuEnum.Palindrome_Check, CheckForPalindrome},
                {MenuEnum.Missing_Numbers, FindMissingNumbers}
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

               if (GetInput(out var text)) continue;
               var parseIntSucces = int.TryParse(text, out var result);
               
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
        
        private bool GetInput(out string text)
        {
            text = Console.ReadLine();
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine($"Please write something before hitting enter next time...");
                return true;
            }

            return false;
        }

        // Task 3.1
        private void WriteVehicles(List<IVehicle> vehicles, bool firstTime = false)
        {
            if (!firstTime)
            {
                Console.WriteLine();
                Console.WriteLine($"Writing {vehicles.Count} Vehicle(s)...");
            }

            vehicles = vehicles.OrderBy(x => x.GetType().Name).ToList();

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }

            Console.WriteLine();
        }

        // Task 3.2
        private void HandleSearch()
        {
            Console.WriteLine();
            Console.WriteLine($"Write part or the whole name of the vehicle, and click enter.");

            if (GetInput(out var text)) return;

            var result = Vehicles.FindElements(inputs =>
            {
                var output = inputs.Where(x => x.GetType().Name.ToLowerInvariant().Contains(text.ToLowerInvariant()))
                    .ToList();
                return output;
            });

            var enumerable = result.ToList();
            if (enumerable.Count != 0) WriteVehicles(enumerable);
            else Console.WriteLine($"No results was found from the search :(");
        }
        
        // Task 3.3
        private void SaveToFile(string path, List<IVehicle> vehicles)
        {
            var finalPath = System.IO.Path.Combine(path, $"Data.json");

            Persistance.SaveToJson(finalPath, vehicles);

            Console.WriteLine();
            Console.WriteLine($"Saved the Vehicles to file at {finalPath}");
        }

        // Task 4.1
        private void ReverseString()
        {
            Console.WriteLine();
            Console.WriteLine($"Write text to reverse, and click enter.");

            if (GetInput(out var text)) return;

            var chars = text.ToList();
            chars = chars.ReverseEnumerable().ToList();

            var reversed = chars.GetString();
            Console.WriteLine();
            Console.WriteLine($"Your reversed text is: {reversed}");
        }
        
        // Task 4.2
        private void CheckForPalindrome()
        {
            Console.WriteLine();
            Console.WriteLine($"Write text to check, and click enter.");

            if (GetInput(out var text)) return;

            var check = text.IsPalindrome();

            Console.WriteLine();
            var message = $"The Check returned: {check}. It is ";
            if (!check) message += "not ";
            message += "a Palindrome!";
            Console.WriteLine(message);
        }

        // Task 4.3
        private void FindMissingNumbers()
        {
            Console.WriteLine();
            Console.WriteLine($"Write numbers to use to find missing numbers with.");
            Console.WriteLine($"Separate numbers with the use of space");
            Console.WriteLine();

            if (GetInput(out var text)) return;

            var split = text.Split(' ');
            var numbers = new List<int>();

            foreach (var str in split)
            {
                var succes = int.TryParse(str, out var number);
                if (!succes)
                {
                    Console.WriteLine($"Something you wrote, couldn't become a number, try again...");
                    return;
                }
                numbers.Add(number);
            }

            var missing = numbers.MissingElements().ToList();

            Console.WriteLine();
            Console.WriteLine($"Your Missing Numbers are as follows:");
            var builder = new StringBuilder();
            string returnText;

            if (missing.Count > 0)
            {
                foreach (var i in missing)
                {
                    builder.Append($"{i}, ");
                }
                returnText = builder.ToString();
                returnText = returnText.TrimEnd(' ', ',');
            }
            else
                returnText = $"There where no missing numbers, Great! :)";

            Console.WriteLine(returnText);
        }
    }
}
