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
            CharVigour.Add(name, VIGOUR);
            CharIntellect.Add(name, INTELLECT);
            CharPerception.Add(name, PERCEPTION);
            CharTenacity.Add(name, TENACITY);
            CharCharisma.Add(name, CHARISMA);
            CharInstinct.Add(name, INSTINCT);
            CharCommunication.Add(name, COMMUNICATION);

            UpdateStats();
        }

        private void UpdateStats()
        {
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

        public bool PrintChar()
        {
            foreach(KeyValuePair<string, string> pair in CharName){
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
            }

            Console.WriteLine("Enter name to view more information..");

            string name = Console.ReadLine();

            if (CharName.ContainsKey(name))
            {
                float val;
                CharStrength.TryGetValue(name, out val);
                Console.WriteLine("STRENGTH is: {0}", val);
                return true;
            }
            return false;
                        
        }

        // Variables
        private Dictionary<string, string> CharName = new Dictionary<string, string>();
        private Dictionary<string, float> CharStrength = new Dictionary<string, float>();
        private Dictionary<string, float> CharVigour = new Dictionary<string, float>();
        private Dictionary<string, float> CharAgility = new Dictionary<string, float>();
        private Dictionary<string, float> CharIntellect = new Dictionary<string, float>();
        private Dictionary<string, float> CharPerception = new Dictionary<string, float>();
        private Dictionary<string, float> CharTenacity = new Dictionary<string, float>();
        private Dictionary<string, float> CharCharisma = new Dictionary<string, float>();
        private Dictionary<string, float> CharInstinct = new Dictionary<string, float>();
        private Dictionary<string, float> CharCommunication = new Dictionary<string, float>();

        private Dictionary<string, float> CharMaxHP = new Dictionary<string, float>();
        private Dictionary<string, float> CharCurrentHP = new Dictionary<string, float>();
        private Dictionary<string, float> CharAttack = new Dictionary<string, float>();
        private Dictionary<string, float> CharPhysDefence = new Dictionary<string, float>();
        private Dictionary<string, float> CharMenDefence = new Dictionary<string, float>();
        private Dictionary<string, float> CharReaction = new Dictionary<string, float>();
        private Dictionary<string, float> CharMovement = new Dictionary<string, float>();
        private Dictionary<string, float> CharEncumbrance = new Dictionary<string, float>();
        private Dictionary<string, float> CharFuror = new Dictionary<string, float>();
        private Dictionary<string, float> CharEXP = new Dictionary<string, float>();
    }
}
