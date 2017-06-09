using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public string sceneName;


    void OnTriggerEnter2D(Collider2D collider)
    {   
       Rabbit rabit = collider.GetComponent<Rabbit>();
       if (rabit != null)
       {
            if (sceneName == "Level2") {
                if (LevelController.current.firstLevel == null)
                    return;

                if (!LevelController.current.firstLevel.levelPassed)
                    return;
            }

        
         SceneManager.LoadScene(sceneName);
       }        
    }


}
