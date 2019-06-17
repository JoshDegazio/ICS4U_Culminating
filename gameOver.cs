using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameOver : MonoBehaviour
{
    //Private Globals
    TextMeshProUGUI scoreText;
    statTracker stats;

    GameObject score;
    GameObject statTracker;

    // Start is called before the first frame update
    void Start()
    {
        //Find gameobjects
        statTracker = GameObject.Find("statTracker");
        score = GameObject.Find("scoreText");

        //Retrieve object components
        scoreText = score.GetComponent<TextMeshProUGUI>();
        stats = statTracker.GetComponent<statTracker>();

        //Set text
        scoreText.text = "Score: " + stats.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
