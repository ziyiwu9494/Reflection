using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_warp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        OnDeath();
    }
    public void OnDeath()
    {

       string c = SceneManagerWithParameters.GetParam("Destroyed");
        if (c == "")
            c = this.gameObject.name;
        else
            c+= ","+this.gameObject.name;
        Debug.Log(c);
        SceneManagerWithParameters.SetParam("Destroyed", c);
        SceneManagerWithParameters.SetParam(this.gameObject.name, this.gameObject.transform.position.ToString());
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
        GameObject.Find("SceneManager").SendMessage("Destroy", this.gameObject.name);
    }
}
