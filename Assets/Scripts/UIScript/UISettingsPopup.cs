using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISettingsPopup : MonoBehaviour {
    public GameObject settingsPrefab;
    public Sprite noMusic, noSound, Music, Sound;

    GameObject obj;
    GameObject closeBack, closeButton, musicButton, soundButton;
    int isMusic, isSound;




    public void showSettings()
    {        
        GameObject parent = UICamera.first.transform.parent.gameObject;

        obj = NGUITools.AddChild(parent, settingsPrefab);

        loadObjects();
        pauseSettings();

        UISettingsPopup popup = obj.GetComponent<UISettingsPopup>();
        
        loadPreferences();
        setBLoadedButtons();
    }

    void setBLoadedButtons() {
        MyButton backgroundClick = closeBack.GetComponent<MyButton>();
        backgroundClick.signalOnClick.AddListener(this.close);


        MyButton closeClick = closeButton.GetComponent<MyButton>();
        closeClick.signalOnClick.AddListener(this.close);
    }

    void loadObjects() {
        closeBack = GameObject.Find("CloseBack");
        closeButton = GameObject.Find("CloseButton");
        musicButton = GameObject.Find("Music");
        soundButton = GameObject.Find("Sound");
    }

    void pauseSettings() {
        string name = SceneManager.GetActiveScene().name;

        if (name == "Level1" || name == "Level2") {
            obj.transform.localPosition += new Vector3(120f, 750f,0f);

            var boxColiider = closeBack.GetComponent<BoxCollider>();
            boxColiider.center = new Vector3(-2550f,-80f);
            boxColiider.size = new Vector3(5800f, 3600f);

            Time.timeScale = 0;
        }

    }

    void loadPreferences() {
        isMusic = PlayerPrefs.GetInt("music", 1);
        isSound = PlayerPrefs.GetInt("sound", 1);

        if (isMusic == 0)
        {
            musicButton.GetComponent<UI2DSprite>().sprite2D = noMusic;
        }

        if (isSound == 0)
        {
            soundButton.GetComponent<UI2DSprite>().sprite2D = noSound;
        }

        MyButton musicClick = musicButton.GetComponent<MyButton>();
        musicClick.signalOnClick.AddListener(this.music);


        MyButton soundClick = soundButton.GetComponent<MyButton>();
        soundClick.signalOnClick.AddListener(this.sound);
    }

    void close() {       
        Destroy(obj);
        Time.timeScale = 1;
    }

    void music() {       

        if (isMusic == 0)
        {
            isMusic = 1;
            SoundManager.Instance.setMusicOn(true);
            PlayerPrefs.SetInt("music", 1);
            musicButton.GetComponent<UI2DSprite>().sprite2D = Music;
            musicButton.GetComponent<UIButton>().normalSprite2D = Music;
            return;
        }
        else if (isMusic == 1)
        {
            SoundManager.Instance.setMusicOn(false);
            isMusic = 0;
            PlayerPrefs.SetInt("music", 0);
            musicButton.GetComponent<UI2DSprite>().sprite2D = noMusic;
            musicButton.GetComponent<UIButton>().normalSprite2D = noMusic;
        }
    }

    void sound() {


        if (isSound == 0)
        {
            isSound = 1;
            PlayerPrefs.SetInt("sound", 1);
            soundButton.GetComponent<UI2DSprite>().sprite2D = Sound;
            soundButton.GetComponent<UIButton>().normalSprite2D = Sound;
            SoundManager.Instance.setSoundOn(true);
            return;
        }
        else if (isSound == 1)
        {
            isSound = 0;
            PlayerPrefs.SetInt("sound", 0);
            soundButton.GetComponent<UI2DSprite>().sprite2D = noSound;
            soundButton.GetComponent<UIButton>().normalSprite2D = noSound;
            SoundManager.Instance.setSoundOn(false);
            return;
        }
    }
}
