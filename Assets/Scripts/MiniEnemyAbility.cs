using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemyAbility : MonoBehaviour
{
    //private variables 
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
       spawnManager = FindObjectOfType<SpawnManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawns boss with ability to spawn mini enemies
    public void MiniSpawn(int currentRound)
    {
        int miniEnemysToSpawn;

        //if it is a boss round
        if (spawnManager.bossRound != 0)
        {
            //variable equal to currentround divided by boss round
            miniEnemysToSpawn = currentRound / spawnManager.bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }

        var boss = spawnManager.bossPrefabs[0];

        //get miniEnemySpawnCount from enemy script and make equal to miniEnemysToSpawn variable
        boss.GetComponent<Enemies>().miniEnemySpawnCount = miniEnemysToSpawn;
    }

    //spawns random mini enemy
    public void SpawnMiniEnemy(int amount)
    {
        //loops amount of mini enemies spawned until argument amount reached
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, spawnManager.miniEnemyPrefabs.Length);
            Instantiate(spawnManager.miniEnemyPrefabs[randomMini], spawnManager.GenerateSpawnPosition(), spawnManager.miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
}
