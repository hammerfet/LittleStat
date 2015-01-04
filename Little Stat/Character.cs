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


        /// <summary>
        /// Checks if character exists
        /// </summary>
        /// <param name="NAME">Name of check</param>
        /// <returns>true or false</returns>
        public bool Exists(string NAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT count(*) FROM Characters WHERE name = @name", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@name", NAME));

                db.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                db.Close();

                if (count == 0) return false;
                else return true;
            }
        }


        /// <summary>
        /// Creates a new character
        /// </summary>
        /// <param name="NAME">Name of character to create</param>
        public void Create(string NAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Characters (Name) VALUES (@name)", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@name", NAME));
                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public void Delete(string NAME)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Characters WHERE Name = @Name", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@name", NAME));
                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        

        /// <summary>
        /// Returns a list of character in database
        /// </summary>
        /// <returns>List of character names</returns>
        public List<string> List()
        {
            List<String> namesList = new List<string>();
           
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Name FROM Characters", db))
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


        /// <summary>
        /// Sets a stat of a character
        /// </summary>
        /// <param name="NAME">Name of character</param>
        /// <param name="STAT">Name of stat</param>
        /// <param name="VALUE">Value of stat</param>
        public void SetStat(string NAME, Stat STAT, float VALUE)
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
                case Stat.HP:
                case Stat.Mana:
                case Stat.Stamina:
                case Stat.EXP:
                    str = string.Format("UPDATE Characters SET {0} = @value WHERE Name = @name", STAT);
                    break;

                default:
                    return;
            }

            using (SQLiteCommand cmd = new SQLiteCommand(str, db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@name", NAME));
                cmd.Parameters.Add(new SQLiteParameter("@value", VALUE));

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
        public float GetStat(string NAME, Stat STAT)
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
                case Stat.HP:
                case Stat.Mana:
                case Stat.Stamina:
                case Stat.EXP:
                    str = string.Format("SELECT {0} FROM Characters WHERE Name = '{1}'", STAT, NAME);
                    break;

                /*
                 * Movement calculation
                 * 
                 * Movement = AGI + VIG
                 */
                case Stat.Movement:
                    str = string.Format("SELECT Agility + Agility + Tenacity FROM Characters WHERE Name = '{0}'", NAME);
                    break;

                /*
                 * Phys defence or Fortitude calculation
                 * 
                 * Fortitude = AGI + VIG + INS
                 */
                case Stat.Fortitude:
                    str = string.Format("SELECT Strength + Agility + Instinct FROM Characters WHERE Name = '{0}'", NAME);
                    break;

                /*
                 * Men defence or Will calculation
                 * 
                 * Will = TEN + INT + INS
                 */
                case Stat.Will:
                    str = string.Format("SELECT Tenacity + Intellect + Instinct FROM Characters WHERE Name = '{0}'", NAME);
                    break;

                /*
                 * Reaction calculation
                 * 
                 * Reaction = INT + PER + INS
                 */
                case Stat.Reaction:
                    str = string.Format("SELECT Intellect + Perception + Instinct FROM Characters WHERE Name = '{0}'", NAME);
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
