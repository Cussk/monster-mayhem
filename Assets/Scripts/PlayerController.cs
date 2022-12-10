using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public variables
    public bool hasPowerup;
    public GameObject powerupIndicator;
    public GameObject missilePrefab;
    public PowerupType currentPowerup = PowerupType.None;

    //private variables
    private float speed = 10.0f;
    private float powerupStrength = 10.0f;
    private float powerupDamage = 1.0f;
    private GameObject tmpMissile;
    private Coroutine powerupCountdown;
    private SpawnManager spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        //attaches indicator on movement
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
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

        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput); //vertical movement
        transform.Translate(Vector3.right * Time.deltaTime *speed * horizontalInput); //side-to-side movement
    }

    //damage enemy on collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerup == PowerupType.Damage)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            //collision direction away
            Vector3 awayFromPlayer = collision.gameObject.transform.position;
            //push force from collision
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            //Damage enemy
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //damage player
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
            tmpMissile = Instantiate(missilePrefab, transform.position +Vector3.up, Quaternion.identity);
            //Calls Fire method and targets enemies
            tmpMissile.GetComponent<MagicMissile>().Fire(enemy.transform);
        }
    }


}
