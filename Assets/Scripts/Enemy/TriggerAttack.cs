using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttack : MonoBehaviour
{
    private ProtectiveEnemy enemy;
    private void Start()
    {
        enemy = GetComponentInParent<ProtectiveEnemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.Attack();
        }
    }
}
