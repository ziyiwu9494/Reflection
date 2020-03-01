using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector2 p;
    // Start is called before the first frame update
    void Start()
    {
       p = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        p.x = player.position.x;
        transform.position = p;
    }
}
