using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// records position of any destroyed objects so it can recreate them on scene switch.  Also has onmousedown function for testing, can be disabled if needed
/// </summary>
public class Destroy_warp : MonoBehaviour
{
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
    }
}
