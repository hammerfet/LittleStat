using System;

public class Stats
{
    public string NAME;
    public int STRENGTH;
    public int VIGOR;
    public int AGILITY;

    public int setStats(string statName, int value)
    {
        if (statName == "STRENGTH"){ STRENGTH = value; }
        if (statName == "VIGOR") { VIGOR = value; }
        if (statName == "AGILITY") { AGILITY = value; }
        
        // Bad stat name, return error.
        else{ return 1; }

        return 0;
    }
}
