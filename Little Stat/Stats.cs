using System;

// Holding all stats of an object
public class Stats
{
    public string NAME = "-none-";
    public float STRENGTH = 0;
    public float VIGOUR = 0;
    public float AGILITY = 0;
    public float INTELLECT = 0;
    public float PERCEPTION = 0;
    public float TENACITY = 0;
    public float CHARISMA = 0;
    public float INSTINCT = 0;
    public float COMMUNICATION = 0;
    public float MAXHP = 0; // Derrived only
    public float CURRENTHP = 0;
    public float ATTACK = 0; // Derrived only
    public float PHYSICALDEFENCE = 0; // Derrived only
    public float MENTALDEFENCE = 0; // Derrived only
    public float REACTION = 0; // Derrived only
    public float MOVEMENT = 0; // Derrived only
    public float ENCUMBRANCE = 0; // Derrived only
    public float WEAPON = 0;
    public float WEAPONCHAR = 0;
    public float ARMOUR = 0;
    public float FUROR = 0; // Derrived only
    public float CURRENTFUROR = 0;


    public int setName(string value)
    {
        NAME = value;
        return 0;
    }

    // For setting major stats, minor ones are automatic
    public int setStats(string statName, float value)
    {
        if (statName == "STRENGTH"){ STRENGTH = value; }
        if (statName == "VIGOUR") { VIGOUR = value; }
        if (statName == "AGILITY") { AGILITY = value; }
        if (statName == "INTELLECT") { INTELLECT = value; }
        if (statName == "PERCEPTION") { PERCEPTION = value; }
        if (statName == "TENACITY") { TENACITY = value; }
        if (statName == "INSTINCT") { INSTINCT = value; }
        if (statName == "COMMUNICATION") { COMMUNICATION = value; }
        if (statName == "CURRENTHP") { CURRENTHP = value; }
        if (statName == "WEAPON") { WEAPON = value; }
        if (statName == "WEAPONCHAR") { WEAPONCHAR = value; }
        if (statName == "ARMOUR") { ARMOUR = value; }
        if (statName == "CURRENTFUROR") { CURRENTFUROR = value; }

        // Bad stat name, return error.
        else{ return 1; }

        derriveMinorStats();

        return 0;
    }

    // Setting the derrived minor stats from major ones
    private void derriveMinorStats()
    {
        float BODY = STRENGTH+VIGOUR+AGILITY;
        float MIND = INTELLECT+PERCEPTION+TENACITY;
        float SOUL = CHARISMA+INSTINCT+COMMUNICATION;

        MAXHP = ((BODY * 3) + (MIND * 2) + SOUL) / 3;
        ATTACK = WEAPON + WEAPONCHAR;
        PHYSICALDEFENCE = AGILITY + VIGOUR + INSTINCT + ARMOUR;
        MENTALDEFENCE = TENACITY + INTELLECT + INSTINCT;
        REACTION = INTELLECT + PERCEPTION + INSTINCT;
        MOVEMENT = AGILITY + VIGOUR;
        ENCUMBRANCE = STRENGTH + STRENGTH + VIGOUR;
        FUROR = (VIGOUR + INSTINCT + TENACITY) / 2;
    }
}
