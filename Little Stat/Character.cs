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
        SQLiteConnection db = new SQLiteConnection(@"Data Source=..\..\LittleStat.s3db");

        //Constructor Method
        public Character()
        {
            
        }

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

        public void ReturnCharList()
        {
            //SQLiteCommand cmd = new SQLiteCommand(db);
            //cmd.CommandText = "select * from MajorStats";

            //SQLiteDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader["NameID"]);

            //}
            //reader.Close();
            //db.Close();

        }

        public void DisplayCharStats(string NAME)
        {


        }
        private void UpdateStats(string NAME)
        {
            //CharMaxHP = ((BODY * 3) + (MIND * 2) + SOUL) / 3;
            
            //CharAttack(name, (WEAPON + WEAPONCHAR));
            //CharPhysDefence(name, ())

            //ATTACK = WEAPON + WEAPONCHAR;
            //PHYSICALDEFENCE = AGILITY + VIGOUR + INSTINCT + ARMOUR;
            //MENTALDEFENCE = TENACITY + INTELLECT + INSTINCT;
            //REACTION = INTELLECT + PERCEPTION + INSTINCT;
            //MOVEMENT = AGILITY + VIGOUR;
            //ENCUMBRANCE = STRENGTH + STRENGTH + VIGOUR;
            //FUROR = (VIGOUR + INSTINCT + TENACITY) / 2;
            // EXP = ?;
        }

        public void GetHP(string NAME, out float CurrentHP, out float MaxHP)
        {

        CurrentHP = 0;
        MaxHP = 0;

        }

        private Dictionary<string, float> CharMaxHP = new Dictionary<string, float>();
        private Dictionary<string, float> CharCurrentHP = new Dictionary<string, float>();
        private Dictionary<string, float> CharAttack = new Dictionary<string, float>();
        private Dictionary<string, float> CharPhysDefence = new Dictionary<string, float>();
        private Dictionary<string, float> CharMenDefence = new Dictionary<string, float>();
        private Dictionary<string, float> CharReaction = new Dictionary<string, float>();
        private Dictionary<string, float> CharMovement = new Dictionary<string, float>();
        private Dictionary<string, float> CharEncumbrance = new Dictionary<string, float>();
        private Dictionary<string, float> CharFuror = new Dictionary<string, float>();
        private Dictionary<string, float> CharEXP = new Dictionary<string, float>();
    }
}
