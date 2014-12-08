using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Stat
{
    class Character
    {
        //Constructor Method
        public Character()
        {
            
        }

        public void CreateChar(string name, string type, float STRENGTH, float VIGOUR, float AGILITY, float INTELLECT,
            float PERCEPTION, float TENACITY, float CHARISMA, float INSTINCT, float COMMUNICATION)
        {
            CharName.Add(name, type);
            CharStrength.Add(name, STRENGTH);
            CharAgility.Add(name, AGILITY);
            CharVigour.Add(name, AGILITY);

            //MAXHP = ((BODY * 3) + (MIND * 2) + SOUL) / 3;
            //ATTACK = WEAPON + WEAPONCHAR;
            //PHYSICALDEFENCE = AGILITY + VIGOUR + INSTINCT + ARMOUR;
            //MENTALDEFENCE = TENACITY + INTELLECT + INSTINCT;
            //REACTION = INTELLECT + PERCEPTION + INSTINCT;
            //MOVEMENT = AGILITY + VIGOUR;
            //ENCUMBRANCE = STRENGTH + STRENGTH + VIGOUR;
            //FUROR = (VIGOUR + INSTINCT + TENACITY) / 2;
            // EXP = ?;
        }

        public void PrintChar()
        {
            foreach(KeyValuePair<string, string> pair in CharName){
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
            }

            Console.WriteLine("Enter name to view more information..");

            string name = Console.ReadLine();
            
            if(CharName.ContainsKey(name)){
                float val;
                CharStrength.TryGetValue(name, out val);
                Console.WriteLine("STRENGTH is: {0}", val);
            }
         
            else
            {
                Console.WriteLine("That character does not exist..");
            }
            
                        
        }

        // Variables
        private Dictionary<string, string> CharName = new Dictionary<string, string>();
        private Dictionary<string, float> CharStrength = new Dictionary<string, float>();
        private Dictionary<string, float> CharVigour = new Dictionary<string, float>();
        private Dictionary<string, float> CharAgility = new Dictionary<string, float>();
        private Dictionary<string, float> CharIntellect = new Dictionary<string, float>();

        private float BODY;
        private float MIND;
        private float SOUL;
        private float MAXHP;
        private float ATTACK;
        private float PHYSICALDEFENCE;
        private float MENTALDEFENCE;
        private float REACTION;
        private float MOVEMENT;
        private float ENCUMBRANCE;
        private float FUROR;
        private float EXP;
    }
}
