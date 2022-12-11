using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    //public variables 
    public float speed = 150.0f;
    public float spawnInterval;
    public int miniEnemySpawnCount;
    public bool isBoss = false;

    //private variables
    private float nextSpawn;
    private Rigidbody enemyRb;
    private GameObject player;
    private SpawnManager spawnManager;
    private MiniEnemyAbility miniEnemyAbility;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        //if there is a boss get spawn manager
        if (isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
            miniEnemyAbility = FindObjectOfType<MiniEnemyAbility>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        MiniEnemySpawn();
    }

    private void FollowPlayer()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        enemyRb.AddForce(lookDirection * speed);
    }

    private void MiniEnemySpawn()
    {
        if (isBoss)
        {
            //if game time is greater than next spawn time
            if (Time.time > nextSpawn)
            {
                //next spawn time is equal to game time plus length of spawnINterval
                nextSpawn = Time.time + spawnInterval;
                //calls spawn manger to spawn a certain amonut of mini enemies
                miniEnemyAbility.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }
    }
}
