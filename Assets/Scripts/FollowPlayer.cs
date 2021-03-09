using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Public fields
    // The player object it needs to follow
    public Transform player;
    // The offset of the camera
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        // Change the camera position
        transform.position = player.position + offset;
    }
}
