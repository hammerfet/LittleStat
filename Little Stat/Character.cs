using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Little_Stat
{
    class Character
    {
        /* Create database object */
        SQLiteConnection db = new SQLiteConnection(@"Data Source=..\..\LittleStat.s3db");


        /* 
         * Checks if the character exists in database.
         * If not, it will create one. 
         * 
         * Args: NAME = name of character to create
         * 
         * Returns: True or False of character existing
         */ 
        public bool CheckForOrCreateChar(string NAME)
        {    
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT count(*) FROM MajorStats WHERE name = @name", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@name", NAME));

                db.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();

                if (count == 0)
                {
                    cmd.CommandText = "INSERT INTO MajorStats (Name) VALUES (@name)";

                    db.Open();
                    cmd.ExecuteNonQuery();
                    db.Close();
                    
                    return false;
                }
                return true;
            }
        }


        /*
         * Gets a list of characters in database
         * 
         * Returns: array of character names
         */
        public List<string> GetListOfChars()
        {
            List<String> namesList = new List<string>();
           
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Name FROM MajorStats", db))
            {
                db.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    namesList.Add(reader.GetString(i));
                }
                reader.Close();
                db.Close();
            }
            return namesList;
        }


        /*
         * Sets a stat of the selected character
         * 
         * Args: NAME = character name to set stat of
         *       STAT = the stat to set
         *       value = the value to set the stat to
         *       
         * Returns: Nothing..
         */
        public void SetCharStats(string NAME, string STAT, float value)
        {
            string str = string.Format("UPDATE MajorStats SET {0} = @value WHERE Name = @name", STAT);
            using (SQLiteCommand cmd = new SQLiteCommand(str, db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@name", NAME));
                cmd.Parameters.Add(new SQLiteParameter("@value", value));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /*
         * Returns the stat of a character, if it's
         * not a base stat, then it's caluculated before
         * being returned.
         * 
         * Args: NAME = the name of the character
         *       STAT = the stat to return
         *       
         * Returns: number of requested stat
         */
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

                    using (SQLiteCommand cmd = new SQLiteCommand(str, db))
                    {
                        db.Open();
                        result = Convert.ToInt32(cmd.ExecuteScalar());
                        db.Close();
                    }
                    break;

                case "MaxHP": // Requires long calculation

                    break;

                case "MaxMana":
                    break;

                case "MaxStamina":
                    break;

                /*
                 * Movement calculation
                 * 
                 * Movement = AGI + VIG
                 */
                case "Movement":
                    str = string.Format("SELECT Agility, Vigour FROM MajorStats WHERE Name = '{0}'", NAME);
                    using (SQLiteCommand cmd = new SQLiteCommand(str, db))
                    {
                        db.Open();
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = Convert.ToInt32(reader["Agility"]) + Convert.ToInt32(reader["Vigour"]);
                        }
                        reader.Close();
                        db.Close();
                    }
                    break;

                /*
                 * Phys defence or Fortitude calculation
                 * 
                 * Fortitude = AGI + VIG + INS
                 */
                case "PhysicalDefence":
                case "Fortitude":
                    str = string.Format("SELECT Agility, Vigour, Instinct FROM MajorStats WHERE Name = '{0}'", NAME);
                    using (SQLiteCommand cmd = new SQLiteCommand(str, db))
                    {
                        db.Open();
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = Convert.ToInt32(reader["Agility"]) + Convert.ToInt32(reader["Vigour"])
                                + Convert.ToInt32(reader["Instinct"]);
                        }
                        reader.Close();
                        db.Close();
                    }
                    break;

                /*
                 * Men defence or Will calculation
                 * 
                 * Will = TEN + INT + INS
                 */
                case "MentalDefence":
                case "Will":
                    str = string.Format("SELECT Tenacity, Intellect, Instinct FROM MajorStats WHERE Name = '{0}'", NAME);
                    using (SQLiteCommand cmd = new SQLiteCommand(str, db))
                    {
                        db.Open();
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = Convert.ToInt32(reader["Tenacity"]) + Convert.ToInt32(reader["Intellect"])
                                + Convert.ToInt32(reader["Instinct"]);
                        }
                        reader.Close();
                        db.Close();
                    }
                    break;

                /*
                 * Reaction calculation
                 * 
                 * Reaction = INT + PER + INS
                 */
                case "Reaction":
                    str = string.Format("SELECT Intellect, Perception, Instinct FROM MajorStats WHERE Name = '{0}'", NAME);
                    using (SQLiteCommand cmd = new SQLiteCommand(str, db))
                    {
                        db.Open();
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = Convert.ToInt32(reader["Intellect"]) + Convert.ToInt32(reader["Perception"])
                                + Convert.ToInt32(reader["Instinct"]);
                        }
                        reader.Close();
                        db.Close();
                    }
                    break;

                /*
                 * Encumbrance calculation
                 * 
                 * Encumbrance = STR + STR + VIG
                 */
                case "MaxEncumberance":
                    str = string.Format("SELECT Strength, Vigour FROM MajorStats WHERE Name = '{0}'", NAME);
                    using (SQLiteCommand cmd = new SQLiteCommand(str, db))
                    {
                        db.Open();
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = Convert.ToInt32(reader["Strength"]) + Convert.ToInt32(reader["Strength"])
                                + Convert.ToInt32(reader["Vigour"]);
                        }
                        reader.Close();
                        db.Close();
                    }
                    break;


            }
            return result;
        }


        /*
         * No more methods here
         */
    }
}
