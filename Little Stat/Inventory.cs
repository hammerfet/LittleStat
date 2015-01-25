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
        public void Create(string CHARNAME, string ITEMNAME) // TODO: Maybe remove the whole quantity thing
        {
            
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Quantity FROM Inventory WHERE Owner = @char AND Name = @item", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@char", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@item", ITEMNAME));

                db.Open();
                int quantity = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();
                cmd.Parameters.Add(new SQLiteParameter("@NewQuantity", quantity + 1));

                if(quantity > 0)    cmd.CommandText = "UPDATE Inventory SET Quantity = @NewQuantity WHERE Owner = @char AND Name = @item";
                else                cmd.CommandText = "INSERT INTO Inventory (Name, Owner, Quantity) VALUES (@item, @char, 1)";

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
        public void Delete(string CHARNAME, string ITEMNAME)
        {

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Quantity FROM Inventory WHERE Owner = @char AND Name = @item", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@char", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@item", ITEMNAME));

                db.Open();
                int quantity = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();
                cmd.Parameters.Add(new SQLiteParameter("@newQuantity", quantity - 1));

                if (quantity > 1)   cmd.CommandText = "UPDATE Inventory SET Quantity = @newQuantity WHERE Owner = @char AND Name = @item";
                else                cmd.CommandText = "DELETE FROM Inventory WHERE Owner = @char AND Name = @item";

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
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Quantity FROM Inventory WHERE Owner = @char AND Name = @item", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@char", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@item", ITEMNAME));

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
        public List<string> List(string CHARNAME, Stat TYPE)
        {
            List<String> itemNameList = new List<string>();

            string str = "SELECT Name FROM Inventory where Owner = @char AND Type = @type";

            // Only if item is combat type, then use this command
            if (TYPE == Stat.CombatItem) str = "SELECT Name FROM Inventory where Owner = @char AND (Type = @heavy OR Type = @light OR Type = @ranged OR Type = @magic)";
 
            using (SQLiteCommand cmd = new SQLiteCommand(str, db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@char", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@type", TYPE));
                cmd.Parameters.Add(new SQLiteParameter("@heavy", Stat.Heavy));
                cmd.Parameters.Add(new SQLiteParameter("@light", Stat.Light));
                cmd.Parameters.Add(new SQLiteParameter("@ranged", Stat.Ranged));
                cmd.Parameters.Add(new SQLiteParameter("@magic", Stat.Magic));
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
        /// Sets the stat of an item
        /// </summary>
        /// <param name="ITEMNAME">Name of item</param>
        /// <param name="STAT">Stat selection from enum</param>
        /// <param name="VALUE">Value to give</param>
        public void SetStat(string CHARNAME, string ITEMNAME, Stat STAT, float VALUE)
        {
            string str = "";

            switch (STAT)
            {
                case Stat.Strength:
                case Stat.Agility:
                case Stat.Constitution:
                case Stat.Intellect:
                case Stat.Perception:
                case Stat.Tenacity:
                case Stat.Charisma:
                case Stat.Instinct:
                case Stat.Communication:
                case Stat.Attack:
                case Stat.Defence:
                case Stat.Triggered:
                case Stat.AoERadius:
                case Stat.Quantity:
                case Stat.Weight:
                case Stat.LastsTurns:
                case Stat.ManaCost:
                case Stat.StaminaCost:
                    str = string.Format("UPDATE Inventory SET {0} = @value WHERE Name = @item AND Owner = @char", STAT);
                    break;

                default:
                    return;
            }

            using (SQLiteCommand cmd = new SQLiteCommand(str, db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@char", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@item", ITEMNAME));
                cmd.Parameters.Add(new SQLiteParameter("@value", VALUE));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Sets the description of an item. Doesn't
        /// care about owners.
        /// </summary>
        /// <param name="ITEMNAME"></param>
        /// <param name="DESC"></param>
        public void SetDescription(string CHARNAME, string ITEMNAME, string DESC)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Inventory SET Description = @desc WHERE Owner = @char AND Name = @item", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@char", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@item", ITEMNAME));
                cmd.Parameters.Add(new SQLiteParameter("@desc", DESC));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Sets the item type
        /// </summary>
        /// <param name="ITEMNAME">Name of item</param>
        /// <param name="TYPE">NAme of type</param>
        public void SetType(string CHARNAME, string ITEMNAME, Stat TYPE)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Inventory SET TYPE = @type WHERE Name = @item AND Owner = @char", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@char", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@item", ITEMNAME));
                cmd.Parameters.Add(new SQLiteParameter("@type", TYPE));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Returns the numerical modifier stat for an
        /// item of a specific character
        /// </summary>
        /// <param name="CHARNAME">Name of character</param>
        /// <param name="ITEMNAME">Name of item</param>
        /// <param name="STAT">stat to return</param>
        /// <returns>float value</returns>
        public float GetStat(string CHARNAME, string ITEMNAME, Stat STAT)
        {
            float result = 0;
            string str = "";

            switch (STAT)
            {
                case Stat.Strength:
                case Stat.Agility:
                case Stat.Constitution:
                case Stat.Intellect:
                case Stat.Perception:
                case Stat.Tenacity:
                case Stat.Charisma:
                case Stat.Instinct:
                case Stat.Communication:
                case Stat.Attack:
                case Stat.Defence:
                case Stat.Triggered:
                case Stat.AoERadius:
                case Stat.Quantity:
                case Stat.Weight:
                case Stat.LastsTurns:
                case Stat.ManaCost:
                case Stat.StaminaCost:
                    str = string.Format("SELECT {0} FROM Inventory WHERE Name = '{1}' AND Owner = '{2}'", STAT, ITEMNAME, CHARNAME);
                    break;

                default:
                    return 0;
            }

            using (SQLiteCommand cmd = new SQLiteCommand(str, db))
            {
                db.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();
            }
            return result;
        }


        /// <summary>
        /// Gets total effect buffs for a character including item bonuses
        /// </summary>
        /// <param name="CHARNAME"></param>
        /// <param name="STAT"></param>
        /// <returns></returns>
        public float GetTotal(string CHARNAME, Stat STAT)
        {
            float TotalStat = 0;
         
            var EffectList = List(CHARNAME, Stat.Effect);
            EffectList.ForEach(delegate(String ITEMNAME)
            {
                TotalStat += GetStat(CHARNAME, ITEMNAME, STAT);
            }
            );

            var WeaponList = List(CHARNAME, Stat.CombatItem);
            WeaponList.ForEach(delegate(String ITEMNAME)
            {
                TotalStat += GetStat(CHARNAME, ITEMNAME, STAT);
            }
            );

            return TotalStat;
        }


        /// <summary>
        /// Gets the description of the item
        /// </summary>
        /// <param name="CHARNAME">Name of character</param>
        /// <param name="ITEMNAME">Name of item</param>
        /// <returns></returns>
        public string GetDescription(string CHARNAME, string ITEMNAME)
        {
            string result = "";
           
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Description FROM Inventory WHERE Name = @item AND Owner = @char", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@item", ITEMNAME));
                cmd.Parameters.Add(new SQLiteParameter("@char", CHARNAME));

                db.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                result = reader.GetString(0);
                db.Close();
            }

            return result;
        }


        /// <summary>
        /// Gets the item type
        /// </summary>
        /// <param name="CHARNAME">Name of character</param>
        /// <param name="ITEMNAME">Name of item</param>
        /// <returns></returns>
        public string GetType(string CHARNAME, string ITEMNAME) // TODO: fix this to use Stat.Type
        {
            string result = "";

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Type FROM Inventory WHERE Name = @item AND Owner = @char", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@item", ITEMNAME));
                cmd.Parameters.Add(new SQLiteParameter("@char", CHARNAME));

                db.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                result = reader.GetString(0);
                db.Close();
            }
            return result;
        }


        /// <summary>
        /// Gives all inventory to the GM when a character
        /// is removed from the database
        /// </summary>
        /// <param name="NAME">Name of character</param>
        public void RemoveChar(string CHARNAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Inventory SET Owner = 'GM' WHERE Owner = @char", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@char", CHARNAME));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Transfers an item from one character to another
        /// </summary>
        /// <param name="FEATNAME">Item to transfer</param>
        /// <param name="FROM">Character to transfer from</param>
        /// <param name="TO">Character to transfer to</param>
        public void Transfer(string ITEMNAME, string FROM, string TO)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Inventory SET Owner = @to WHERE Owner = @from AND Name = @item", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@item", ITEMNAME));
                cmd.Parameters.Add(new SQLiteParameter("@from", FROM));
                cmd.Parameters.Add(new SQLiteParameter("@to", TO));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Copies item from one character to another
        /// </summary>
        /// <param name="ITEMNAME">item to copy</param>
        /// <param name="FROM">Character to copy from</param>
        /// <param name="TO">Character to copy to</param>
        public void Copy(string ITEMNAME, string FROM, string TO)
        {
            
        }


        /*
         * No more methods here
         */

        /*
         * No local variables
         */
    }
}
