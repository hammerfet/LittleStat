using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Little_Stat
{
    class Counter
    {
        /// <summary>
        /// Like Next turn but applies to all players
        /// </summary>
        public void AllNextTurn()
        {
            var CharList = character.List();
            CharList.ForEach(delegate(String CHARNAME)
            {
                NextTurn(CHARNAME);
            }
            );
        }


        /// <summary>
        /// Drops all the "lasts turns" values for all items & effects.
        /// If 1, the item expires and moves to GM ownership.
        /// items with 0 turns aren't effected.
        /// </summary>
        public void NextTurn(string CHARNAME)
        {
            var ItemList = inventory.List(CHARNAME, Stat.Effect);
            ItemList.ForEach(delegate(String ITEMNAME)
            {
                int CurrentTurn = (int) inventory.GetStat(CHARNAME, ITEMNAME, Stat.LastsTurns);

                if (CurrentTurn == 1)
                {
                    inventory.Transfer(ITEMNAME, CHARNAME, "GM");
                }

                else if (CurrentTurn > 1)
                {
                    inventory.SetStat(CHARNAME, ITEMNAME, Stat.LastsTurns, CurrentTurn - 1);
                }
            }
            );
        }


        /*
         * No more methods here
         */


        /*
         * Variables
         */

        Inventory inventory = new Inventory();
        Character character = new Character();
    }
}
