using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Stat
{
    public enum Stat
    {
        /* Primary stats for chars and items */
        Strength,
        Agility,
        Constitution,
        Intellect,
        Perception,
        Tenacity,
        Charisma,
        Instinct,
        Communication,

        /* Secondary stats  for chars only */
        Movement,
        Fortitude,
        Will,
        Reaction,

        /* Variables */
        HP,
        Mana,
        Stamina,
        EXP,

        /* Item related */
        Attack,
        Defence,
        Type,
        Triggered,
        AoERadius,      
        
        Description,
        Quantity,
        Weight,
        LastsTurns,

        /* Item types */
        Effect,
        Useable,
        Feat,
        CombatItem,

        /* Weapon types */
        Heavy,
        Light,
        Ranged,
        Magic,

        /* Feat related */
        ManaCost,
        StaminaCost
    };

    public enum Conflict
    {
        HeavyAttack,
        QuickAttack,
        RangedAttack,
        StealthAttack,

        FromFront,
        FromSide,
        FromBehind,
        FromAbove,
        FromBelow,
        FromFar
    }

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
                Console.WriteLine("    7 - ");
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
                        counter.NextTurn("Adi");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        Console.Clear();
                        Console.WriteLine("\n\n    Result: {0}", dice.Roll(100));
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        Combat1v1();
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
            character.SetStat(name, Stat.Strength, GetFloatFromConsole());

            Console.Write("    Enter AGILITY value: ");
            character.SetStat(name, Stat.Agility, GetFloatFromConsole());

            Console.Write("    Enter CONSTITUTION value: ");
            character.SetStat(name, Stat.Constitution, GetFloatFromConsole());

            Console.Write("    Enter INTELLECT value: ");
            character.SetStat(name, Stat.Intellect, GetFloatFromConsole());

            Console.Write("    Enter PERCEPTION value: ");
            character.SetStat(name, Stat.Perception, GetFloatFromConsole());

            Console.Write("    Enter TENACITY value: ");
            character.SetStat(name, Stat.Tenacity, GetFloatFromConsole());

            Console.Write("    Enter CHARISMA value: ");
            character.SetStat(name, Stat.Charisma, GetFloatFromConsole());

            Console.Write("    Enter INSTINCT value: ");
            character.SetStat(name, Stat.Instinct, GetFloatFromConsole());

            Console.Write("    Enter COMMUNICATION value: ");
            character.SetStat(name, Stat.Communication, GetFloatFromConsole());
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
                Console.Write("    HP = {0}%", character.GetStat(characterName, Stat.HP));
                Console.Write("    Mana = {0}%", character.GetStat(characterName, Stat.Mana));
                Console.Write("    Stamina = {0}%", character.GetStat(characterName, Stat.Stamina));
                Console.Write("    Experience: {0}", character.GetStat(characterName, Stat.EXP));
                Console.WriteLine("\n");
                Console.Write("    Strength: {0} ({1})", character.GetStat(characterName, Stat.Strength), inventory.GetTotal(characterName, Stat.Strength));
                Console.Write("    Agility: {0}", character.GetStat(characterName, Stat.Agility));
                Console.Write("       Constitution: {0}", character.GetStat(characterName, Stat.Constitution));
                Console.WriteLine("");
                Console.Write("    Intellect: {0}", character.GetStat(characterName, Stat.Intellect));
                Console.Write("   Perception: {0}", character.GetStat(characterName, Stat.Perception));
                Console.Write("    Tenacity: {0}", character.GetStat(characterName, Stat.Tenacity));
                Console.WriteLine("");
                Console.Write("    Charisma: {0}", character.GetStat(characterName, Stat.Charisma));
                Console.Write("    Instinct: {0}", character.GetStat(characterName, Stat.Instinct));
                Console.Write("      Communication: {0}", character.GetStat(characterName, Stat.Communication));
                Console.WriteLine("\n");
                Console.Write("    Movement: {0}", character.GetStat(characterName, Stat.Movement));
                Console.Write("   Reaction: {0}", character.GetStat(characterName, Stat.Reaction));
                Console.Write("    Fortitude: {0}", character.GetStat(characterName, Stat.Fortitude));
                Console.Write("   Will: {0}", character.GetStat(characterName, Stat.Will));
                Console.WriteLine("\n");
            }
            
            // Notify user if character doesnt exist
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("");
                Console.WriteLine("  No such character. Press any key to return");
                Console.WriteLine("");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            // Press any key to return
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("  Press 'I' to list usable items");
            Console.WriteLine("  Press 'F' to display character feats");
            Console.WriteLine("  Press 'E' to display character effects");
            Console.WriteLine("  Press 'C' to display combat items");
            Console.WriteLine("  Press 'Del' to remove character");
            Console.WriteLine("  Press any key to return");
            Console.ResetColor();
            var menu = Console.ReadKey();
            if (menu.Key == ConsoleKey.Delete) DeleteChar(characterName);
            if (menu.Key == ConsoleKey.I) DisplayInventory(characterName, Stat.Useable);
            if (menu.Key == ConsoleKey.F) DisplayInventory(characterName, Stat.Feat);
            if (menu.Key == ConsoleKey.E) DisplayInventory(characterName, Stat.Effect);
            if (menu.Key == ConsoleKey.C) DisplayInventory(characterName, Stat.CombatItem);

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
        static void CreateInventory(string CHARNAME, Stat ITEMTYPE)
        {
            // Header
            Console.Clear();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Create new Item/Effect/Feat");
            Console.WriteLine("  ---------------");
            Console.ResetColor();
            Console.WriteLine("");
            Console.Write("    Enter name: ");

            // Get name and check string length
            string ITEMNAME = Console.ReadLine();
            if (ITEMNAME.Length > 30 || ITEMNAME.Length < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n    Name has to be between than 0 and 30 characters, \n    Press any key to return..");
                Console.ResetColor();
                var menu = Console.ReadKey();
                return;
            }

            // Check if character exists
            if (inventory.Exists(CHARNAME, ITEMNAME))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n    Name already exists, \n    Press Enter to overwrite or any key to return..\n");
                Console.ResetColor();
                var menu = Console.ReadKey();
                if (menu.Key != ConsoleKey.Enter) return;
            }

            // Create item if it doesn't exist and set defult quantity to 1
            else inventory.Create(CHARNAME, ITEMNAME);

            // Finally write or overwite character stats
            Console.Write("    Enter description: ");
            inventory.SetDescription(CHARNAME, ITEMNAME, Console.ReadLine());

            switch (ITEMTYPE)
            {
                case Stat.Useable:
                    Console.Write("    Enter QUANTITY: ");
                    inventory.SetStat(CHARNAME, ITEMNAME, Stat.Quantity, GetFloatFromConsole());

                    Console.Write("    Enter WEIGHT: ");
                    inventory.SetStat(CHARNAME, ITEMNAME, Stat.Weight, GetFloatFromConsole());

                    Console.Write("    Enter how many TURNS the item lasts (0 for inf): ");
                    inventory.SetStat(CHARNAME, ITEMNAME, Stat.LastsTurns, GetFloatFromConsole());
                    
                    inventory.SetStat(CHARNAME, ITEMNAME, Stat.Triggered, 1);

                    inventory.SetType(CHARNAME, ITEMNAME, Stat.Useable);
                    break;

                case Stat.Feat:
                    Console.Write("    Enter how many TURNS the feat lasts (0 for inf): ");
                    inventory.SetStat(CHARNAME, ITEMNAME, Stat.LastsTurns, GetFloatFromConsole());
                    
                    Console.Write("    Enter Stamina cost: ");
                    inventory.SetStat(CHARNAME, ITEMNAME, Stat.StaminaCost, GetFloatFromConsole());
                    
                    Console.Write("    Enter Mana cost: ");
                    inventory.SetStat(CHARNAME, ITEMNAME, Stat.ManaCost, GetFloatFromConsole());

                    inventory.SetType(CHARNAME, ITEMNAME, Stat.Feat);
                    break;

                case Stat.Effect:
                    Console.Write("    Enter how many TURNS the effect lasts (0 for inf): ");
                    inventory.SetStat(CHARNAME, ITEMNAME, Stat.LastsTurns, GetFloatFromConsole());

                    inventory.SetType(CHARNAME, ITEMNAME, Stat.Effect);
                    break;

                case Stat.CombatItem:
                    Console.Write("    Enter ATTACK power: ");
                    inventory.SetStat(CHARNAME, ITEMNAME, Stat.Attack, GetFloatFromConsole());

                    Console.Write("    Enter Defence value: ");
                    inventory.SetStat(CHARNAME, ITEMNAME, Stat.Defence, GetFloatFromConsole());

                    Console.Write("    Enter weapon Type: ");
                    inventory.SetType(CHARNAME, ITEMNAME, GetWeaponTypeFromConsole());
                    break;
            }

            Console.Write("    Enter STRENGTH modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, Stat.Strength, GetFloatFromConsole());

            Console.Write("    Enter AGILITY modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, Stat.Agility, GetFloatFromConsole());

            Console.Write("    Enter CONSTITUTION modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, Stat.Constitution, GetFloatFromConsole());

            Console.Write("    Enter INTELLECT modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, Stat.Intellect, GetFloatFromConsole());

            Console.Write("    Enter PERCEPTION modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, Stat.Perception, GetFloatFromConsole());

            Console.Write("    Enter TENACITY modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, Stat.Tenacity, GetFloatFromConsole());

            Console.Write("    Enter CHARISMA modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, Stat.Charisma, GetFloatFromConsole());

            Console.Write("    Enter INSTINCT modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, Stat.Instinct, GetFloatFromConsole());

            Console.Write("    Enter COMMUNICATION modifier: ");
            inventory.SetStat(CHARNAME, ITEMNAME, Stat.Communication, GetFloatFromConsole());
        }


        /// <summary>
        /// Lists the Inventory of a character, allows adding
        /// deleting, transfering or copying of the item
        /// </summary>
        /// <param name="NAME">Name of character</param>
        static void DisplayInventory(string CHARNAME, Stat TYPE)
        {
            // Header
            Console.Clear();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  {0}s belonging to: {1}", TYPE, CHARNAME);
            Console.WriteLine("  -------------------");
            Console.ResetColor();
            Console.WriteLine("");

            // Get list of items
            var ItemList = inventory.List(CHARNAME, TYPE);
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
                switch (TYPE)
                {
                    case Stat.Useable:
                        Console.Write("    Quantity: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Quantity));
                        Console.Write("    Weight: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Weight));
                        Console.Write("    AoE Radius: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.AoERadius));
                        break;

                    case Stat.Effect:
                        Console.Write("    Effect lasts for {0} turns", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.LastsTurns));
                        Console.Write("    AoE Radius: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.AoERadius));
                        break;

                    case Stat.Feat:
                        Console.Write("    Feat lasts for {0} turns", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.LastsTurns));
                        Console.Write("    Stamina/Mana cost: {0}:{1}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.StaminaCost), inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.ManaCost)); 
                        Console.Write("    AoE Radius: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.AoERadius));
                        break;

                    case Stat.CombatItem:
                        Console.Write("    Attack power: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Attack));
                        Console.Write("    Defence value: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Defence));
                        Console.Write("    AoE Radius: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.AoERadius));

                        break;
                }

                Console.WriteLine("\n");
                Console.Write("    STR modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Strength));
                Console.Write("    AGI modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Agility));
                Console.Write("    CON modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Constitution));
                Console.WriteLine("");
                Console.Write("    INT modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Intellect));
                Console.Write("    PER modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Perception));
                Console.Write("    TEN modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Tenacity));
                Console.WriteLine("");
                Console.Write("    CHA modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Charisma));
                Console.Write("    INS modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Instinct));
                Console.Write("    COM modifier: {0}", inventory.GetStat(CHARNAME, SELECTEDITEM, Stat.Communication));
                Console.WriteLine("\n");

                // Ask to add, transfer or copy item
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  Press 'A' to add");
                Console.WriteLine("  Press 'T' to transfer");
                Console.WriteLine("  Press 'C' to copy item (not implemented yet)");
                Console.WriteLine("  Press 'Del' to remove");
                Console.WriteLine("  Press any key to return");
                Console.ResetColor();

                // Detecting keystroke
                var menu = Console.ReadKey();
                Console.Write("\b \b\n");
                
                // Delete item
                if (menu.Key == ConsoleKey.Delete) inventory.Delete(CHARNAME, SELECTEDITEM);           
                
                // Copy or transfer item
                if (menu.Key == ConsoleKey.A) CreateInventory(CHARNAME, TYPE);
                if (menu.Key == ConsoleKey.T || menu.Key == ConsoleKey.C)
                {
                    Console.Write("  Enter new owners name: ");
                    string NEWOWNER = Console.ReadLine();
                    if (character.Exists(NEWOWNER))
                    {
                        if (menu.Key == ConsoleKey.T) inventory.Transfer(SELECTEDITEM, CHARNAME, NEWOWNER);
                        if (menu.Key == ConsoleKey.C) inventory.Copy(SELECTEDITEM, CHARNAME, NEWOWNER);
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
                Console.WriteLine("  No such {0}.", TYPE);
                Console.WriteLine("");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  Press 'A' to add");
                Console.WriteLine("  Press any key to return");
                Console.ResetColor();

                var menu = Console.ReadKey();
                if (menu.Key == ConsoleKey.A) CreateInventory(CHARNAME, TYPE);
            }

            // Press any key to return
            return;
        }


        /// <summary>
        /// Converts the text input into a float shows an error message if not suceeded
        /// </summary>
        /// <returns>/floating value</returns>
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


        /// <summary>
        /// Gets the weapon type from the console
        /// </summary>
        /// <returns>The type of weapon defined under the Stat enum</returns>
        static Stat GetWeaponTypeFromConsole()
        {
            while (true)
            {
                string type = Console.ReadLine();

                switch (type)
                {
                    case "Light":
                    case "light":
                    case "L":
                    case "l":
                        return Stat.Light;

                    case "Heavy":
                    case "heavy":
                    case "H":
                    case "h":
                        return Stat.Heavy;

                    case "Ranged":
                    case "ranged":
                    case "R":
                    case "r":
                        return Stat.Heavy;

                    case "Magic":
                    case "magic":
                    case "M":
                    case "m":
                        return Stat.Heavy;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("    Input has to be heavy, light, ranged or magic , try again: ");
                        Console.ResetColor();
                        break;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        static void Combat1v1()
        {
            // Header
            Console.Clear();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Following characters available for combat");
            Console.WriteLine("  -----------------------------------------");
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
            Console.Write("  Select first character: ");
            string FIRSTCHAR = Console.ReadLine();
            Console.Write("  Select second character: ");
            string SECONDCHAR = Console.ReadLine();
            Console.ResetColor();

            // Print out info is character exists
            if(combat.CanFight(FIRSTCHAR, SECONDCHAR))
            {
                float AoE = 0;
                float att = combat.UseWeapon(FIRSTCHAR, out AoE);
                float def = combat.DefenceTest(FIRSTCHAR, Conflict.FromFront);

                float res = combat.Test(att, def);
                Console.WriteLine(res);
                Console.ReadKey();

            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n    Invalid selection. Press any key to return..\n");
                Console.ResetColor();
                var menu = Console.ReadKey();
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
        static Feats feats = new Feats();
        static RollGenerator dice = new RollGenerator();
        static Combat combat = new Combat();
        static Counter counter = new Counter();
        
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
