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

        public void SetCharStats(string NAME, string STAT, float value)
        {
   
            using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO MajorStats (NameID, Strength) VALUES (@NameID, @Strength)", db))
            {
                cmd.Parameters.Add(new SQLiteParameter("@Strength", value));
                cmd.Parameters.Add(new SQLiteParameter("@NameID", NAME));
            
                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }

            //SQLiteCommand cmd = new SQLiteCommand(db);
            //cmd.CommandText = "select * from MajorStats";
        
            //SQLiteDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader["NameID"]);

            //}
            //reader.Close();
            //db.Close();

            Console.ReadLine();


            //UpdateStats(NAME);
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

        public bool GetCharStats(string name, out float STRENGTH, out float VIGOUR)
        {
            if (CharType.ContainsKey(name))
            {
                CharStrength.TryGetValue(name, out STRENGTH);
                CharVigour.TryGetValue(name, out VIGOUR);
                return true;
            }
            STRENGTH = 0;
            VIGOUR = 0;
            return false;   
        }

        // Public Variables
        public Dictionary<string, string> CharType = new Dictionary<string, string>();
        
        // Private Variables
        private Dictionary<string, float> CharStrength = new Dictionary<string, float>();
        private Dictionary<string, float> CharVigour = new Dictionary<string, float>();
        private Dictionary<string, float> CharAgility = new Dictionary<string, float>();
        private Dictionary<string, float> CharIntellect = new Dictionary<string, float>();
        private Dictionary<string, float> CharPerception = new Dictionary<string, float>();
        private Dictionary<string, float> CharTenacity = new Dictionary<string, float>();
        private Dictionary<string, float> CharCharisma = new Dictionary<string, float>();
        private Dictionary<string, float> CharInstinct = new Dictionary<string, float>();
        private Dictionary<string, float> CharCommunication = new Dictionary<string, float>();

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
