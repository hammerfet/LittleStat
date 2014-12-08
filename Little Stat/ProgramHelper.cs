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
            Console.Write("Enter AGILITY value: ");
            AGILITY = StringConv();

            character.CreateChar(name, type, STRENGTH, VIGOUR, AGILITY, INTELLECT, PERCEPTION, TENACITY, CHARISMA, INSTINCT, COMMUNICATION);
        }

        public void DisplayChar()
        {
            Console.WriteLine("Current list of characters are:..");
            character.PrintChar();
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
