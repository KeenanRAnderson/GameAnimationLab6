using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Keenan Anderson and Matthew Fawcett
public class CameraFollow : MonoBehaviour
{
    [SerializeField] float teleRate;
    [SerializeField] Vector3 offsetVector;

    public Transform targetPlayer;

    bool teleMove = false;

    void Update()
    {
        if (!teleMove)
        {
            //Move camera x to follow player
            transform.position = targetPlayer.position + offsetVector;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position + offsetVector, Vector3.Distance(transform.position, targetPlayer.position) / teleRate);
            if (Vector3.Distance(transform.position + offsetVector, targetPlayer.position) < 1)
            {
                teleMove = false;
            }
        }
    }

    public void teleport()
    {
        teleMove = true;
    }
}
