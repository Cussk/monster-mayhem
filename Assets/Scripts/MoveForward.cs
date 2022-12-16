using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    //private variables
    private float speed = 30.0f;
    private float aliveTimer = 1.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        Destroy(gameObject, aliveTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Damage enemy
            other.GetComponent<EnemyHealth>().EnemyDamage(1);
            //destroy arrow
            Destroy(gameObject);
        }
    }

}
