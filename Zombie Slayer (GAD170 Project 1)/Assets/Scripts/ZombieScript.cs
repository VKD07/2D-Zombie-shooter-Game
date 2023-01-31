using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using TMPro;

public class ZombieScript : MonoBehaviour
{
    [Header("Zombie Stats")]
    [SerializeField] public float ZombieDamage = 10;
    [SerializeField] public int HP = 5;


    //Object Components
    Animator animator;

    [Header("Scoring")]
    //Score Text Reference
    public GameObject scoreReference;
    [SerializeField] int PointsPerKill = 10;

    private void Start()
    {
        animator = GetComponent<Animator>();
        scoreReference = GameObject.FindGameObjectWithTag("ScoreReference");
    }
    //Collider conditions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the zombie collides with a bullet, destroy the bullet and damage this zombie by 1.
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            HP -= 1;
            //if Zombie HP is 0 then destroy this zombie then add score
            if(HP == 0)
            {
                animator.SetBool("Dead", true);
                scoreReference.GetComponent<UIHandler>().Score += PointsPerKill;
                Destroy(this.gameObject, 0.5f);
            }
        }
        //If this zombie collided with the player, reduce the health of the player by the zombie damage and print the health of the player in the Console
        // Also divide the score by 2 if it hits the zombie
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().Health -= (int)ZombieDamage;
            print("Player Health :" + collision.gameObject.GetComponent<PlayerScript>().Health);
            scoreReference.GetComponent<UIHandler>().Score /= 2;
        }
    }
}
