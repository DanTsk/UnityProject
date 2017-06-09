using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGamePopup : MonoBehaviour
{
    public GameObject settingsPrefab;
    public Sprite red, blue, green;

    GameObject obj;
    GameObject closeBack, closeButton, restartButton, nextButton;
    GameObject redGem, blueGem, greenGem;
    UILabel frLabel, coinsLabel;

    public AudioClip fmusic = null;
    AudioSource fmusicSource = null;

    public AudioClip dmusic = null;
    AudioSource dmusicSource = null;

    public void showSettings(int mode = 1)
    {
        GameObject parent = UICamera.first.transform.parent.gameObject;

        obj = NGUITools.AddChild(parent, settingsPrefab);

        loadObjects(mode);
        perfabFix(mode);

        UISettingsPopup popup = obj.GetComponent<UISettingsPopup>();


        setBLoadedButtons();
        setBControllButtons();
        Time.timeScale = 1;


        if (!Rabbit.Hero.isDead())
        {        
         saveState();
        }



        if (mode == 1 ) {
            fmusicSource = gameObject.AddComponent<AudioSource>();
            fmusicSource.clip = fmusic;

            if (SoundManager.Instance.isMusicOn())
                fmusicSource.Play();

        }
        else if(mode == 2)
        {
            dmusicSource = gameObject.AddComponent<AudioSource>();
            dmusicSource.clip = dmusic;

            if (SoundManager.Instance.isMusicOn())
                dmusicSource.Play();

        }

            

        LevelController.current.setEnd();
    }

    void setBLoadedButtons()
    {
        MyButton backgroundClick = closeBack.GetComponent<MyButton>();
        backgroundClick.signalOnClick.AddListener(this.close);


        MyButton closeClick = closeButton.GetComponent<MyButton>();
        closeClick.signalOnClick.AddListener(this.close);
    }

    void setBControllButtons() {
        MyButton restartClick = restartButton.GetComponent<MyButton>();
        restartClick.signalOnClick.AddListener(this.restart);


        MyButton nextClick = nextButton.GetComponent<MyButton>();
        nextClick.signalOnClick.AddListener(this.next);
    }

    void next() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    void restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void saveState() {
        LevelStats stats = new LevelStats();

        if (LevelController.current.getFruitsCMPX().Count == LevelController.current.availableFruits)
        {
            stats.hasAllFruits = true;
        }
        else
        {
            stats.hasAllFruits = false;
        }


        if (LevelController.current.gems[0] == 1 &&
            LevelController.current.gems[1] == 1 &&
            LevelController.current.gems[2] == 1)
        {
            stats.hasCrystals = true;
        }
        else
        {
            stats.hasCrystals = false;
        }

        stats.levelPassed = true;
        stats.collectedFruits = LevelController.current.getFruitsCMPX();


        string str = JsonUtility.ToJson(stats);

        int numbe = (SceneManager.GetActiveScene().name == "Level1") ? 1 : 2;

        PlayerPrefs.SetString("stats_"+ numbe, str);
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + LevelController.current.getCoins());
    }



    void loadObjects(int mode)
    {
        closeBack = GameObject.Find("CloseBack");
        closeButton = GameObject.Find("CloseButton");
        restartButton = GameObject.Find("Restart");
        nextButton = GameObject.Find("Menu");


        if (mode == 1)
        {

            frLabel = GameObject.Find("FruitsEnding").GetComponent<UILabel>();
            coinsLabel = GameObject.Find("CoinsEnding").GetComponent<UILabel>();

            redGem = GameObject.Find("RedCrystal");
            blueGem = GameObject.Find("BlueCrystal");
            greenGem = GameObject.Find("GreenCrystal");

            frLabel.text = LevelController.current.getFruits() + "/" + LevelController.current.availableFruits;
            coinsLabel.text = "+" + LevelController.current.getCoins();

            if (LevelController.current.gems[0] == 1) redGem.GetComponent<UI2DSprite>().sprite2D = red;
            if (LevelController.current.gems[1] == 1) blueGem.GetComponent<UI2DSprite>().sprite2D = blue;
            if (LevelController.current.gems[2] == 1) greenGem.GetComponent<UI2DSprite>().sprite2D = green;
        }

    }

    void perfabFix(int mode )
    {
       if(mode == 1)
         obj.transform.localPosition += new Vector3(120f, 750f, 0f);
       else
         obj.transform.localPosition = new Vector3(120f, 0f, 0f);

       var boxColiider = closeBack.GetComponent<BoxCollider>();
       boxColiider.center = new Vector3(-2550f, -80f);
       boxColiider.size = new Vector3(15800f, 13600f);
    }

   
    void close()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

  
}
