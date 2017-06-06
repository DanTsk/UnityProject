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
         SceneManager.LoadScene(sceneName);
       }        
    }


}
