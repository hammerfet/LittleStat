using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Stat
{
    class ProgramHelper
    {
        public void CreateChar(string TYPE)
        {
            Console.Write("Enter PC name: ");
            string name = Console.ReadLine();

            character.SetCharStats(name, TYPE, 0);

            Console.Write("Enter STRENGTH value: ");
            character.SetCharStats(name, "STRENGTH", StringConv());
            
            Console.Write("Enter VIGOUR value: ");
            character.SetCharStats(name, "VIGOUR", StringConv());
            
            Console.Write("Enter AGILITY value: ");
            character.SetCharStats(name, "AGILITY", StringConv());
            
            Console.Write("Enter INTELLECT value: ");
            character.SetCharStats(name, "INTELLECT", StringConv());
            
            Console.Write("Enter PERCEPTION value: ");
            character.SetCharStats(name, "PERCEPTION", StringConv());
            
            Console.Write("Enter TENACITY value: ");
            character.SetCharStats(name, "TENACITY", StringConv());
            
            Console.Write("Enter CHARISMA value: ");
            character.SetCharStats(name, "CHARISMA", StringConv());
            
            Console.Write("Enter INSTINCT value: ");
            character.SetCharStats(name, "INSTINCT", StringConv());
            
            Console.Write("Enter COMMUNICATION value: ");
            character.SetCharStats(name, "COMMUNICATION", StringConv());
        }

        public void DisplayChar()
        {
            Console.WriteLine("Current list of characters are:..");
            foreach (KeyValuePair<string, string> pair in character.CharType)
            {
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
            }

            Console.WriteLine("Enter name to view more information..");
            
            if (character.GetCharStats(Console.ReadLine(), out STRENGTH, out VIGOUR))
            {
                Console.WriteLine("STRENGTH is : {0}", STRENGTH);
                Console.WriteLine("VIGOUR is: {0}", VIGOUR);
            }
            
            else
            {
                Console.WriteLine("That character does not exist..");
            }

            Console.WriteLine("Press any key to return..");
            Console.ReadKey();
        }

        private float StringConv()
        {
            while (true)
            {
                float res;

                if (float.TryParse(Console.ReadLine(), out res)) return res;

                else
                {
                    Console.WriteLine("Input has to be a number, try again: ");
                }
            }

        }

        Character character = new Character();
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
