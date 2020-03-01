using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// place on a scenemanager or like.  Loads in players and objects as they appear 
/// only player is programmed now though
/// </summary>
public class LoadingScene : MonoBehaviour
{
    GameObject player;
    GameObject mirror;
    GameObject obj;
    Dictionary<string, string> objectDict = new Dictionary<string, string>();
    // Start is called before the first frame update
    void Start()
    {
        objectDict = new Dictionary<string, string>();
        string name = SceneManager.GetActiveScene().name;
        string active = SceneManagerWithParameters.GetParam(name + "Active");
        Debug.Log("active: " + active);
        if (active == ""){
            GameObject[] destroyable = GameObject.FindGameObjectsWithTag("Breakable");
            foreach (GameObject des in destroyable)
            {
                Debug.Log(des.name);
                if (des.GetComponent<SpriteRenderer>().enabled == false)
                {
                    objectDict.Add(des.name, "Destroyed");
                }
                else
                {
                    objectDict.Add(des.name, des.transform.position.ToString());
                }
                Debug.Log("dictionary- " + des.name + ": " + objectDict[des.name]);
            }
        } else{
            string[] sArray = active.Split(';');
            foreach(string s in sArray)
            {
                Debug.Log(s);
                string[] unit = s.Split(':');
                objectDict.Add(unit[0], unit[1]);
                Debug.Log(unit[0] + ":" + unit[1]);
            }
        }


        player = GameObject.Find("Player");
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
                    
                    obj = GameObject.Find(i);
                    objectDict[i]= SceneManagerWithParameters.GetParam(i);
                    Vector3 pos = StringToVector3(SceneManagerWithParameters.GetParam(i));
                }
                destroyed = "";
                SceneManagerWithParameters.SetParam("Destroyed", destroyed);
            }
        }
        foreach(string k in objectDict.Keys)
        {
            obj = GameObject.Find(k);
            if (objectDict[k].Equals("Destroyed"))
            {
                obj.GetComponent<Rigidbody2D>().simulated = false;
                obj.GetComponent<SpriteRenderer>().enabled = false;
                obj.GetComponent<BoxCollider2D>().enabled = false;

            } else
            {

                obj.transform.position = StringToVector3(objectDict[k]);
                obj.GetComponent<Rigidbody2D>().simulated = true;
                obj.GetComponent<SpriteRenderer>().enabled = true;
                obj.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
    void Destroy(string name)
    {
        Debug.Log(name);
        objectDict[name] = "Destroyed";
    }
    void Leave(string scene)
    {

        string name = SceneManager.GetActiveScene().name;
        string result="";
        foreach (string k in objectDict.Keys)
        {
            if (objectDict[k].Equals("Destroyed"))
                result += ";" + k + ":" + objectDict[k];
            else
                result += ";" + k + ":" + GameObject.Find(k).transform.position;

        }
        if (result!="")
            result = result.Substring(1);
        Debug.Log("result: "+result);
        SceneManagerWithParameters.SetParam(name + "Active",result);
        SceneManagerWithParameters.Load(scene, "warped", "true");
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
        Vector3 result = new Vector3(

            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2])
            );

        return result;
    }
}
