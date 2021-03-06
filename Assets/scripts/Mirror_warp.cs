﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror_warp : MonoBehaviour
{
    public string warplocation;
    public MirrorType kind;
    public string scene;
    public float distance=1;
    public bool warped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public enum MirrorType
    {
        sideways,
        updown
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
            if (Input.GetKeyDown(KeyCode.X) &&other.gameObject.name == "Player" && !warped)
            {

                SceneManagerWithParameters.SetParam("location", warplocation);
                if (kind == MirrorType.sideways)
                {
                    SceneManagerWithParameters.SetParam("type", "sideways");
                    if (other.gameObject.transform.position.x > transform.position.x)
                        SceneManagerWithParameters.SetParam("orientation", "right");
                    else
                        SceneManagerWithParameters.SetParam("orientation", "left");

                    SceneManagerWithParameters.SetParam("velocity", other.GetComponent<Rigidbody2D>().velocity.ToString());
                    SceneManagerWithParameters.SetParam("vertposition", (other.transform.position.y - transform.position.y).ToString());

                }
                if (kind == MirrorType.updown)
                {
                    SceneManagerWithParameters.SetParam("type", "updown");


                    SceneManagerWithParameters.SetParam("velocity", other.GetComponent<Rigidbody2D>().velocity.ToString());
                    SceneManagerWithParameters.SetParam("horiposition", (other.transform.position.x - transform.position.x).ToString());

                }
                //GameObject.Find("MainCamera").SendMessage("StoreTime");
                GameObject.Find("SceneManager").SendMessage("Leave", scene);
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        warped = false;
    }
}
