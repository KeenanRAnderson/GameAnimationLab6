using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectiveEnemy : MonoBehaviour
{
    public float force;
    public float moveSpeed;
    private Vector3 startPos;
    private Rigidbody rb;
    private float timer;
    private bool _attacked;

    private void Start()
    {
        _attacked = false;
        startPos = transform.localPosition;
        rb = GetComponent<Rigidbody>();
    }
    public bool Attacked()
    {
        return _attacked;
    }
    public void Update()
    {
        if (!_attacked && timer > 2f)
        {
            if (Random.Range(0,2) == 1)
            {
                transform.Rotate(new Vector3(0,180,0));
            }
            timer = 0;
        }
        else if (_attacked)
        {
            if (timer <= 1.5f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                //Walk back to start pos without
                //transform.LookAt(startPos);
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPos, moveSpeed * Time.deltaTime);

                if (transform.localPosition.x == startPos.x)
                {
                    _attacked = false;
                    Debug.Log("Attacked = false");
                    timer = 0;
                }
            }
            
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
    public void Attack()
    {
        Debug.Log("Attack!");
        _attacked = true;
        Debug.Log("Attacked = true");
        rb.AddForce((transform.forward + transform.up) * force, ForceMode.Impulse);
        timer = 0;
    }
}
