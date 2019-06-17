using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour
{
    private float timer;
    private GameObject player;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        var angle = (Mathf.Atan2(pc.direction.y, pc.direction.x) * Mathf.Rad2Deg) + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        timer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
