using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Stat
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Little Stat RPG stat Engine - Press any key to start");
            Console.ReadKey();

            string name;

            Dictionary<string, string> Character = new Dictionary<string, string>() { };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu..");
                Console.WriteLine("  1 - Enter New Player");
                Console.WriteLine("  2 - Enter New NPC");
                Console.WriteLine("  3 - Enter New Hostile");
                Console.WriteLine("  4 - Create battle instance");
                Console.WriteLine("  5 - Show Inventory");
                Console.WriteLine("  6 - Show Stats");
                Console.WriteLine("  ESC - EXIT");
                var menu = Console.ReadKey();

                switch (menu.Key)
                {
                    case ConsoleKey.D1:
                        name = Console.ReadLine();
                        Character.Add(name, "PC");
                        break;

                    case ConsoleKey.D2:
                        name = Console.ReadLine();
                        Character.Add(name, "NPC");
                        break;

                    case ConsoleKey.D3:
                        //
                        break;

                    case ConsoleKey.D4:
                        //
                        break;

                    case ConsoleKey.D5:
                        //
                        break;

                    case ConsoleKey.D6:
                        //
                        break;

                    case ConsoleKey.D7:
                        //
                        break;

                    case ConsoleKey.Escape:
                        System.Environment.Exit(1);
                        break;
                }
            }
        }
    }
}
