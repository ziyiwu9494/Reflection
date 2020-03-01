using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public int health;
    public Destroy_warp warper;
    public AudioSource audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        audioPlayer.Play();
        
        if(health <= 0)
        {
            warper.OnDeath();
        }
    }
}
