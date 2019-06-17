using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    //Public Globals
    [Header("Objects and Components")]
    public GameObject walker;
    public GameObject chunk;
    public GameObject player;

    [Header("Variables")]
    public string ZombieType;
    public float timer;
    private float setTime;
    private float newTime;

    //Private Globals
    PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        //Set a time that we can refer back to
        setTime = timer;
        //Set the timer in a random range
        timer = Random.Range(0, setTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Get component constantly
        pc = player.GetComponent<PlayerController>();

        //Countdown
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        //If countdown has ended
        if (timer <= 0)
        {
            //If the spawner type is "Walker"
            if (ZombieType == "Walker")
            {
                //Create Walker
                GameObject w = (Instantiate(walker, transform.position, Quaternion.identity));
                w.SetActive(true);
            }
            //If the spawner type is "Chunk"
            else if(ZombieType == "Chunk")
            {
                //Create Chunk
                GameObject c = (Instantiate(chunk, transform.position, Quaternion.identity));
                c.SetActive(true);
            }

            //Slowly decrease time inbetween zombie spawns
            newTime = setTime - pc.score / 250;
            if (newTime > 2)
            {
                timer = Random.Range(newTime, newTime * 1.5f);
            }
            else
            {
                //Don't let zombies spawn faster than every two and a half seconds
                timer = 2.3f;
            }
        }
    }
}
