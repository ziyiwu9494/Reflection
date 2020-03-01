using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// place on a scenemanager or like.  Loads in players updated position and any destroyed objects 

/// </summary>
public class LoadingScene : MonoBehaviour
{
    GameObject player;
    GameObject mirror;
    GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        player= GameObject.Find("Player");
        if (SceneManagerWithParameters.GetParam("warped").Equals("true"))
        {
            mirror = GameObject.Find(SceneManagerWithParameters.GetParam("location"));
            Mirror_warp c = (Mirror_warp)mirror.gameObject.GetComponent("Mirror_warp");
            c.warped = true;
            if (SceneManagerWithParameters.GetParam("type").Equals("sideways"))
            {
    
                float ydiff = float.Parse(SceneManagerWithParameters.GetParam("vertposition"));
                if (SceneManagerWithParameters.GetParam("orientation").Equals("left"))
                {
                    player.transform.position = new Vector2(mirror.transform.position.x+c.distance, mirror.transform.position.y+ydiff);
                } else
                    player.transform.position = new Vector2(mirror.transform.position.x -c.distance, mirror.transform.position.y + ydiff);
                string velString = SceneManagerWithParameters.GetParam("velocity");
                player.GetComponent<Rigidbody2D>().velocity= StringToVector2(velString);

            }
            else
            {

                float xdiff = float.Parse(SceneManagerWithParameters.GetParam("horiposition"));
               
                player.transform.position = new Vector2(mirror.transform.position.x + xdiff, mirror.transform.position.y + c.distance);
                string velString = SceneManagerWithParameters.GetParam("velocity");

                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                Vector2 v = StringToVector2(velString);
                v = new Vector2(v.x, -v.y);
                playerRb.velocity = v;
            }
            string destroyed = SceneManagerWithParameters.GetParam("Destroyed");
            if (destroyed != "")
            {
                string[] sArray = destroyed.Split(',');
                foreach (string i in sArray)
                {

                    Debug.Log(i);
                    obj = GameObject.Find(i);
                    Vector3 pos = StringToVector3(SceneManagerWithParameters.GetParam(i));
                    obj.transform.position = pos;
                    obj.GetComponent<Rigidbody2D>().simulated = true;
                    obj.GetComponent<SpriteRenderer>().enabled = true;
                    obj.GetComponent<BoxCollider2D>().enabled = true;
                }
                destroyed = "";
                SceneManagerWithParameters.SetParam("Destroyed", destroyed);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mirror != null)
        {
            Mirror_warp mw = (Mirror_warp)(mirror.gameObject.GetComponent("Mirror_warp"));
            mw.warped = false;
        }
    }

    Vector2 StringToVector2(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3            
        Debug.Log(sVector);
        Vector2 result = new Vector2(

            float.Parse(sArray[0]),
            float.Parse(sArray[1]));

        return result;
    }
    Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3            
        Debug.Log(sVector);
        Vector3 result = new Vector3(

            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2])
            );

        return result;
    }
}
