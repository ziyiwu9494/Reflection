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
    private float timeBetweenAttack;
    public float startTimeBetweenAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage; 

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetBool("Hit", true);
                hit = true;
                Collider2D [] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Breakable>().TakeDamage(damage);
                }
            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jump", true);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
