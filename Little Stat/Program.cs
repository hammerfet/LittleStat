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

            Character character = new Character();

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
                Console.Clear();

                switch (menu.Key)
                {
                    case ConsoleKey.D1:
                        Console.Write("Enter PC name: ");
                        name = Console.ReadLine();
                        character.CreatePC(name, "PC");
                        break;

                    case ConsoleKey.D2:
                        Console.Write("Enter NPC name: ");                        
                        name = Console.ReadLine();
                        character.CreatePC(name, "NPC");
                        break;

                    case ConsoleKey.D3:
                        character.PrintPC();
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
