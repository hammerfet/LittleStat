using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Little_Stat
{
    class Combat
    {
        public bool CanFight(string FIRSTCHAR, string SECONDCHAR)
        {

            if (character.Exists(FIRSTCHAR) && character.Exists(SECONDCHAR))
            {
                    // TODO: check char HP & stamina > 0
                //if()
                //{ 
                  //  return 1;
                //}
            }

            return false;

        }

        public float UseWeapon(string FIRSTCHAR, out float AoE)
        {
            float Attack = 0;
            
            // Set primary weapon as type of attack
            Stat TYPE = Stat.Ranged;
 
            switch (TYPE)
            {
                case Stat.Heavy:
                    Attack += inventory.GetTotal(FIRSTCHAR, Stat.Attack);   // Attack
                    Attack += inventory.GetTotal(FIRSTCHAR, Stat.Strength); // Total Strength
                    Attack += character.GetStat(FIRSTCHAR, Stat.Strength);
                    break;

                case Stat.Light:
                    Attack += inventory.GetTotal(FIRSTCHAR, Stat.Attack);     // Attack
                    Attack += inventory.GetTotal(FIRSTCHAR, Stat.Agility);    // Total Agility
                    Attack += character.GetStat(FIRSTCHAR, Stat.Agility);
                    Attack += inventory.GetTotal(FIRSTCHAR, Stat.Perception); // Total Perception
                    Attack += character.GetStat(FIRSTCHAR, Stat.Perception);
                    Attack -= inventory.GetTotal(FIRSTCHAR, Stat.Weight);     // Negate Weight
                    break;

                case Stat.Ranged:
                    Attack += inventory.GetTotal(FIRSTCHAR, Stat.Attack); // TODO: Make this a ranged attack variable in DB
                    Attack += inventory.GetTotal(FIRSTCHAR, Stat.Perception); // Total Perception
                    Attack += character.GetStat(FIRSTCHAR, Stat.Perception);
                    Attack += inventory.GetTotal(FIRSTCHAR, Stat.Instinct); // Total Instinct
                    Attack += character.GetStat(FIRSTCHAR, Stat.Instinct);
                    break;

            }
            AoE = inventory.GetTotal(FIRSTCHAR, Stat.AoERadius);
            return dice.Roll(Attack);
        }

        public float DefenceTest(string SECONDCHAR, Conflict FACING)
        {
            float Defence = 0;

            switch (FACING)
            {
                case Conflict.FromFront:
                    Defence = inventory.GetTotal(SECONDCHAR, Stat.Defence);
                    break;

                case Conflict.FromSide:
                    break;

                case Conflict.FromBehind:
                    break;

            }
            return dice.Roll(Defence);
        }


        public float Test(float ATTACK, float DEFENCE)
        {
            float result = ATTACK - DEFENCE;

            // TODO: Scale to defenders constitution
            // TODO: apply attack value to health
            
            if (result > 0) { return result; }
            else { return 0; }

        }


        public float ConsumeItem(string ITEMNAME, string CHARNAME)
        {

            return 0;
        }

        static Character character = new Character();
        static Inventory inventory = new Inventory();
        static Feats feats = new Feats();
        static RollGenerator dice = new RollGenerator();
    }
}
