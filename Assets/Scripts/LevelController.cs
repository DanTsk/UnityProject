using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int availableFruits = 0;
    public int lifes = 3;
    public int[] gems;

    public MyButton playButton, settingsButton;

    int currentFruits;
    int coins;
    int death;


    public static LevelController current;
    public static GameObject objects;

     List<int> collectedFruits;

    Vector3 startingPosition;


    public LevelStats firstLevel, secondLevel;

    public AudioClip music = null;
    AudioSource musicSource = null;


    public AudioSource getMusic() {
        return musicSource;
    }

    public List<int> getFruitsCMPX() {
        return collectedFruits;
    }


    void Awake()
    {
           
        current = this;

        objects = GameObject.Find("Objects");


        string name = "stats_";
        string currentName = SceneManager.GetActiveScene().name;

        if (currentName == "Level1")
        {
            name += 1;
            loadFruits(name);
        }
        else if (currentName == "Level2")
        {
            name += 2;
            loadFruits(name);
        }
        else if (currentName == "LevelSelect")
        {
            string str = PlayerPrefs.GetString("stats_1", null);
            firstLevel = JsonUtility.FromJson<LevelStats>(str);

            str = PlayerPrefs.GetString("stats_2", null);
            secondLevel = JsonUtility.FromJson<LevelStats>(str);
        }

              


        currentFruits = 0;
        death = 0;
        coins = 0;

        gems = new int[3] { 0, 0, 0 };

        mainMenuButtons();
        gameInButtons();

    }

    void Start() {
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2")
            UIFruit.fruitCounter.setFruits(0, availableFruits);


        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
        musicSource.volume = 0.125f;
        musicSource.priority = 255;

        if (SoundManager.Instance.isMusicOn())
        {         
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    void onPlay() {
        SceneManager.LoadScene("LevelSelect");
    }

    void onSettings() {
        GameObject.Find("SettingsPopup").GetComponent<UISettingsPopup>().showSettings();
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
                GameObject.Find("LostPopup").GetComponent<UIGamePopup>().showSettings(2);
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
        UIFruit.fruitCounter.setFruits(currentFruits, availableFruits);
    }


    public void mainMenuButtons(){
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            playButton = GameObject.Find("StartButton").GetComponent<MyButton>();
            playButton.signalOnClick.AddListener(this.onPlay);

            settingsButton = GameObject.Find("SettingsButton").GetComponent<MyButton>();
            settingsButton.signalOnClick.AddListener(this.onSettings);
        }
    }

    public void gameInButtons()
    {
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2")
        {
            settingsButton = GameObject.Find("PauseButton").GetComponent<MyButton>();
            settingsButton.signalOnClick.AddListener(this.onSettings);
        }

    }

    public void setEnd() {
        Rabbit.Hero.setUnactive();
    }


    public int getCoins() {
        return this.coins;
    }

    public int getFruits()
    {
        return this.currentFruits;
    }

    public int[] getGems() {
        return this.gems;
    }


    void loadFruits(string name) {
        string str = PlayerPrefs.GetString(name, null);
        LevelStats stats = JsonUtility.FromJson<LevelStats>(str);
        if (stats != null)
        {       
            this.collectedFruits = stats.collectedFruits;
        }


        if (collectedFruits == null)
            collectedFruits = new List<int>();


        
        foreach (int i in collectedFruits)
        {
            Debug.Log(i);
            GameObject.Find(i.ToString()).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        }

            
          
       
    }
}
