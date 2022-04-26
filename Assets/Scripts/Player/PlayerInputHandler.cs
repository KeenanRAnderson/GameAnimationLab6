using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Written by Keenan Anderson and Matthew Fawcett
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] RemAnimation remAnimation;

    public float MoveSpeed;
    [SerializeField] float jumpVel;
    [SerializeField] float minTimeJumping, maxTimeJumping;

    private enum Direction { Left, Right };
    private Direction moveDir;
    private bool isMoving, isJumping, jumpPressed;
    private float timeSinceJumpStart = 0;
    private Transform tf;
    private Collider col;
    private Rigidbody rb;

    void Start()
    {
        tf = this.gameObject.transform;
        rb = GetComponent<Rigidbody>();
        col = GetComponentInChildren<Collider>();
        isMoving = false;
        isJumping = false;
        jumpPressed = false;
    }
    
    public void Update()
    {

        if (isMoving)
        {
            UpdatePosition();
            remAnimation.SetMove();
        }

        if (isJumping)
        {
            UpdateJump();
        }
        CheckMovingPlatform();
    }
    private void UpdatePosition()
    {
        if (moveDir == Direction.Left)
        {
            tf.position = new Vector3(tf.position.x - (MoveSpeed * Time.deltaTime), tf.position.y, tf.position.z);
        }
        else if (moveDir == Direction.Right)
        {
            tf.position = new Vector3(tf.position.x + (MoveSpeed * Time.deltaTime), tf.position.y, tf.position.z);
        }
    }

    private void UpdateJump()
    {
        timeSinceJumpStart += Time.deltaTime;

        if ((timeSinceJumpStart < minTimeJumping && !jumpPressed) || (timeSinceJumpStart < maxTimeJumping && jumpPressed))
        {
            rb.velocity = new Vector3(0, jumpVel, 0);
        }

        if (IsGrounded() && rb.velocity.y <= 0)
        {
            isJumping = false;
            timeSinceJumpStart = 0;
            remAnimation.SetJumpGround();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 MoveVector = context.ReadValue<Vector2>();
        if (MoveVector.x < 0)
        {
            isMoving = true;
            moveDir = Direction.Left;
            remAnimation.TurnLeft();
        }
        else if (MoveVector.x > 0)
        {
            isMoving = true;
            moveDir = Direction.Right;
            remAnimation.TurnRight();
        }
        else
        {
            isMoving = false;
        }

        if (!isMoving)
        {
            remAnimation.SetStop();
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && !isJumping && context.started)
        {
            rb.velocity = new Vector3(0, jumpVel, 0);
            isJumping = true;
            remAnimation.SetJumpStart();
            jumpPressed = true;
            Debug.Log("Jump Pressed");
        }

        if (isJumping && jumpPressed && context.performed)
        {
            jumpPressed = false;
            remAnimation.SetJumpPeak();
            Debug.Log("Jump Released");
        }
    }
    private bool IsGrounded()
    {
        //Sends a raycast to see if there is an object below collider. May need to adjust constant for better behaviour
        Debug.DrawRay(transform.position, -Vector3.up, Color.red, col.bounds.extents.y, false);
        return Physics.Raycast(transform.position, -Vector3.up, col.bounds.extents.y);
    }

    private void CheckMovingPlatform()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast(ray, out hit, col.bounds.extents.y))
        {
            Unparent();
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("MovingPlatform"))
                {
                    this.gameObject.transform.parent = hit.collider.gameObject.transform;
                }
            }
        }
    }
    private void Unparent()
    {
        this.gameObject.transform.parent = null;
    }
}
