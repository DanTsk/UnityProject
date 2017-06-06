using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int availableFruits = 0;
    public int lifes = 3;

    public MyButton playButton;

    int currentFruits;
    int coins;
    int death;


    public static LevelController current;
    public static GameObject objects;

    Vector3 startingPosition;

    void Awake()
    {
        current = this;

        objects = GameObject.Find("Objects");


        currentFruits = 0;
        death = 0;
        coins = 0;


        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            playButton = GameObject.Find("StartButton").GetComponent<MyButton>();
            playButton.signalOnClick.AddListener(this.onPlay);
        }

    }

    void Start() {
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2")
            UIFruit.fruitCounter.setFruits(0, availableFruits);
    } 

    void onPlay() {
        SceneManager.LoadScene("LevelSelect");
    }



    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void onRabitDeath(Rabbit rabit)
    {
   
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2")
        {
            lifes--;
            death++;

            if (lifes < 0)
            {
                SceneManager.LoadScene("LevelSelect");
            }
            else
            {
                UILifes.lifes.die(death);
            }
        }

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
        UICoins.coinsCounter.setCoins(coins);
    }

    public void addFruit() {
        currentFruits++;
        UIFruit.fruitCounter.setFruits(currentFruits,availableFruits);
    }
}
