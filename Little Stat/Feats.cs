using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Little_Stat
{
    class Feats
    {
        SQLiteConnection db = new SQLiteConnection(@"Data Source=..\..\LittleStat.s3db");


        /// <summary>
        /// Checks if feat exists
        /// </summary>
        /// <param name="CHARNAME">name of character</param>
        /// <param name="FEATNAME">name of feat</param>
        /// <returns></returns>
        public bool Exists(string CHARNAME, string FEATNAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT count(*) FROM Feats WHERE Character = @Character AND FeatName = @FeatName", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Character", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@FeatName", FEATNAME));

                db.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();

                if (count == 0) return false;
                else return true;
            }
        }    


        /// <summary>
        /// Creates a new feat with name and character
        /// </summary>
        /// <param name="CHARNAME">Name of character</param>
        /// <param name="FEATNAME">Name of feat</param>
        public void Create(string CHARNAME, string FEATNAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Feats (Character, FeatName) VALUES (@Character, @FeatName)", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Character", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@FeatName", FEATNAME));
                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Deletes a feat from a charcter
        /// </summary>
        /// <param name="CHARNAME">Name of character</param>
        /// <param name="FEATNAME">Name of feat</param>
        public void Delete(string CHARNAME, string FEATNAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Feats WHERE Character = @Character AND FeatName = @FeatName", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Character", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@FeatName", FEATNAME));
                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        

        /// <summary>
        /// Lists the feats of a character
        /// </summary>
        /// <param name="CHARNAME">Name of character</param>
        /// <returns>A list of feats</returns>
        public List<string> List(string CHARNAME)
        {
            List<String> FeatList = new List<string>();
           
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT FeatName FROM Feats WHERE Character = @Character", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Character", CHARNAME));
                db.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    FeatList.Add(reader.GetString(i));
                }
                reader.Close();
                db.Close();
            }
            return FeatList;
        }


        /// <summary>
        /// Sets a stat of a feat
        /// </summary>
        /// <param name="NAME">Name of character</param>
        /// <param name="STAT">Name of stat</param>
        /// <param name="VALUE">Value of stat</param>
        public void SetStat(string FEAT, string STAT, float VALUE)
        {
            string str = "";

            switch (STAT)
            {
                case "HealthHit":
                case "ManaHit":
                case "StaminaHit":
                case "STRModifier":
                case "VIGModifier":
                case "AGIModifier":
                case "INTModifier":
                case "PERModifier":
                case "TENModifier":
                case "CHAModifier":
                case "INSModifier":
                case "COMModifier":
                case "LastsTurns":
                    str = string.Format("UPDATE Feats SET {0} = @value WHERE FeatName = @FeatName", STAT);
                    break;

                default:
                    return;
            }

            using (SQLiteCommand cmd = new SQLiteCommand(str, db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@FeatName", FEAT));
                cmd.Parameters.Add(new SQLiteParameter("@value", VALUE));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Sets the description of a feat
        /// </summary>
        /// <param name="FEAT">Name of feat</param>
        /// <param name="DESC">String description of feat</param>
        public void SetDescription(string FEAT, string DESC)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Feats SET Description = @desc WHERE FeatName = @FeatName", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@FeatName", FEAT));
                cmd.Parameters.Add(new SQLiteParameter("@desc", DESC));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Returns the numerical modifier stat for the
        /// feat of a specific character
        /// </summary>
        /// <param name="CHARNAME">Name of character</param>
        /// <param name="FEATNAME">Name of feat</param>
        /// <param name="STAT">stat to return</param>
        /// <returns>float value</returns>
        public float GetStat(string CHARNAME, string FEATNAME, string STAT)
        {
            float result = 0;
            string str = "";

            switch (STAT)
            {
                case "HealthHit":
                case "ManaHit":
                case "StaminaHit":
                case "STRModifier":
                case "VIGModifier":
                case "AGIModifier":
                case "INTModifier":
                case "PERModifier":
                case "TENModifier":
                case "CHAModifier":
                case "INSModifier":
                case "COMModifier":
                case "LastsTurns":
                    str = string.Format("SELECT {0} FROM Feats WHERE FeatName = '{1}' AND Character = '{2}'", STAT, FEATNAME, CHARNAME);
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
        /// Returns the description of a feat
        /// </summary>
        /// <param name="CHARNAME">Name of character</param>
        /// <param name="FEATNAME">Name of feat</param>
        /// <returns>string value</returns>
        public string GetDescription(string CHARNAME, string FEATNAME)
        {
            string result = "";

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Description FROM Feats WHERE Character = @Character AND FeatName = @FeatName", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Character", CHARNAME));
                cmd.Parameters.Add(new SQLiteParameter("@FeatName", FEATNAME));

                db.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                result = reader.GetString(0);
                db.Close();
            }

            return result;
        }


        /// <summary>
        /// Gives all feats to the GM when a character
        /// is removed from the database
        /// </summary>
        /// <param name="NAME">Name of character</param>
        public void RemoveChar(string CHARNAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Feats SET Character = 'GM' WHERE Character = @Character", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Character", CHARNAME));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Transfers a feat from one character to another
        /// </summary>
        /// <param name="FEATNAME">Feat to transfer</param>
        /// <param name="FROM">Character to transfer from</param>
        /// <param name="TO">Character to transfer to</param>
        public void Transfer(string FEATNAME, string FROM, string TO)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Feats SET Character = @TO WHERE Character = @FROM AND FeatName = @FEATNAME", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@FEARNAME", FEATNAME));
                cmd.Parameters.Add(new SQLiteParameter("@FROM", FROM));
                cmd.Parameters.Add(new SQLiteParameter("@TO", TO));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Copies feat from one character to another
        /// </summary>
        /// <param name="FEATNAME">Feat to copy</param>
        /// <param name="FROM">Character to copy from</param>
        /// <param name="TO">Character to copy to</param>
        public void Copy(string FEATNAME, string FROM, string TO)
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
