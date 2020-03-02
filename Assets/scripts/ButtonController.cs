using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Animator animator;    
    // Start is called before the first frame update
    public void ButtonClicked()
    {
        animator.SetBool("Begin", true);   
    }
}
