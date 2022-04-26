using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Keenan Anderson
public class PlayerCommands : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject dieScreen;
    public GameObject playerModel;
    public int waitTime;
    public float deathByYValue;
    private Vector3 startPos;
    private void Start()
    {
        startPos = playerModel.transform.position + new Vector3(0,1,0);
    }
    public void Update()
    {
        if (playerModel.transform.position.y < deathByYValue)
        {
            StartCoroutine(Die());
        }
    }
    public IEnumerator Die()
    {
        dieScreen.SetActive(true);
        playerModel.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        playerModel.transform.position = startPos;
        dieScreen.SetActive(false);
        playerModel.SetActive(true);
    }
    public IEnumerator Win()
    {
        winScreen.SetActive(true);
        playerModel.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        playerModel.transform.position = startPos;
        winScreen.SetActive(false);
        playerModel.SetActive(true);
    }
}
