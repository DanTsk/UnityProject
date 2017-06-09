using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsLoader : MonoBehaviour {

    public int lvlNumber;

    public Sprite rdyFruit, rdyGems;


    void Start() {
        if (lvlNumber == 1)
        {
            loadIt(LevelController.current.firstLevel);
        }
        else if(lvlNumber == 2)
        {
            loadIt(LevelController.current.secondLevel);
        }

        if (LevelController.current.firstLevel != null && LevelController.current.firstLevel.levelPassed)
        {
            Destroy(GameObject.Find("lock"));
        }
    }


    void loadIt(LevelStats stats) {
        Debug.Log(lvlNumber);

        var fruits = GameObject.Find("fruits_" + lvlNumber).GetComponent<SpriteRenderer>();
        var gems = GameObject.Find("gems_" + lvlNumber).GetComponent<SpriteRenderer>();
        var finished = GameObject.Find("finished_" + lvlNumber).GetComponent<SpriteRenderer>();



        if (stats == null)
        {
            finished.sprite = null;
            return;
        }


        if (stats.hasAllFruits)
            fruits.sprite = rdyFruit;

        if (stats.hasCrystals)
            gems.sprite = rdyGems;


        if(!stats.levelPassed)
            finished.sprite = null;

    }
	
}
