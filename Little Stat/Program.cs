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
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  Little Stat | RPG Battle Engine");
                Console.WriteLine("  -------------------------------");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine("    Main menu");
                Console.WriteLine("    ---------");
                Console.WriteLine("");
                Console.WriteLine("    1 - Display Character Info");
                Console.WriteLine("    2 - Create/Modify Character");
                Console.WriteLine("");
                Console.WriteLine("    3 - Display Inventory");
                Console.WriteLine("    4 - Add/Remove Inventory");
                Console.WriteLine("");
                Console.WriteLine("    5 - ");
                Console.WriteLine("    6 - ");
                Console.WriteLine("    7 - ");
                Console.WriteLine("    8 - ");
                Console.WriteLine("    9 - Start Combat");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("    ESC - EXIT");
                Console.ResetColor();

                var menu = Console.ReadKey();
                Console.Clear();

                switch (menu.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        CreateChar();                         
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        DisplayChar();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        //    
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        //
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        //
                        break;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        //
                        break;

                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        //
                        break;

                    case ConsoleKey.Escape:
                        System.Environment.Exit(1);
                        break;
                }
            }
        }


        /*
         * Creates a character, with all base stats
         */
        static void CreateChar()
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Create new Character");
            Console.WriteLine("  --------------------");
            Console.ResetColor();
            Console.WriteLine("");
            Console.Write("    Enter PC name: ");

            string name = Console.ReadLine();

            if (character.CheckForOrCreateChar(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n    Character already exists, \n    Press Enter to overwrite or any key to return..\n");
                Console.ResetColor();
                var menu = Console.ReadKey();
                if (menu.Key != ConsoleKey.Enter) return;
            }

            Console.Write("    Enter STRENGTH value: ");
            character.SetCharStats(name, "Strength", GetFloatFromConsole());

            Console.Write("    Enter VIGOUR value: ");
            character.SetCharStats(name, "Vigour", GetFloatFromConsole());

            Console.Write("    Enter AGILITY value: ");
            character.SetCharStats(name, "Agility", GetFloatFromConsole());

            Console.Write("    Enter INTELLECT value: ");
            character.SetCharStats(name, "Intellect", GetFloatFromConsole());

            Console.Write("    Enter PERCEPTION value: ");
            character.SetCharStats(name, "Perception", GetFloatFromConsole());

            Console.Write("    Enter TENACITY value: ");
            character.SetCharStats(name, "Tenacity", GetFloatFromConsole());

            Console.Write("    Enter CHARISMA value: ");
            character.SetCharStats(name, "CHARISMA", GetFloatFromConsole());

            Console.Write("    Enter INSTINCT value: ");
            character.SetCharStats(name, "INSTINCT", GetFloatFromConsole());

            Console.Write("    Enter COMMUNICATION value: ");
            character.SetCharStats(name, "COMMUNICATION", GetFloatFromConsole());
        }


        /*
         * Displays characters stats.
         * Currently a placeholder
         */
        static void DisplayChar()
        {
            Console.WriteLine(character.ReturnStat("Bud", "Fortitude"));
            Console.ReadKey();
        }


        /*
         * Converts the text input into a float
         * shows an error message if not suceeded
         */
        static float GetFloatFromConsole()
        {
            while (true)
            {
                float res;
                if (float.TryParse(Console.ReadLine(), out res)) return res;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("    Input has to be a number, try again: ");
                    Console.ResetColor();
                }
            }
        }

        /*
         * Local variable declarations
         */
        static Character character = new Character();
        
        float STRENGTH;
        float VIGOUR;
        float AGILITY;
        float INTELLECT;
        float PERCEPTION;
        float TENACITY;
        float CHARISMA;
        float INSTINCT;
        float COMMUNICATION;
    }
}
