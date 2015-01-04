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
        public void Create(string CHARNAME, string ITEMNAME)
        {
            
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Quantity FROM Inventory WHERE Owner = @Owner AND Name = @Name", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Owner", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@Name", ITEMNAME));

                db.Open();
                int quantity = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();
                cmd.Parameters.Add(new SQLiteParameter("@NewQuantity", quantity + 1));

                if(quantity > 0)    cmd.CommandText = "UPDATE Inventory SET Quantity = @NewQuantity WHERE Owner = @Owner AND Name = @Name";
                else                cmd.CommandText = "INSERT INTO Inventory (Name, Owner) VALUES (@Name, @Owner)";

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

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Quantity FROM Inventory WHERE Owner = @Owner AND Name = @Name", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Owner", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@Name", ITEMNAME));

                db.Open();
                int quantity = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();
                cmd.Parameters.Add(new SQLiteParameter("@NewQuantity", quantity - 1));

                if (quantity > 1)   cmd.CommandText = "UPDATE Inventory SET Quantity = @NewQuantity WHERE Owner = @Owner AND Name = @Name";
                else                cmd.CommandText = "DELETE FROM Inventory WHERE Owner = @Owner AND Name = @Name";

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
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Quantity FROM Inventory WHERE Owner = @Owner AND Name = @Name", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Owner", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@Name", ITEMNAME));

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
        public List<string> List(string CHARNAME)
        {
            List<String> itemNameList = new List<string>();

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Name FROM Inventory where Owner = @Owner", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Owner", CHARNAME));
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
        /// <param name="ITEMNAME"></param>
        /// <param name="STAT"></param>
        /// <param name="value"></param>
        public void SetStat(string ITEMNAME, Stat STAT, float VALUE)
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
                case Stat.AoERadius:
                case Stat.HeadArmour:
                case Stat.BodyArmour:
                case Stat.BackArmour:
                case Stat.LegsArmour:
                case Stat.Quantity:
                case Stat.Weight:
                case Stat.LastsTurns:
                    str = string.Format("UPDATE Inventory SET {0} = @value WHERE Name = @Name", STAT);
                    break;

                default:
                    return;
            }

            using (SQLiteCommand cmd = new SQLiteCommand(str, db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Name", ITEMNAME));
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
        public void SetDescription(string ITEMNAME, string DESC)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Inventory SET Description = @desc WHERE Name = @Name", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Name", ITEMNAME));
                cmd.Parameters.Add(new SQLiteParameter("@desc", DESC));

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
                case Stat.AoERadius:
                case Stat.HeadArmour:
                case Stat.BodyArmour:
                case Stat.BackArmour:
                case Stat.LegsArmour:
                case Stat.Quantity:
                case Stat.Weight:
                case Stat.LastsTurns:
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
        /// 
        /// </summary>
        /// <param name="CHARNAME">Name of character</param>
        /// <param name="ITEMNAME">Name of item</param>
        /// <returns></returns>
        public string GetDescription(string CHARNAME, string ITEMNAME)
        {
            string result = "";
           
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Description FROM Inventory WHERE Name = @Name AND Owner = @Owner", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Name", ITEMNAME));
                cmd.Parameters.Add(new SQLiteParameter("@Owner", CHARNAME));

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
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Inventory SET Owner = 'GM' WHERE Owner = @Owner", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Owner", CHARNAME));

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
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Inventory SET Owner = @TO WHERE Owner = @FROM AND Name = @ITEMNAME", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@ITEMNAME", ITEMNAME));
                cmd.Parameters.Add(new SQLiteParameter("@FROM", FROM));
                cmd.Parameters.Add(new SQLiteParameter("@TO", TO));

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
