using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Stat
{
    class Program
    {
        static void newObject()
        {
            Stats stats = new Stats();

            Console.Clear();
            Console.Write("Enter NAME: ");
            string name = Console.ReadLine();

            for (int index = 0; index < stats.BaseStatNames.Length; index++ )
            {
                Console.Write("Enter ");
                Console.Write(stats.BaseStatNames[index]);
                Console.Write(" value: ");

                int value;
                string line = Console.ReadLine();

                if (int.TryParse(line, out value))
                {
                    stats.BaseStatValues.SetValue(index, value);
                }

                else
                {
                    Console.WriteLine("This has to be a number");
                    index--;
                }
            }

            stats.derriveMinorStats();
           
            Console.WriteLine("Minor stats are...");
            Console.Write("Max HP: ");
            Console.WriteLine(stats.MAXHP);

            Console.WriteLine("Press anykey to return...");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Little Stat RPG stat Engine - Press any key to start");
            Console.ReadKey();

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
                        newObject();
                        break;

                    case ConsoleKey.D2:
                        //
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
