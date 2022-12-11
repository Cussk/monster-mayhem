using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnManager : MonoBehaviour
{
    //public variables
    public int enemyCount;
    public int waveNumber = 1;
    public int bossRound;
    public GameObject heart;
    public GameObject[] bossPrefabs;
    public GameObject[] miniEnemyPrefabs;
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;

    //private variables
    private float spawnRange = 20.0f;
    private MiniEnemyAbility miniEnemyAbility;

    // Start is called before the first frame update
    void Start()
    {
        miniEnemyAbility = FindObjectOfType<MiniEnemyAbility>();
        //initial enemy wave
        SpawnEnemyWave(waveNumber);
        //initial powerup
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemies>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;

            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(); //spawn boss
                SpawnHeart(); //spawn heart
                //if spawning boss exists activate mini spawn ability
                if (bossPrefabs[0] != null)
                {
                    miniEnemyAbility.MiniSpawn(waveNumber);
                }
            }
            else
            {
                SpawnEnemyWave(waveNumber); //spawn more enemies each wave
            }

            SpawnPowerup(); //spawn powerup
        }

        
    }

    //spawn wave of random enemies
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation);
        }
    }

    //spawns random boss
    void SpawnBossWave()
    {
        int randomBoss = Random.Range(0, bossPrefabs.Length);
        Instantiate(bossPrefabs[randomBoss], GenerateSpawnPosition(), bossPrefabs[randomBoss].transform.rotation);
    }

    //spawn random powerup
    public void SpawnPowerup()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
    }

    //spawns heart for healing player lives
    void SpawnHeart()
    {
        Instantiate(heart, GenerateSpawnPosition(), heart.transform.rotation);
    }

    //generate random position for enemy spawn points
    public Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0.5f, spawnPosZ);

        return randomPos;
    }
    
}
