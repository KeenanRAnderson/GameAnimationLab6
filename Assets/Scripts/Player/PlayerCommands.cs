using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Keenan Anderson
public class PlayerCommands : MonoBehaviour
{
    public float deathByYValue;
    private Vector3 startPos;
    private void Start()
    {
        startPos = this.transform.position;
    }
    public void Update()
    {
        if (this.transform.position.y < deathByYValue)
        {
            Die();
        }
    }
    public void Die()
    {
        this.transform.position = startPos;
    }
}
