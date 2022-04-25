using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Keenan Anderson
public class MovingPlatform : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float moveSpeed;

    private void Update()
    {
        this.transform.localPosition = Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time * moveSpeed, 1.0f));
    }  
}
