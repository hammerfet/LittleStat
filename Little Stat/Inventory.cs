using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Little_Stat
{
    class Inventory
    {
        SQLiteConnection db = new SQLiteConnection(@"Data Source=..\..\LittleStat.s3db");


        /// <summary>
        /// Adds item to the database or increments
        /// quantity of existing same item
        /// </summary>
        /// <param name="CHARNAME">name of character to add item to</param>
        /// <param name="ITEMNAME">name of item</param>
        public void AddItem(string CHARNAME, string ITEMNAME)
        {
            
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Quantity FROM Inventory WHERE BelongsTo = @BelongsTo AND ItemName = @ItemName", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@BelongsTo", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@ItemName", ITEMNAME));

                db.Open();
                int quantity = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();
                cmd.Parameters.Add(new SQLiteParameter("@NewQuantity", quantity + 1));

                if(quantity > 0)    cmd.CommandText = "UPDATE Inventory SET Quantity = @NewQuantity WHERE BelongsTo = @BelongsTo AND ItemName = @ItemName";
                else                cmd.CommandText = "INSERT INTO Inventory (ItemName, BelongsTo) VALUES (@ItemName, @BelongsTo)";

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();

            }
        }


        /// <summary>
        /// Removes item from database by decrementing
        /// quantity or deleting item
        /// </summary>
        /// <param name="CHARNAME">nane of character to remove item from</param>
        /// <param name="ITEMNAME">name of item</param>
        public void RemoveItem(string CHARNAME, string ITEMNAME)
        {

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Quantity FROM Inventory WHERE BelongsTo = @BelongsTo AND ItemName = @ItemName", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@BelongsTo", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@ItemName", ITEMNAME));

                db.Open();
                int quantity = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();
                cmd.Parameters.Add(new SQLiteParameter("@NewQuantity", quantity - 1));

                if (quantity > 1)   cmd.CommandText = "UPDATE Inventory SET Quantity = @NewQuantity WHERE BelongsTo = @BelongsTo AND ItemName = @ItemName";
                else                cmd.CommandText = "DELETE FROM Inventory WHERE BelongsTo = @BelongsTo AND ItemName = @ItemName";

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();

            }
        }


        /// <summary>
        /// Checks if item exists
        /// </summary>
        /// <param name="CHARNAME">name of character</param>
        /// <param name="ITEMNAME">name of item</param>
        /// <returns></returns>
        public bool Exists(string CHARNAME, string ITEMNAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Quantity FROM Inventory WHERE BelongsTo = @BelongsTo AND ItemName = @ItemName", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@BelongsTo", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@ItemName", ITEMNAME));

                db.Open();
                int quantity = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();

                if (quantity > 0) return true;
                else return false;
            }
        }
        

        /// <summary>
        /// Returns a list of items held by a character
        /// </summary>
        /// <returns>List containing item names</returns>
        public List<string> ListItems(string CHARNAME)
        {
            List<String> itemNameList = new List<string>();

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT ItemName FROM Inventory where BelongsTo = @BelongsTo", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@BelongsTo", CHARNAME));
                db.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    itemNameList.Add(reader.GetString(i));
                }
                reader.Close();
                db.Close();
            }
            return itemNameList;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CHARNAME"></param>
        /// <param name="ITEMNAME"></param>
        /// <param name="STAT"></param>
        /// <param name="value"></param>
        public void SetItemStats(string CHARNAME, string ITEMNAME, string STAT, float value)
        {
            string str = string.Format("UPDATE Inventory SET {0} = @value WHERE ItemName = @ItemName AND BelongsTo = @BelongsTo", STAT);
            using (SQLiteCommand cmd = new SQLiteCommand(str, db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@ItemName", ITEMNAME));
                cmd.Parameters.Add(new SQLiteParameter("@BelongsTo", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@value", value));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="NAME"></param>
        /// <param name="STAT"></param>
        /// <returns></returns>
        public float ReturnStat(string NAME, string STAT)
        {
            float result = 0;
            string str = "";

            switch (STAT)
            {
                case "Strength": // All these stats are base stats, the can be called directly from the db
                case "Vigour":
                case "Agility":
                case "Intellect":
                case "Perception":
                case "Tenacity":
                case "Charisma":
                case "Instinct":
                case "Communication":
                case "CurrentHP":
                case "CurrentMana":
                case "CurrentStamina":
                case "EXP":
                    str = string.Format("SELECT {0} FROM MajorStats WHERE Name = '{1}'", STAT, NAME);
                    break;

                /*
                 * Max hit points calculation
                 * 
                 * Mased mainly on phisical stats
                 */
                case "MaxHP":
                    str = string.Format("SELECT ((Strength + Vigour + Agility) * 3) + ((Intellect + Perception + Tenacity) * 2) + (Charisma + Instinct + Communication) FROM MajorStats WHERE Name = '{0}'", NAME);
                    break;

                /*
                 * Max mana calculation
                 * 
                 * Base mainly on Mental stats
                 */
                case "MaxMana":
                    str = string.Format("SELECT Vigour + Intellect + Tenacity FROM MajorStats WHERE Name = '{0}'", NAME);
                    break;

                /*
                 * Max stamina calculation
                 * 
                 * Base on phisical stats
                 */
                case "MaxStamina":
                    str = string.Format("SELECT Strength + Agility + Vigour FROM MajorStats WHERE Name = '{0}'", NAME);
                    break;

                /*
                 * Movement calculation
                 * 
                 * Movement = AGI + VIG
                 */
                case "Movement":
                    str = string.Format("SELECT Agility + Vigour + Instinct FROM MajorStats WHERE Name = '{0}'", NAME);
                    break;

                /*
                 * Phys defence or Fortitude calculation
                 * 
                 * Fortitude = AGI + VIG + INS
                 */
                case "PhysicalDefence":
                case "Fortitude":
                    str = string.Format("SELECT Agility + Vigour + Instinct FROM MajorStats WHERE Name = '{0}'", NAME);
                    break;

                /*
                 * Men defence or Will calculation
                 * 
                 * Will = TEN + INT + INS
                 */
                case "MentalDefence":
                case "Will":
                    str = string.Format("SELECT Tenacity + Intellect + Instinct FROM MajorStats WHERE Name = '{0}'", NAME);
                    break;

                /*
                 * Reaction calculation
                 * 
                 * Reaction = INT + PER + INS
                 */
                case "Reaction":
                    str = string.Format("SELECT Intellect + Perception + Instinct FROM MajorStats WHERE Name = '{0}'", NAME);
                    break;

                /*
                 * Encumbrance calculation
                 * 
                 * Encumbrance = STR + STR + VIG
                 */
                case "MaxEncumberance":
                    str = string.Format("SELECT Strength + Strength + Vigour FROM MajorStats WHERE Name = '{0}'", NAME);
                    break;


            }

            using (SQLiteCommand cmd = new SQLiteCommand(str, db))
            {
                db.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();
            }
            return result;
        }


        /*
         * No more methods here
         */

        /*
         * No local variables
         */
    }
}
