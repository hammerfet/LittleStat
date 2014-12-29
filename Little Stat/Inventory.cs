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
        /// Returns the numerical modifier stat for an
        /// item of a specific character
        /// </summary>
        /// <param name="CHARNAME">Name of character</param>
        /// <param name="ITEMNAME">Name of item</param>
        /// <param name="STAT">stat to return</param>
        /// <returns></returns>
        public float ReturnModifierStat(string CHARNAME, string ITEMNAME, string STAT)
        {
            float result = 0;
            string str = "";

            switch (STAT)
            {
                case "Quantity":
                case "Weight":
                case "STRModifier":
                case "VIGModifier":
                case "AGIModifier":
                case "INTModifier":
                case "PERModifier":
                case "TENModifier":
                case "CHAModifier":
                case "INSModifier":
                case "COMModifier":
                    str = string.Format("SELECT {0} FROM Inventory WHERE ItemName = '{1}' AND BelongsTo = '{2}'", STAT, ITEMNAME, CHARNAME);
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
        /// Gives all inventory to the GM when a character
        /// is removed from the database
        /// </summary>
        /// <param name="NAME">Name of character</param>
        public void RemoveChar(string CHARNAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Inventory SET BelongsTo = 'GM' WHERE BelongsTo = @BelongsTo", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@BelongsTo", CHARNAME));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Transfers an item from one character to another
        /// </summary>
        /// <param name="FROM">Character to transfer from</param>
        /// <param name="TO">character to transfer to</param>
        public void TransferItem(string FROM, string TO)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Inventory SET BelongsTo = @TO WHERE BelongsTo = @FROM", db))
            {
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
        /// <param name="FROM">Character to copy item from</param>
        /// <param name="TO">Character to copy item to</param>
        public void CopyItem(string FROM, string TO)
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
