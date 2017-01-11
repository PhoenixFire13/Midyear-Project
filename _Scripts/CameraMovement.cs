using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    Vector3 playerPos;
    GameObject player;

    public Vector3 offset;
    public float followSpeed;

    void Start()
    {
        playerPos = transform.parent.position;
    }

    void Update()
    {
        if (player)
        {
            transform.position = Vector3.Lerp(transform.position, playerPos + offset, followSpeed);
        }
    }

}
