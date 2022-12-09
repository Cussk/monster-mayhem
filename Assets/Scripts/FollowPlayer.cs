using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //public variables
    public GameObject player;

    private Vector3 offset = new Vector3(0, 12, -1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LateUpdate is called once per frame after regular update
    void LateUpdate()
    {
        //camera position follows player position with offset
        transform.position = player.transform.position + offset;
    }
}
