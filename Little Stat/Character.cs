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

        public void Create()
        {
            //public string[] BaseStatNames = { "STRENGTH", "VIGOUR", "AGILITY", "INTELLECT", "PERCEPTION", "TENACITY", "CHARISMA", "INSTINCT", "COMMUNICATION" };
            //public int[] BaseStatValues = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

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

        // Variables
        private Dictionary<string, string> chhar = new Dictionary<string, string>();

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
