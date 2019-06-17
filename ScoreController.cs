using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//UI Controller
public class ScoreController : MonoBehaviour
{
    //Global Variables
    [Header("Variables")]
    public bool isScore;
    public bool isHealth;
    public int score;
    public int health;

    //Private Variables
    PlayerController pc;
    GameObject player;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        //Get components and objects
        text = gameObject.GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set the private variables to be equal to that of the player variables
        score = pc.score;
        health = pc.health;

        //If the object this script is on, is the score text
        if(isScore == true)
        {
            //Set the text to:
            text.text = "Score: " + score.ToString();
        }
        //If the object this script is on, is the health text
        else if (isHealth == true)
        {
            //Set the text to:
            text.text = "Health: " + health.ToString();
        }
    }
}
