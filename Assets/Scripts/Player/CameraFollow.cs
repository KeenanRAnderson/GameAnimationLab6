using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float zOffset;
    public float yOffset;
    public Transform targetPlayer;

    void Update()
    {
        //Move camera x to follow player
        transform.position = new Vector3(targetPlayer.transform.position.x, yOffset, zOffset);
    }
}
