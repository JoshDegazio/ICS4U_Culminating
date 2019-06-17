/*
 * 
 * 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    //Public Globals
    [Header("Objects and Components")]
    public Rigidbody2D rb;
    public Transform tr;
    public GameObject bullet;
    public GameObject smoke;
    public Vector3 direction { get; private set; }
    public GameObject statTracker;

    [Header("Variables")]
    public float speed;
    public int damage = 10;
    public int score = 0;
    public int health = 100;
    public float fireRate = 8f;

    //Private Globals
    private bool canShoot;
    private float timer;
    private float moveHorizontal;
    private float moveVertical;
    private Animator animator;
    SpriteRenderer sr;
    Color color_Hurt;
    Color color_Normal;

    // Start is called before the first frame update
    void Start()
    {
        //Get components from player object
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //set timer
        timer = 1;

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

        //Bool if player is able to shoot
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal = 1 if "D" or ">" is pressed
        //Horizontal = -1 if "A" or "<" is pressed
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        //Set player rotation based on current position of the mouse 
        //relative to the camera/player's position in the world
        tr.rotation = new Quaternion(180f, 180f, 0f, 0f);
        var pos = Camera.main.WorldToScreenPoint(tr.position);
        direction = Input.mousePosition - pos;
        var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90;
        tr.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Run method for movement and animation 
        MovementandAnimation();

        //If the colour isn't normal
        if (sr.color != color_Normal)
        {
            //Slowly change from hurt to normal
            sr.color += color_Normal * (Time.deltaTime * 10);
        }


        //Shoot Bullet
        if (Input.GetMouseButton(0) && canShoot == true || Input.GetMouseButtonDown(0) && canShoot == true)
        {
            //Create Bullet
            GameObject b = (Instantiate(bullet, transform.position, Quaternion.identity));
            b.SetActive(true);
           

            //Create Gun Smoke
            GameObject s = (Instantiate(smoke, transform.position, Quaternion.identity));
            s.SetActive(true);

            //Set timer for firerate cap
            timer = 1;
            canShoot = false;
        }

        //Lower the timer based on firerate and the time that passed in between frames
        if (timer > 0)
        {
            timer -= Time.deltaTime * fireRate;
        }
        //If the timer is 0, let the player be able to shoot again
        else if (timer <= 0)
        {
            canShoot = true;
        }
    }

    //Movement and animation Method
    private void MovementandAnimation()
    {
        if (moveHorizontal != 0 && moveVertical != 0)
        {
            moveHorizontal *= .75f;
            moveVertical *= .75f;
        }

        //Set movement of the player object's rigidbody
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;

        //If the player is standing still
        if (movement.magnitude == 0)
        {
            //Tell the animator
            animator.SetBool("isWalking", false);

            //Set the velocity of the rigidbody
            rb.velocity = new Vector2(0, 0);
            rb.angularVelocity = 0;
        }
        //If the player is walking
        else if (movement.magnitude != 0)
        {
            //If the animator doesn't know the player is walking
            if (animator.GetBool("isWalking") == false)
            {
                //Tell the animator that the player is walking
                animator.SetBool("isWalking", true);
            }
        }
    }

    public void Die()
    {
        //Load the gameover scene
        DontDestroyOnLoad(statTracker);
        SceneManager.LoadSceneAsync("Game Over");
    }

    public void Hurt()
    {
        //Change the color of the entire sprite to red,
        sr.color = color_Hurt;
    }
}
