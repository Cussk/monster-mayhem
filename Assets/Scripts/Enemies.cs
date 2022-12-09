using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    //public variables 
    public float speed = 3.0f;
    public float spawnInterval;

    //private variables
    private GameObject player;
    //private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        transform.Translate(lookDirection * speed * Time.deltaTime);
    }
}
