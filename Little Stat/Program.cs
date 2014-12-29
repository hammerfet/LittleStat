using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Stat
{
    class Program
    {
        /// <summary>
        /// Main application for Little Stat - Console
        /// based RPG combat engine
        /// </summary>
        /// <param name="args"></param>
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
                Console.WriteLine("    1 - List Characters/Inventory");
                Console.WriteLine("    2 - Create/Modify Character");
                Console.WriteLine("    3 - ");
                Console.WriteLine("    4 - ");
                Console.WriteLine("    5 - ");
                Console.WriteLine("    6 - ");
                Console.WriteLine("    7 - Test D% Roll");
                Console.WriteLine("    8 - Test normally distributed roll");
                Console.WriteLine("    9 - Start Combat");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("    ESC - EXIT");
                Console.ResetColor();

                var menu = Console.ReadKey();

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
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        
                        break;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        //
                        break;

                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        Console.Clear();
                        Console.WriteLine("\n\n    Result: {0}", dice.RollPercentile());
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        Console.Clear();
                        Console.WriteLine("\n\n    Result: {0}", dice.RollNormal());
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        break;

                    case ConsoleKey.Escape:
                        System.Environment.Exit(1);
                        break;
                }
            }
        }


        /// <summary>
        /// Creates a character, with all base stats.
        /// The function checks if the character name is a
        /// good length or if it already exists. 
        /// It will give an option to overwrite or return
        /// with an error message
        /// </summary>
        static void CreateChar()
        {
            // Header
            Console.Clear();
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
            character.SetStat(name, "Strength", GetFloatFromConsole());

            Console.Write("    Enter VIGOUR value: ");
            character.SetStat(name, "Vigour", GetFloatFromConsole());

            Console.Write("    Enter AGILITY value: ");
            character.SetStat(name, "Agility", GetFloatFromConsole());

            Console.Write("    Enter INTELLECT value: ");
            character.SetStat(name, "Intellect", GetFloatFromConsole());

            Console.Write("    Enter PERCEPTION value: ");
            character.SetStat(name, "Perception", GetFloatFromConsole());

            Console.Write("    Enter TENACITY value: ");
            character.SetStat(name, "Tenacity", GetFloatFromConsole());

            Console.Write("    Enter CHARISMA value: ");
            character.SetStat(name, "CHARISMA", GetFloatFromConsole());

            Console.Write("    Enter INSTINCT value: ");
            character.SetStat(name, "INSTINCT", GetFloatFromConsole());

            Console.Write("    Enter COMMUNICATION value: ");
            character.SetStat(name, "COMMUNICATION", GetFloatFromConsole());

            // Set Max HP, Mana and Stamina
            character.SetStat(name, "CurrentHP", character.GetStat(name, "MaxHP"));
            character.SetStat(name, "CurrentMana", character.GetStat(name, "MaxMana"));
            character.SetStat(name, "CurrentStamina", character.GetStat(name, "MaxStamina"));
        }


        /// <summary>
        /// Displays all characters in database,
        /// user can then select a character to see
        /// their details. Returns if user types in
        /// an invalid character name
        /// </summary>
        static void DisplayChar()
        {
            // Header
            Console.Clear();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  The following characters exist");
            Console.WriteLine("  ------------------------------");
            Console.ResetColor();
            Console.WriteLine("");
            
            // Get list of characters
            var NamesList = character.List();
            NamesList.ForEach(delegate(String name)
                {   Console.WriteLine("    {0}", name);   }
            );

            // Let the user type in the character name to get info for
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  Type character name to get more info, or Enter to skip: ");
            string characterName = Console.ReadLine();
            Console.ResetColor();

            // Print out info is character exists
            if (character.Exists(characterName))
            {
                Console.WriteLine("");
                Console.Write("    HP = {0} / {1}", character.GetStat(characterName, "CurrentHP"), character.GetStat(characterName, "MaxHP"));
                Console.Write("    Mana = {0} / {1}", character.GetStat(characterName, "CurrentMana"), character.GetStat(characterName, "MaxMana"));
                Console.Write("    Stamina = {0} / {1}", character.GetStat(characterName, "CurrentStamina"), character.GetStat(characterName, "MaxStamina"));
                Console.WriteLine("\n");
                Console.Write("    Strength: {0}", character.GetStat(characterName, "Strength"));
                Console.Write("    Vigour: {0}", character.GetStat(characterName, "Vigour"));
                Console.Write("        Agility: {0}", character.GetStat(characterName, "Agility"));
                Console.WriteLine("");
                Console.Write("    Intellect: {0}", character.GetStat(characterName, "Intellect"));
                Console.Write("   Perception: {0}", character.GetStat(characterName, "Perception"));
                Console.Write("    Tenacity: {0}", character.GetStat(characterName, "Tenacity"));
                Console.WriteLine("");
                Console.Write("    Charisma: {0}", character.GetStat(characterName, "Charisma"));
                Console.Write("    Instinct: {0}", character.GetStat(characterName, "Instinct"));
                Console.Write("      Communication: {0}", character.GetStat(characterName, "Communication"));
                Console.WriteLine("\n");
                Console.Write("    Movement: {0}", character.GetStat(characterName, "Movement"));
                Console.Write("   Reaction: {0}", character.GetStat(characterName, "Reaction"));
                Console.WriteLine("");
                Console.Write("    Fortitude: {0}", character.GetStat(characterName, "Fortitude"));
                Console.Write("  Will: {0}", character.GetStat(characterName, "Will"));
                Console.WriteLine("\n");
                Console.Write("    Experience: {0}", character.GetStat(characterName, "EXP"));
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
            Console.WriteLine("  Press 'I' to list characters inventory");
            Console.WriteLine("  Press 'Del' to remove character");
            Console.WriteLine("  Press any key to return");
            Console.ResetColor();
            var menu = Console.ReadKey();
            if (menu.Key == ConsoleKey.Delete) DeleteChar(characterName);
            if (menu.Key == ConsoleKey.I) DisplayInventory(characterName);

            return;
        }


        /// <summary>
        /// Removes character and their inventory.
        /// All items are passed to the GM character
        /// </summary>
        /// <param name="NAME">Name of character</param>
        static void DeleteChar(string NAME)
        {
            character.Delete(NAME);
            inventory.RemoveChar(NAME);
        }


        /// <summary>
        /// Creates new item within the inventory.
        /// User must enter all related stats to the item
        /// </summary>
        /// <param name="CHARNAME">Name of character</param>
        static void CreateInventory(string CHARNAME)
        {
            // Header
            Console.Clear();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Create new Item");
            Console.WriteLine("  ---------------");
            Console.ResetColor();
            Console.WriteLine("");
            Console.Write("    Enter item name: ");

            // Get name and check string length
            string ITEMNAME = Console.ReadLine();
            if (ITEMNAME.Length > 30 || ITEMNAME.Length < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n    item name length has to be between than 0 and 30, \n    Press any key to return..");
                Console.ResetColor();
                var menu = Console.ReadKey();
                return;
            }

            // Check if character exists
            if (inventory.Exists(CHARNAME, ITEMNAME))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n    Item already exists, \n    Press Enter to overwrite or any key to return..\n");
                Console.ResetColor();
                var menu = Console.ReadKey();
                if (menu.Key != ConsoleKey.Enter) return;
            }

            // Create character if doesn't exist
            else inventory.Create(CHARNAME, ITEMNAME);

            // Finally write or overwite character stats
            Console.Write("    Enter item description: ");
            inventory.SetDescription(CHARNAME, ITEMNAME, Console.ReadLine());

            Console.Write("    Enter item QUANTITY: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "Quantity", GetFloatFromConsole());

            Console.Write("    Enter item WEIGHT: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "Weight", GetFloatFromConsole());

            Console.Write("    Enter STRENGTH modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "STRModifier", GetFloatFromConsole());

            Console.Write("    Enter VIGOUR modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "VIGModifier", GetFloatFromConsole());

            Console.Write("    Enter AGILITY modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "AGIModifier", GetFloatFromConsole());

            Console.Write("    Enter INTELLECT modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "INTModifier", GetFloatFromConsole());

            Console.Write("    Enter PERCEPTION modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "PERModifier", GetFloatFromConsole());

            Console.Write("    Enter TENACITY modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "TENModifier", GetFloatFromConsole());

            Console.Write("    Enter CHARISMA modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "CHAModifier", GetFloatFromConsole());

            Console.Write("    Enter INSTINCT modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "INSModifier", GetFloatFromConsole());

            Console.Write("    Enter COMMUNICATION modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "COMModifier", GetFloatFromConsole());

            Console.Write("    Enter WEAPON power: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "WeaponValue", GetFloatFromConsole());

            Console.Write("    Enter ARMOR power: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "ArmorValue", GetFloatFromConsole());

            Console.Write("    Enter HP BOOST modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "HPBoost", GetFloatFromConsole());

            Console.Write("    Enter MANA BOOST modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "ManaBoost", GetFloatFromConsole());

            Console.Write("    Enter STAMINA BOOST modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, "StaminaBoost", GetFloatFromConsole());

            Console.Write("    Enter how many TURNS the item lasts (0 for inf): ");
            inventory.SetStat(CHARNAME, ITEMNAME, "LastsTurns", GetFloatFromConsole());

        }


        /// <summary>
        /// Lists the Inventory of a character, allows adding
        /// deleting, transfering or copying of the item
        /// </summary>
        /// <param name="NAME">Name of character</param>
        static void DisplayInventory(string CHARNAME)
        {
            // Header
            Console.Clear();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Items belonging to: {0}", CHARNAME);
            Console.WriteLine("  -------------------");
            Console.ResetColor();
            Console.WriteLine("");

            // Get list of items
            var ItemList = inventory.List(CHARNAME);
            ItemList.ForEach(delegate(String ITEMNAME)
                {
                    string ITEMDESC = inventory.GetDescription(CHARNAME, ITEMNAME);
                    Console.WriteLine("    {0} - {1}", ITEMNAME, ITEMDESC);
                }
            );

            // Let the user type in the item name to get info for
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  Type item name to get more info, or Enter to skip: ");
            string SELECTEDITEM = Console.ReadLine();
            Console.ResetColor();

            // Print out info is character exists
            if (inventory.Exists(CHARNAME, SELECTEDITEM))
            {
                Console.WriteLine("");
                Console.Write("    Quantity: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "Quantity"));
                Console.Write("    Weight: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "Weight"));
                Console.WriteLine("\n");
                Console.Write("    STR modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "STRModifier"));
                Console.Write("    VIG modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "VIGModifier"));
                Console.Write("    AGI modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "AGIModifier"));
                Console.WriteLine("");
                Console.Write("    INT modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "INTModifier"));
                Console.Write("    PER modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "PERModifier"));
                Console.Write("    TEN modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "TENModifier"));
                Console.WriteLine("");
                Console.Write("    CHA modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "CHAModifier"));
                Console.Write("    INS modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "INSModifier"));
                Console.Write("    COM modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "COMModifier"));
                Console.WriteLine("\n");
                Console.Write("    HP Boost: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "HPBoost"));
                Console.Write("    Mana Boost: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "ManaBoost"));
                Console.Write("    Stamina Boost: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "StaminaBoost"));
                Console.WriteLine("\n");
                Console.Write("    Weapon Value: {0}",inventory.GetStat(CHARNAME, SELECTEDITEM, "WeaponValue"));
                Console.Write("    Armor Value: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, "ArmorValue"));
                Console.WriteLine("\n");
                Console.Write("    Item lasts for {0} turns", inventory.GetStat(CHARNAME, SELECTEDITEM, "LastsTurns"));
                Console.WriteLine("\n");

                // Ask to add, transfer or copy item
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  Press 'A' to add an item");
                Console.WriteLine("  Press 'T' to transfer item");
                //Console.WriteLine("  Press 'C' to copy item");
                Console.WriteLine("  Press 'Del' to remove item");
                Console.WriteLine("  Press any key to return");
                Console.ResetColor();

                // Detecting keystroke
                var menu = Console.ReadKey();
                Console.Write("\b \b\n");
                
                // Delete item
                if (menu.Key == ConsoleKey.Delete) inventory.Delete(CHARNAME, SELECTEDITEM);           
                
                // Copy or transfer item
                if (menu.Key == ConsoleKey.A) CreateInventory(CHARNAME);
                if (menu.Key == ConsoleKey.T || menu.Key == ConsoleKey.C)
                {
                    Console.Write("  Enter new owners name: ");
                    string NEWOWNER = Console.ReadLine();
                    if (character.Exists(NEWOWNER))
                    {
                        if (menu.Key == ConsoleKey.T) inventory.Transfer(SELECTEDITEM, CHARNAME, NEWOWNER);
                        //if (menu.Key == ConsoleKey.C) inventory.Copy(SELECTEDITEM, CHARNAME, NEWOWNER);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  No such character. ");
                        Console.WriteLine("");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                }
            }

            // Notify user if item doesn't exist
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("");
                Console.WriteLine("  No such item. ");
                Console.WriteLine("");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  Press 'A' to add an item");
                Console.WriteLine("  Press any key to return");
                Console.ResetColor();

                var menu = Console.ReadKey();
                if (menu.Key == ConsoleKey.A) CreateInventory(CHARNAME);
            }

            // Press any key to return
            return;
        }


        /// <summary>
        /// Converts the text input into a float shows an error message if not suceeded
        /// </summary>
        /// <returns></returns>
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
        static Inventory inventory = new Inventory();
        static RollGenerator dice = new RollGenerator();
        
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
