using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    int coins;


    public static LevelController current;

    Vector3 startingPosition;

    void Awake()
    {
        current = this;
        coins = 0;
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void onRabitDeath(Rabbit rabit)
    {
        rabit.removeBuffes();
        rabit.transform.position = this.startingPosition;
    }

    public static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {        
            Vector3 pos = obj.transform.position;
            obj.transform.parent = new_parent; 
            obj.transform.position = pos;
        }
    }

    public void addCoins(int numb) {
        coins += numb;
    }
}
