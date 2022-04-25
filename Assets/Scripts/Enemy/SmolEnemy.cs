using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmolEnemy : MonoBehaviour
{
    public int MoveSpeed;

    private enum Action { MoveLeft, MoveRight, Wait }

    void Start()
    {
        StartCoroutine(MoveEnemy());
    }

    IEnumerator MoveEnemy()
    {
        float timer = 0.0f;
        Action moveAction = Action.MoveLeft;
        Transform tf = this.transform;
        while (true)
        {
            moveAction = UpdateAction(timer);
            if (moveAction == Action.MoveLeft)
            {
                tf.position = new Vector3(tf.position.x - (MoveSpeed * Time.deltaTime), tf.position.y, tf.position.z);
            }
            else if (moveAction == Action.MoveRight)
            {
                tf.position = new Vector3(tf.position.x + (MoveSpeed * Time.deltaTime), tf.position.y, tf.position.z);
            }
            timer += Time.deltaTime;
        }
    }

    private Action UpdateAction(float timer)
    {
        Action moveAction;
        if (timer >= 4)
        {
            timer = 0.0f;
            moveAction = Action.MoveLeft;
        }
        else if (timer > 3)
        {
            moveAction = Action.Wait;
        }
        else if (timer > 2)
        {
            moveAction = Action.MoveRight;
        }
        else
        {
            moveAction = Action.Wait;
        }
        return moveAction;
    }
}
