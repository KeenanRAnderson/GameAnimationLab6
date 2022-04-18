using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    public ProtectiveEnemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!enemy.Attacked())
            {
                enemy.gameObject.SetActive(false);
            }
        }
    }
}
