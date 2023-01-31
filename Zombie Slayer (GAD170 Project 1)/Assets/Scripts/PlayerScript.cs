using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float RunSpeed = 5f;
    [SerializeField] float JumpForce = 400f;

    //Bool if player is on the ground
    bool isOnTheGround = false;

    [Header("Bullet Settings")]
    [SerializeField] GameObject bulletobject;
    [SerializeField] GameObject bulletSpawner;
    [SerializeField] float bulletSpeed = 100f;
    [SerializeField] float BulletReleaseTime = 1f;
    float BulletCountDown;
    Vector3 bulletPosition;

    [Header("Player Stats")]
    [SerializeField] public int Health = 100;

    [Header("Gun Sound")]
    [SerializeField] AudioClip GunShots;

    //Object Components
    SpriteRenderer spriteRenderer;
    Rigidbody2D characterRB;
    Animator CharacterAnimation;
    AudioSource audioSource;

    void Start()
    {
        //Initializing components
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterRB = GetComponent<Rigidbody2D>();
        CharacterAnimation = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        //This function is responsible for player movement
        PlayerMovement();
        //This function is responsibile for the player shooting.
        PlayerGun();
        //Death Condition
        PlayerDeath();

    }

    private void PlayerDeath()
    {
        //If the health of this player is 0 or less then Destroy this player.
        if(Health <= 0)
        {
            Destroy(this.gameObject);
        //If player died, they can reset the scene by pressing tab
        }
    }

    private void PlayerMovement()
    {
        //If player press D, the character will run to the right or if the player press A, the character will run to the left with an amount of speed.
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * RunSpeed * Time.deltaTime;

            //If the scale of the character is negative then turn it into positive, making the character face to the right
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                
            }

            //Play Character Running
            CharacterAnimation.SetBool("isRunning", true);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * RunSpeed * Time.deltaTime;

            //If the scale of the character is positive, turn it into negative, making the character face to the left
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            //Play Character Running
            CharacterAnimation.SetBool("isRunning", true);
        }
        else
        {
            //Stop Running Animation
            CharacterAnimation.SetBool("isRunning", false);
        }

        //If player press Space and on the ground, then the  character will go up with an amount of jump force
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround == true)
        {
            //Disables the player to jump again while in the air;
            isOnTheGround = false;

            characterRB.AddForce(Vector3.up * JumpForce);

            //Play Character Jump Animation
            CharacterAnimation.SetTrigger("isJumping");
        }
    }

    private void PlayerGun()
    {
        //If player presses left mouse button, it will fire a bullet
        //If the BulletCountDown reaches 3 seconds or more, the bullet will be released, this will control the fire rate of the gun
        if (Input.GetKey(KeyCode.Mouse0) && BulletCountDown >= BulletReleaseTime)
        {
            //if the gun shot is already playing then dont play anymore gun shot sounds
            if (audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(GunShots);
            }

            //After a bullet has been released it will have to wait for some time to be able to release another one
            BulletCountDown = 0;
            //Bullet will spawn from the bullet spawner without any rotation, and making the this as an object to gain access to its components.
            GameObject bullet = Instantiate(bulletobject, bulletSpawner.transform.position, Quaternion.identity);
            //If the player is facing left then the bullet direction will go to the left, else if facing right, then the bullet direction will go right with an amount of speed
            if (transform.localScale.x < 0)
            {
               bulletPosition = Vector3.left * bulletSpeed * Time.deltaTime;
            }
            else
            {
              bulletPosition = Vector3.right * bulletSpeed * Time.deltaTime;
            }
            //giving velocity to the bullet object that is instantiated
            bullet.GetComponent<Rigidbody2D>().velocity = bulletPosition;

            //Play Shooting Animation
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                CharacterAnimation.SetBool("isShootingRunning", true);
            }
            else
            {
                CharacterAnimation.SetTrigger("isShooting");
            }

        }
        //This will be the time of the bullet
        BulletCountDown += Time.deltaTime;      
    }

    //Enables player to jump if the player is collided on the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isOnTheGround = true;
        }
    }

}//End of Class bracket
