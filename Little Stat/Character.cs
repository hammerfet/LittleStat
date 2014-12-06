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

        public void CreatePC(string name, string type)
        {
            CharName.Add(name, type);
            //public string[] BaseStatNames = { "STRENGTH", "VIGOUR", "AGILITY", "INTELLECT", "PERCEPTION", "TENACITY", "CHARISMA", "INSTINCT", "COMMUNICATION" };

            //float BODY = BaseStatValues[1] + BaseStatValues[2] + BaseStatValues[3];
            //float MIND = BaseStatValues[3] + BaseStatValues[4] + BaseStatValues[5];
            //float SOUL = BaseStatValues[6] + BaseStatValues[7] + BaseStatValues[8];

            //MAXHP = ((BODY * 3) + (MIND * 2) + SOUL) / 3;
            //ATTACK = WEAPON + WEAPONCHAR;
            //PHYSICALDEFENCE = AGILITY + VIGOUR + INSTINCT + ARMOUR;
            //MENTALDEFENCE = TENACITY + INTELLECT + INSTINCT;
            //REACTION = INTELLECT + PERCEPTION + INSTINCT;
            //MOVEMENT = AGILITY + VIGOUR;
            //ENCUMBRANCE = STRENGTH + STRENGTH + VIGOUR;
            //FUROR = (VIGOUR + INSTINCT + TENACITY) / 2;
        }

        public void PrintPC(){

            foreach(KeyValuePair<string, string> pair in CharName){
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
            }
            Console.ReadKey();
                        
        }

        // Variables
        private Dictionary<string, string> CharName = new Dictionary<string, string>();
        private Dictionary<string, string> CharStrength = new Dictionary<string, string>();
        private Dictionary<string, string> CharVigour = new Dictionary<string, string>();
        private Dictionary<string, string> CharAgility = new Dictionary<string, string>();
        private Dictionary<string, string> CharIntellect = new Dictionary<string, string>();

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
    }
}
