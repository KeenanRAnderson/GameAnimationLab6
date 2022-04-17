using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] RemAnimation remAnimation;
    public float MoveSpeed;
    public float JumpHeight;
    
    private enum Direction { Left, Right };
    private Direction moveDir;
    private bool isMoving;
    private float jumpForce;
    private Transform tf;
    private Collider col;
    private Rigidbody rb;

    void Start()
    {
        jumpForce = Mathf.Sqrt(JumpHeight * -2 * Physics.gravity.y);
        tf = this.gameObject.transform;
        rb = GetComponent<Rigidbody>();
        col = GetComponentInChildren<Collider>();
        isMoving = false;
    }
    
    public void Update()
    {
        if (isMoving)
        {
            UpdatePosition();
        }
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
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move");
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
        if (isMoving)
        {
            remAnimation.SetMove();
        }
        else
        {
            remAnimation.SetStop();
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {        
        if (IsGrounded() && context.started)
        {
            Debug.Log("Add force");
            rb.AddForce(jumpForce * Vector2.up, ForceMode.Impulse);
            remAnimation.SetJump();
        }
    }
    private bool IsGrounded()
    {
        //Sends a raycast to see if there is an object below collider. May need to adjust constant for better behaviour
        Debug.DrawRay(transform.position, -Vector3.up, Color.red, 1f, false);
        return Physics.Raycast(transform.position, -Vector3.up, col.bounds.extents.y + 0.1f);
    }
}
