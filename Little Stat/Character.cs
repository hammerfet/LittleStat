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
        /* Create database connection object */
        SQLiteConnection db = new SQLiteConnection(@"Data Source=..\..\LittleStat.s3db");


        /*
         * Checks if character exists in database
         * 
         * Args: Name of character to check
         * 
         * Returns: true or false
         */
        public bool Exists(string NAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT count(*) FROM MajorStats WHERE name = @name", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@name", NAME));

                db.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();

                if (count == 0) return false;
                else return true;
            }
        }


        /* 
         * Creates character in database. Should
         * only be called after checking if character
         * already exists.
         * 
         * Args: NAME = name of character to create
         * 
         * Returns: Nothing
         */
        public void Create(string NAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO MajorStats (Name) VALUES (@name)", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@name", NAME));
                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public void Delete(string NAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM MajorStats WHERE Name = @Name", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@name", NAME));
                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        

        /*
         * Gets a list of characters in database
         * 
         * Returns: array of character names
         */
        public List<string> List()
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
        public void SetStat(string NAME, string STAT, float value)
        {
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
                    str = string.Format("UPDATE MajorStats SET {0} = @value WHERE Name = @name", STAT);
                    break;

                default:
                    return;
            }

            using (SQLiteCommand cmd = new SQLiteCommand(str, db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@name", NAME));
                cmd.Parameters.Add(new SQLiteParameter("@value", value));

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        /// <summary>
        /// Returns major stats of a character from the database.
        /// If it's a minor stat, it will calculate from major
        /// stats and return the value
        /// </summary>
        /// <param name="NAME">The name of the character</param>
        /// <param name="STAT">Stat required</param>
        /// <returns>Raw float value of the stat</returns>
        public float GetStat(string NAME, string STAT)
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


        /*
         * No more methods here
         */

        /*
         * No local variables
         */
    }
}
