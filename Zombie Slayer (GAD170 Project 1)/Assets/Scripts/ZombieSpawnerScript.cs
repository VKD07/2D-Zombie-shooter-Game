using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    [SerializeField] bool SpawnerLeft;
    float ZombieSpeed;
    bool spawnZombies = true;
    Vector3 Direction;
    GameObject zombies;

    void Update()
    {
        if (spawnZombies == true)
        {
            SpawnZombie();
        }
    }

   void SpawnZombie()
    {
        //Disabling spawning zombies after a certain amount of time.
        spawnZombies = false;
        //Giving Random speed to each zombies spawned
        ZombieSpeed = Random.Range(500, 1000);
       //Spawning zombies in the spawner location with no rotation
        GameObject zombies = Instantiate(zombie, transform.position, Quaternion.identity);
        //This is a condition if this spawner is on the left side or on the right side of the screen
        if(SpawnerLeft == true)
        {
            //if this spawner is on the left side on the screen then zombies will run to the right and they will face to the right
            Direction = Vector3.right;
            zombies.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            //Else if the spawner is on the right then let the zombies run to the left
            Direction = Vector3.left;
        }
        //Getting the instantiated object's RigidBody component to add velocity to it, running to a certain direction with certain amount of speed
        zombies.GetComponent<Rigidbody2D>().velocity = Direction * ZombieSpeed * Time.deltaTime;
        //This function will delay the zombies being spawned
        StartCoroutine(DelayTime());
        
    }
    IEnumerator DelayTime()
    {
        //The delay of the spawn is also randomize from 1 second to 4 seconds until the it can spawn a zombie again
        yield return new WaitForSeconds(Random.Range(1, 5));
        spawnZombies = true;
    }
}// End of class bracket
