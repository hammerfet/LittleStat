using System;

// Holding all stats of an object
public class Stats
{
    public string[] BaseStatNames = { "STRENGTH", "VIGOUR", "AGILITY", "INTELLECT", "PERCEPTION", "TENACITY", "CHARISMA", "INSTINCT", "COMMUNICATION" };
    public int[] BaseStatValues = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    //public float MAXHP = 0; // Derrived only
    //public float CURRENTHP = 0;
    //public float ATTACK = 0; // Derrived only
    //public float PHYSICALDEFENCE = 0; // Derrived only
    //public float MENTALDEFENCE = 0; // Derrived only
    //public float REACTION = 0; // Derrived only
    //public float MOVEMENT = 0; // Derrived only
    //public float ENCUMBRANCE = 0; // Derrived only
    //public float WEAPON = 0;
    //public float WEAPONCHAR = 0;
    //public float ARMOUR = 0;
    //public float FUROR = 0; // Derrived only
    //public float CURRENTFUROR = 0;
    
    public Stats()
    {

    }


    // Setting the derrived minor stats from major ones
    public void derriveMinorStats()
    {
        //float BODY = BaseStatValues[1] + BaseStatValues[2] + BaseStatValues[3];
        //float MIND = BaseStatValues[3] + BaseStatValues[4] + BaseStatValues[5];
        //float SOUL = BaseStatValues[6] + BaseStatValues[7] + BaseStatValues[8];

        //MAXHP = ((BODY * 3) + (MIND * 2) + SOUL) / 3;
        //ATTACK = WEAPON + WEAPONCHAR;
        //PHYSICALDEFENCE = AGILITY + VIGOUR + INSTINCT + ARMOUR;
        //MENTALDEFENCE = TENACITY + INTELLECT + INSTINCT;
        //REACTION = INTELLECT + PERCEPTION + INSTINCT;
        //MOVEMENT = AGILITY + VIGOUR;
        //ENCUMBRANCE = STRENGTH + STRENGTH + VIGOUR;
        //FUROR = (VIGOUR + INSTINCT + TENACITY) / 2;
    }
}
