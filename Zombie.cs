using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Zombie : MonoBehaviour
{
    //Global variables
    [Header("Objects and Components")]
    public GameObject bloodSplat1;
    public GameObject bloodSplat2;

    [Header("Variables")]
    public float speed;
    public float hp;
    public int damage;
    public int scoreGiven;
    public float attackTimer;

    //Private variables
    private GameObject player;
    private Collider2D mapBorderLeft;
    private Collider2D mapBorderRight;
    private Collider2D mapBorderTop;
    private Collider2D mapBorderBottom;
    Transform tr;
    Vector3 direction;
    Rigidbody2D rb;
    PlayerController pc;
    CameraController cc;
    SpriteRenderer sr;
    Color color_Hurt;
    Color color_Normal;
    Animator animator;
    System.Random r = new System.Random();
    private bool isAttacking;
    private bool takenHP;
    private bool isColliding;
    private float setAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        //Get the components from the newly created zombie
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //Find the player object and assign player to be the same as that object
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();

        //Timer
        setAttackTime = attackTimer;


        //Set color values of red, blue, green, and alpha.
        //Hurt animation
        color_Hurt.a = 1;
        color_Hurt.r = 1;
        color_Hurt.b = 0;
        color_Hurt.g = 0;
        //Normal
        color_Normal.a = 1;
        color_Normal.r = 1;
        color_Normal.b = 1;
        color_Normal.g = 1;

        //Get map borders
        mapBorderBottom = GameObject.Find("Map Border Bottom").GetComponent<Collider2D>();
        mapBorderTop = GameObject.Find("Map Border Top").GetComponent<Collider2D>();
        mapBorderLeft = GameObject.Find("Map Border Left").GetComponent<Collider2D>();
        mapBorderRight = GameObject.Find("Map Border Right").GetComponent<Collider2D>();

        //Ignore collision with map borders
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), mapBorderLeft);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), mapBorderRight);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), mapBorderTop);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), mapBorderBottom);
    }

    // Update is called once per frame
    void Update()
    {
        //If the zombie isn't attacking
        if (isAttacking == false)
        {
            //Move towards the player
            float step = speed * Time.deltaTime;
            Vector2 movement = Vector2.MoveTowards(rb.transform.localPosition, player.transform.localPosition, step);
            rb.position = movement;
        }

        //If the zombie isn't moving
        if(rb.velocity.magnitude != 0)
        {
            //Make sure it isn't moving
            rb.velocity = new Vector2(0, 0);
            rb.angularVelocity = 0;
        }

        //Rotate towards the player, relative to the zombie's current position
        direction = rb.transform.localPosition - player.transform.localPosition;
        var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        tr.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Run timers
        Timers();
    }

    //When something collides with the zombie
    void OnTriggerEnter2D(Collider2D col)
    {
        //If the object is of the type "Projectile"
        if (col.gameObject.tag == "Projectile")
        {
            //If the zombies health is greater 
            //than the damage the player can deal
            if (hp > pc.damage)
            {
                //Hurt the zombie
                hp -= pc.damage;
                Hurt();
            }
            //Otherwise
            else
            {
                //The zombie needs to die
                Die();
            }
        }
        //Otherwise if the zombie is of the type "Player"
        else if (col.gameObject.tag == "Player")
        {
            //The zombie is colliding with the player
            isColliding = true;
            //If the zombie isn't already attacking
            if (isAttacking == false)
            {
                //Attack
                isAttacking = true;
                Attack();
            }
        }
    }

    //When something is still colliding with the zombie
    void OnTriggerStay2D(Collider2D col)
    {
        //If the collision is with an object
        //with the type "Player"
        if (col.gameObject.tag == "Player")
        {
            //If the zombie isn't attacking
            if (isAttacking == false)
            {
                //Attack
                isAttacking = true;
                Attack();
            }
        }
    }

    //When something is no longer colliding with the zombie
    void OnTriggerExit2D(Collider2D col)
    {
        //If the object is the player
        if (col.gameObject.tag == "Player")
        {
            //The zombie is no longer colliding with the player
            isColliding = false;
        }
    }

    private void Die()
    {
        //New random and gameobject
        System.Random r = new System.Random();
        GameObject bs;

        //If random returns 2 or 0
        if (r.Next(0, 2) % 2 == 0)
        {
            //Create a bloodsplat with the first splat sprite
            bs = (Instantiate(bloodSplat1, transform.position, Quaternion.identity));
        }
        else
        {
            //Create a bloodsplat with the second splat sprite
            bs = (Instantiate(bloodSplat2, transform.position, Quaternion.identity));
        }

        //Set the splat to be active, and set the rotation of it 
        //to be the same as the current zombie's rotation
        bs.SetActive(true);
        bs.GetComponent<BloodSplatController>().setRotation(direction);

        //Add to the player's score
        pc.score += scoreGiven;
        //Destroy the zombie
        Destroy(gameObject);
        //Deactivate in hierarchy if the object somehow doesn't destroy
        gameObject.SetActive(false);
    }

    private void Hurt()
    {
        //Change sprite color
        sr.color = color_Hurt;
    }

    private void Attack()
    {
        //The zombie is attacking
        isAttacking = true;

        //Use a random attack animation
        if (r.Next(0, 2) == 1)
        {
            animator.SetBool("attackOne", true);
        }
        else animator.SetBool("attackTwo", true);

        animator.SetBool("isAttacking", true);
    }

    private void Timers()
    {
        //Change the sprite from red to normal
        if (sr.color != color_Normal)
        {
            sr.color += color_Normal * (Time.deltaTime * 10);
        }

        //If the zombie is attacking
        if (isAttacking == true)
        {
            //Countdown
            if (attackTimer >= 0)
            {
                attackTimer -= Time.deltaTime;
            }
            //If the countdown is almost done
            if (attackTimer < setAttackTime / 3)
            {
                //If the zombie hasn't attacked yet and still is colliding
                if (takenHP == false && isColliding == true)
                {
                    //If the player's health is greater than the zombie's damage
                    if (pc.health > damage)
                    {
                        //Hurt the player
                        pc.health -= damage;
                        pc.Hurt();
                    }
                    //Otherwise if the player's health is less than the zombie's damage
                    else if (pc.health <= damage)
                    {
                        //Kill the player
                        pc.Die();
                    }
                    //The zombie has attacked
                    takenHP = true;
                }
            }
            //If the attacktimer has reached or passed 0
            if (attackTimer <= 0)
            {
                //Reset values
                isAttacking = false;
                takenHP = false;
                animator.SetBool("isAttacking", false);
                animator.SetBool("attackTwo", false);
                animator.SetBool("attackOne", false);
                attackTimer = setAttackTime;
            }
        }
    }
}
