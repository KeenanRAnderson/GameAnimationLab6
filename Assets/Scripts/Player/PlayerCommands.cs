using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Keenan Anderson
public class PlayerCommands : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject dieScreen;
    public int waitTime;
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
    public IEnumerator Die()
    {
        dieScreen.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        this.transform.position = startPos;
        dieScreen.SetActive(false);
    }
    public IEnumerator Win()
    {
        winScreen.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        this.transform.position = startPos;
        winScreen.SetActive(false);
    }
}
