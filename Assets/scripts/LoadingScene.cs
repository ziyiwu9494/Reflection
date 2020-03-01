using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// place on a scenemanager or like.  Loads in players and objects as they appear 
/// only player is programmed now though
/// </summary>
public class LoadingScene : MonoBehaviour
{
    GameObject player;
    GameObject mirror;
    
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
                Debug.Log(velString);
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
                Debug.Log(v);
                playerRb.velocity = v;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Mirror_warp c = (Mirror_warp)mirror.gameObject.GetComponent("Mirror_warp");
        c.warped = false;
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

}
