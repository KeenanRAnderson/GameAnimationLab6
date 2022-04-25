using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Keenan Anderson
public class KillEnemy : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            /*ProtectiveEnemy protEnemy = enemy.GetComponent<ProtectiveEnemy>();
            if (protEnemy && !protEnemy.Attacked())
            {
                enemy.gameObject.SetActive(false);
            }*/
            enemy.gameObject.SetActive(false);
        }
    }
}
