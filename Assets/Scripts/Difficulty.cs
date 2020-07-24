using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this static class is currently only used for difficulty selection
public static class Difficulty  {
    private static float deathSpeed=0.8f; //speed of death
    private static bool cheatMode = false;//if cheat mode is enabled
    private static int Level = 1;   //level to load
    public static float Speed
    {
        get
        {
          return  deathSpeed;
        }
        set
        {
            deathSpeed = value;
        }
    }

    public static bool Cheat    
    {
        get
        {
            return cheatMode;
        }
        set
        {
            cheatMode = value;
        }
    }

    
    public static int level
    {
        get
        {
            return Level;
        }
        set
        {
            Level = value;
        }
    }
	
}
