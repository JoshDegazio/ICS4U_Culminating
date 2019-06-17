using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatController : MonoBehaviour
{
    //Private Globals
    float speedOfFade;
    SpriteRenderer sr;
    Color c;

    // Start is called before the first frame update
    void Start()
    {
        //Normal colour
        //Allows sprite to fade 
        c.a = 1;
        c.r = 1;
        c.b = 1;
        c.g = 1;
        //Adjust speed of fade
        speedOfFade = 40;
        //Get component
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Fade
        c.a -= Time.deltaTime / speedOfFade;
        sr.color = c;

        //If object has faded out
        if(c.a == 0)
        {
            //Destroy
            Destroy(gameObject);
        }
    }

    //Set the rotation given a vector
    public void setRotation(Vector3 rotation)
    {
        var angle = (Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
