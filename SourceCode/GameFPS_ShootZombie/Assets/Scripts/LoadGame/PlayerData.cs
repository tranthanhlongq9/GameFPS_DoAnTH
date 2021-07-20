using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    //public int level;
    //public int exp;
    //public float health;
    //public int fund;
    //public GameObject pl;

    public float[] position;

    public PlayerData(SavePoint pla)
    {
        //level = levelSystem.level;
        //exp = levelSystem.exp;
        //health = healthManager.health;
        //fund = fundSystem.fund;

        position = new float[3];
        position[0] = pla.transform.position.x;
        position[1] = pla.transform.position.y;
        position[2] = pla.transform.position.z;
    }
}
