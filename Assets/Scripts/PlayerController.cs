using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public variables
    public bool hasPowerup;
    public GameObject powerupIndicator;
    public GameObject missilePrefab;
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public PowerupType currentPowerup = PowerupType.None;

    //private variables
    private float speed = 300.0f;
    private float powerupStrength = 75.0f;
    private Rigidbody playerRb;
    private GameObject tmpMissile;
    private Coroutine powerupCountdown;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb= GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            MovePlayer();
            //attaches indicator on movement
        }
            powerupIndicator.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            //powertype missile and presses F key fires missiles
            if (currentPowerup == PowerupType.MagicMissile && Input.GetKeyDown(KeyCode.F))
            {
                LaunchMissiles();
            }
            //powerup type Arrows and presses space bar, fire arrows
            if (currentPowerup == PowerupType.Arrows && Input.GetKeyDown(KeyCode.Space))
            {
                FireArrow();
            }
        
    }

    //moves player with arrow/wasd
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward.normalized * speed * verticalInput); //vertical movement
        playerRb.AddForce(Vector3.right.normalized * speed * horizontalInput); //side-to-side movement
    }

    //damage enemy on collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerup == PowerupType.Damage)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            //Damage enemy
            enemyRigidbody.GetComponent<EnemyHealth>().EnemyDamage(1);
            //collision direction away
            Vector3 awayFromPlayer = collision.gameObject.transform.position;
            //push force from collision
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            
        }
        else if (collision.gameObject.CompareTag("Enemy") && currentPowerup == PowerupType.None)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            //collision direction away
            Vector3 awayFromPlayer = collision.gameObject.transform.position;
            //push force from collision
            enemyRigidbody.AddForce(awayFromPlayer * 50.0f, ForceMode.Impulse);
            //damage player
            gameManager.UpdateLives(-1);
        }
    }

    //destroy powerup on collision and set powerup type
    private void OnTriggerEnter(Collider other)
    {
        //activates powerup of different type
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerup = other.gameObject.GetComponent<Powerup>().powerupType;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
        }
        //resets to new powerup on new pickup
        if (powerupCountdown != null)
        {
            StopCoroutine(powerupCountdown);
        }
        powerupCountdown = StartCoroutine(PowerupCountdownRoutine());

        if (other.gameObject.CompareTag("Heart"))
        {
            //add life
            gameManager.UpdateLives(+1);
            Destroy(other.gameObject);
        }
    }

    //coroutine powerup countdown after 5 seconds deactivate, spawn new powerup
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        currentPowerup = PowerupType.None;
        powerupIndicator.gameObject.SetActive(false);
        spawnManager.SpawnPowerup();
    }

    void LaunchMissiles()
    {
        foreach (var enemy in FindObjectsOfType<Enemies>())
        {
            //spawns rockets, launches in y-axis to not pushback player, no quaternion rotation
            tmpMissile = Instantiate(missilePrefab, transform.position + Vector3.up, Quaternion.identity);
            //Calls Fire method and targets enemies
            tmpMissile.GetComponent<MagicMissile>().Fire(enemy.transform);
        }
    }

    void FireArrow()
    {
        Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowPrefab.transform.rotation);
    }
}
