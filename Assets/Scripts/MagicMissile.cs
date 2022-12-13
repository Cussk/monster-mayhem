using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    //private variables
    private Transform target;
    private bool homing;
    private float speed = 15.0f;
    private float missileStrength = 15.0f;
    private float aliveTimer = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (homing && target != null)
        {
            //missile moving toward target
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            //missile movement speed
            transform.position += moveDirection * speed * Time.deltaTime;
            //faces missile to target
            transform.LookAt(target);
        }
    }

    //fires missiles and destroys if no collision after set time
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        //destroy missile after set time
        Destroy(gameObject, aliveTimer);
    }

    //missile collision and damage
    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            //if matches target tag collide
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                //determines collision contact and direction of pushback
                Vector3 away = -collision.contacts[0].normal;
                //push target away
                targetRigidbody.AddForce(away * missileStrength, ForceMode.Impulse);
                //damage enemy
                targetRigidbody.GetComponent<EnemyHealth>().EnemyDamage(2);
                //destroy missile
                Destroy(gameObject);
            }
        }
    }
}
