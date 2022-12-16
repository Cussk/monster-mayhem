using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    //public variables
    public Slider healthSlider;
    public int maxHealth;

    //private variables
    private int currentHealth;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void EnemyDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.fillRect.gameObject.SetActive(true);
        healthSlider.value = currentHealth;

        if (currentHealth < 1 && gameManager.isGameActive)
        {
            gameManager.UpdateScore(maxHealth);
            Destroy(gameObject, 0.1f);

        }
    }
}
