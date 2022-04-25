using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Keenan Anderson
public class SmolEnemy : MonoBehaviour
{
    public int MoveSpeed;
    public float Delay;
    public float MoveTime;
    public float WaitTime;
    private Transform tf;
    private enum Action { MoveLeft, MoveRight, Wait }

    void Start()
    {
        tf = this.transform;
        StartCoroutine("Movement");
    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(Delay);
        while (true)
        {
            yield return MoveLeft();
            yield return new WaitForSeconds(WaitTime);
            yield return MoveRight();
            yield return new WaitForSeconds(WaitTime);
        }
    }

    IEnumerator MoveLeft()
    {
        Debug.Log("Move left");
        float timer = 0.0f;
        while (timer < MoveTime)
        {
            tf.position = new Vector3(tf.position.x - (MoveSpeed * Time.deltaTime), tf.position.y, tf.position.z);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return 0;
    }
    IEnumerator MoveRight()
    {
        Debug.Log("Move Right");
        float timer = 0.0f;
        while (timer < MoveTime)
        {
            tf.position = new Vector3(tf.position.x + (MoveSpeed * Time.deltaTime), tf.position.y, tf.position.z);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return 0;
    }
}
