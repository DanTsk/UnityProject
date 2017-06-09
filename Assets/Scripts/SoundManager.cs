using UnityEngine;

public class SoundManager
{
    bool is_sound_on = true;
    bool is_music_on = true;


    public bool isSoundOn()
    {
        return is_sound_on;
    }




    public void setSoundOn(bool val)
    {
        this.is_sound_on = val;   
    }



    public bool isMusicOn()
    {
        return is_music_on;
    }

    
    public void setMusicOn(bool val)
    {
        if (val == false)
        {
            LevelController.current.getMusic().Stop();
        }
        else
        {
            LevelController.current.getMusic().loop = true;
            LevelController.current.getMusic().Play();
        }

        this.is_music_on = val;
    }


    SoundManager()
    {
        
        is_sound_on = PlayerPrefs.GetInt("sound", 1) == 1;
        is_music_on = PlayerPrefs.GetInt("music", 1) == 1;
    }

    public static SoundManager Instance = new SoundManager();
}