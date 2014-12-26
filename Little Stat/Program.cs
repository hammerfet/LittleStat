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
            // Start the helper object
            ProgramHelper helper = new ProgramHelper();

            // Simple GUI interface
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu..");
                Console.WriteLine("  1 - Create New Character");
                Console.WriteLine("  2 - ..");
                Console.WriteLine("  3 - ..");
                Console.WriteLine("  4 - Display Character info");
                Console.WriteLine("  5 - Load Character from file");
                Console.WriteLine("  6 - Save Character to file");
                Console.WriteLine("  7 - Modify Character Stats");
                Console.WriteLine("  8 - Check Character Inventory");
                Console.WriteLine("  9 - Start Combat");
                Console.WriteLine("  ESC - EXIT");
                var menu = Console.ReadKey();
                Console.Clear();

                switch (menu.Key)
                {
                    case ConsoleKey.D1:
                        helper.CreateChar();                         
                        break;

                    case ConsoleKey.D2:
                        break;

                    case ConsoleKey.D3:
                        break;

                    case ConsoleKey.D4:
                        helper.DisplayChar();
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
        
        
        /*
         * No more methods here
         */
    }
}
