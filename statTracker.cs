using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class statTracker : MonoBehaviour
{
    //Public Globals
    public int score;

    //Private Globals
    GameObject Player;
    PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        //Find gameobject
        Player = GameObject.Find("Player");
        //Get component
        pc = Player.GetComponent<PlayerController>();
        //Assign value
        score = pc.score;
    }

    // Update is called once per frame
    void Update()
    {
        //If scores aren't the same
        if (score != pc.score)
        {
            //Make them the same
            score = pc.score;
        }
        //If the user is at the mainmenu
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            //Destroy the stat tracker
            Destroy(gameObject);
        }
    }
}
