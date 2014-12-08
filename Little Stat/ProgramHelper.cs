using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Stat
{
    class ProgramHelper
    {
        public void CreateChar(string type)
        {
            string name;
            Console.Write("Enter PC name: ");
            name = Console.ReadLine();
            Console.Write("Enter STRENGTH value: ");
            STRENGTH = StringConv();
            Console.Write("Enter VIGOUR value: ");
            VIGOUR = StringConv();
            Console.Write("Enter AGILITY value: ");
            AGILITY = StringConv();
            Console.Write("Enter INTELLECT value: ");
            INTELLECT = StringConv();
            Console.Write("Enter PERCEPTION value: ");
            PERCEPTION = StringConv();
            Console.Write("Enter TENACITY value: ");
            TENACITY = StringConv();
            Console.Write("Enter CHARISMA value: ");
            CHARISMA = StringConv();
            Console.Write("Enter INSTINCT value: ");
            INSTINCT = StringConv();
            Console.Write("Enter COMMUNICATION value: ");
            COMMUNICATION = StringConv();

            character.CreateChar(name, type, STRENGTH, VIGOUR, AGILITY, INTELLECT, PERCEPTION, TENACITY, CHARISMA, INSTINCT, COMMUNICATION);
        }

        public void DisplayChar()
        {
            Console.WriteLine("Current list of characters are:..");
            foreach (KeyValuePair<string, string> pair in character.CharName)
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
