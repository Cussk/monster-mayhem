using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    //public variables 
    public float spawnInterval;
    public int miniEnemySpawnCount;
    public bool isBoss = false;

    //private variables
    [SerializeField] private float speed = 150.0f;
    private float nextSpawn;
    private Rigidbody enemyRb;
    private Animator enemyAnim;
    private GameObject player;
    private SpawnManager spawnManager;
    private GameManager gameManager;

    // Awake is called on scene load
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        //if there is a boss get spawn manager
        if (isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            FollowPlayer();
            MiniEnemySpawn();
        }
        
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
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }
    }
}
