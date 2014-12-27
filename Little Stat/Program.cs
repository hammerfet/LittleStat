using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Stat
{
    class Program
    {
        /*
         * Main application is simply
         * a console based user interface
         */
        static void Main(string[] args)
        {
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
                        DisplayChar();                     
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        CreateChar();  
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
         * Checks if the character name is a good length
         * and if it already exists. It will give an option
         * to overwrite.
         * 
         * Args: none
         * 
         * Returns: only if bad parameter or user cancels
         */
        static void CreateChar()
        {
            // Header
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Create new Character");
            Console.WriteLine("  --------------------");
            Console.ResetColor();
            Console.WriteLine("");
            Console.Write("    Enter PC name: ");

            // Get name and check string length
            string name = Console.ReadLine();
            if (name.Length > 30 || name.Length < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n    Character name length has to be between than 0 and 30, \n    Press any key to return..");
                Console.ResetColor();
                var menu = Console.ReadKey();
                return;
            }

            // Check if character exists
            if (character.Exists(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n    Character already exists, \n    Press Enter to overwrite or any key to return..\n");
                Console.ResetColor();
                var menu = Console.ReadKey();
                if (menu.Key != ConsoleKey.Enter) return;
            }

            // Create character if doesn't exist
            else character.Create(name);

            // Finally write or overwite character stats
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

            // Set Max HP, Mana and Stamina
            character.SetCharStats(name, "CurrentHP", character.ReturnStat(name, "MaxHP"));
            character.SetCharStats(name, "CurrentMana", character.ReturnStat(name, "MaxMana"));
            character.SetCharStats(name, "CurrentStamina", character.ReturnStat(name, "MaxStamina"));
        }


        /*
         * Displays all characters in database.
         * User can then select a character to
         * see details of.
         * 
         * Returns: only if user types invalid character
         */
        static void DisplayChar()
        {
            // Header
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  The following characters exist");
            Console.WriteLine("  ------------------------------");
            Console.ResetColor();
            Console.WriteLine("");
            
            // Get list of characters
            var NamesList = character.GetListOfChars();
            NamesList.ForEach(delegate(String name)
                {   Console.WriteLine("    {0}", name);   }
            );

            // Let the user type in the character name to get info for
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  Select Character to get info: ");
            string characterName = Console.ReadLine();
            Console.ResetColor();

            // Print out info is character exists
            if (character.Exists(characterName))
            {
                Console.WriteLine("");
                Console.Write("    HP = {0} / {1}", character.ReturnStat(characterName, "CurrentHP"), character.ReturnStat(characterName, "MaxHP"));
                Console.Write("    Mana = {0} / {1}", character.ReturnStat(characterName, "CurrentMana"), character.ReturnStat(characterName, "MaxMana"));
                Console.Write("    Stamina = {0} / {1}", character.ReturnStat(characterName, "CurrentStamina"), character.ReturnStat(characterName, "MaxStamina"));
                Console.WriteLine("\n");
                Console.Write("    Strength: {0}", character.ReturnStat(characterName, "Strength"));
                Console.Write("    Vigour: {0}", character.ReturnStat(characterName, "Vigour"));
                Console.Write("        Agility: {0}", character.ReturnStat(characterName, "Agility"));
                Console.WriteLine("");
                Console.Write("    Intellect: {0}", character.ReturnStat(characterName, "Intellect"));
                Console.Write("   Perception: {0}", character.ReturnStat(characterName, "Perception"));
                Console.Write("    Tenacity: {0}", character.ReturnStat(characterName, "Tenacity"));
                Console.WriteLine("");
                Console.Write("    Charisma: {0}", character.ReturnStat(characterName, "Charisma"));
                Console.Write("    Instinct: {0}", character.ReturnStat(characterName, "Instinct"));
                Console.Write("      Communication: {0}", character.ReturnStat(characterName, "Communication"));
                Console.WriteLine("\n");
                Console.Write("    Movement: {0}", character.ReturnStat(characterName, "Movement"));
                Console.Write("   Reaction: {0}", character.ReturnStat(characterName, "Reaction"));
                Console.WriteLine("");
                Console.Write("    Fortitude: {0}", character.ReturnStat(characterName, "Fortitude"));
                Console.Write("  Will: {0}", character.ReturnStat(characterName, "Will"));
                Console.WriteLine("\n");
                Console.Write("    Experience: {0}", character.ReturnStat(characterName, "EXP"));
                Console.WriteLine("\n");

            }
            
            // Notify user if character doesnt exist
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("");
                Console.WriteLine("  No such character. ");
                Console.WriteLine("");
                Console.ResetColor();
            }

            // Press any key to return
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("  Press any key to return or 'Del' to remove character");
            Console.ResetColor();
            var menu = Console.ReadKey();
            if (menu.Key == ConsoleKey.Delete) character.Delete(characterName);

            return;
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
         * No more methods here
         */


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
