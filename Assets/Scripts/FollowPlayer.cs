using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //public variables
    public GameObject player;

    private Vector3 offset = new Vector3(0.0f, 11.0f, -5.0f);

    // LateUpdate is called once per frame after regular update
    void LateUpdate()
    {
        //camera position follows player position with offset
        transform.position = player.transform.position + offset;
    }
}
