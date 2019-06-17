using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Public Globals
    [Header("Objects and Components")]
    public GameObject bullet;
    public GameObject player;

    [Header("Variables")]
    public float speed;

    //Private Globals
    private Rigidbody2D rb;
    private PlayerController pc;
    private Vector2 movement;
    private SpriteRenderer sr;



    void Start()
    {
        //Get Player Components and objects
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();

        //Set the bullet to be active
        bullet = gameObject;
        bullet.SetActive(true);

        //Get self components
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        //Rotate and set movement
        var angle = (Mathf.Atan2(pc.direction.y, pc.direction.x) * Mathf.Rad2Deg) + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 directionVector = (pc.direction - transform.position).normalized;
        movement = directionVector * speed;
    }

    void Update()
    {
        //Move in the constant direction previously set
        rb.velocity = movement * speed;

        //If the bullet is off the screen
        if (!sr.isVisible)
        {
            //Destroy
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    //When a collision occurs
    void OnTriggerEnter2D(Collider2D col)
    {
        //If the collision is with a zombie
        if (col.gameObject.tag == "Zombie")
        {
            //Destroy self
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
        //Otherwise
        else if (col.gameObject.tag == "Impenetrable")
        {
            //Still destroy
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
