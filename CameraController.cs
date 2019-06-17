using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraController : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject player;

    [Space]

    [Header("Object Offsets")]
    private Vector3 p_offset;

    void Start()
    {
        player = GameObject.Find("Player");
        p_offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + p_offset;
    }
}
