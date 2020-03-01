using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    // Start is called before the first frame update
    float horizontalMove = 0;
    public float runSpeed = 40f;
    bool jump = false;
    bool hit = false;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jump", true);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            hit = true;
            animator.SetBool("Hit", true);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }   

    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime, false, jump);
        jump = false;
        animator.SetBool("Hit", false);
    }
    
}
