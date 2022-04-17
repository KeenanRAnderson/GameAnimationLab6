using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemAnimation : MonoBehaviour
{
    private enum MoveState
    {
        Walking,
        ReturningToInitial,
        Stopped
    };

    private enum CapeState
    {
        MovingUp,
        MovingDown,
        Stopped
    }

    [SerializeField] GameObject full;
    [SerializeField] GameObject leftArm;
    [SerializeField] GameObject rightArm;
    [SerializeField] GameObject leftLeg;
    [SerializeField] GameObject rightLeg;
    [SerializeField] GameObject cape;

    MoveState moveState = MoveState.Stopped;
    CapeState capeState = CapeState.Stopped;

    [SerializeField] float limbSpeed;
    [SerializeField] float maxLimbRotation;
    [SerializeField] float capeSpeed;
    [SerializeField] float minCapeRotation;
    [SerializeField] float maxCapeRotation;

    private int limbDirection = 1;   // This will be either 1 or negative 1

    void Update()
    {
        Vector3 angles, oppositeAngles;

        switch(moveState)
        {
            case MoveState.Walking:

                angles = leftLeg.transform.rotation.eulerAngles;
                angles.z += Time.deltaTime * limbDirection * limbSpeed;

                // Check if the max or min angle has been breached.  This has  so many exprs because Unity doesn't have negative angles
                if ((limbDirection == 1 && angles.z > maxLimbRotation && angles.z < 360f - maxLimbRotation) || (limbDirection == -1 && angles.z < 360f - maxLimbRotation && angles.z > maxLimbRotation))
                {
                    angles.z = maxLimbRotation * limbDirection;
                    limbDirection *= -1;
                }

                oppositeAngles = angles;
                oppositeAngles.z *= -1;
                
                leftLeg.transform.eulerAngles = angles;
                rightLeg.transform.eulerAngles = oppositeAngles;
                leftArm.transform.eulerAngles = oppositeAngles;
                rightArm.transform.eulerAngles = angles;

                break;
            case MoveState.ReturningToInitial:

                angles = leftLeg.transform.rotation.eulerAngles;
                angles.z += Time.deltaTime * limbDirection * limbSpeed;

                // Check if the limbs have gone past 0 degrees
                if ((limbDirection == 1 && angles.z < maxLimbRotation) || (limbDirection == -1 && angles.z >  maxLimbRotation))
                {
                    angles.z = 0;
                    moveState = MoveState.Stopped;
                    capeState = CapeState.MovingDown;
                }

                oppositeAngles = angles;
                oppositeAngles.z *= -1;

                leftLeg.transform.eulerAngles = angles;
                rightLeg.transform.eulerAngles = oppositeAngles;
                leftArm.transform.eulerAngles = oppositeAngles;
                rightArm.transform.eulerAngles = angles;

                break;
            case MoveState.Stopped:
                break;
        }
    
        // Some temp code that only works for movement, not jumping
        switch(capeState)
        {
            case CapeState.MovingUp:
                angles = cape.transform.rotation.eulerAngles;
                angles.z -= Time.deltaTime * capeSpeed;

                if (angles.z < 360f - maxCapeRotation)
                {
                    angles.z = -maxCapeRotation;
                    capeState = CapeState.Stopped;
                }

                cape.transform.eulerAngles = angles;

                break;
            case CapeState.MovingDown:
                angles = cape.transform.rotation.eulerAngles;
                angles.z += Time.deltaTime * capeSpeed;

                if (angles.z > 360f - minCapeRotation)
                {
                    angles.z = -minCapeRotation;
                    capeState = CapeState.Stopped;
                }

                cape.transform.eulerAngles = angles;

                break;
            case CapeState.Stopped:
                break;
        }
    }

    public void SetMove()
    {
        moveState = MoveState.Walking;
        capeState = CapeState.MovingUp;
    }

    public void SetJump()
    {
        moveState = MoveState.Walking;
    }
    public void SetStop()
    {
        moveState = MoveState.ReturningToInitial;
        // Check to see if the limbs need to change direction
        if ((limbDirection == 1 && leftLeg.transform.rotation.eulerAngles.z < maxLimbRotation) || (limbDirection == -1 && leftLeg.transform.rotation.eulerAngles.z > maxLimbRotation))
        {
            limbDirection *= -1;
        }
    }

    public void TurnRight()
    {
        // I wish you could stright edit from the property
        Vector3 angles = full.transform.rotation.eulerAngles;
        angles.y = 0;
        full.transform.eulerAngles = angles;
    }

    public void TurnLeft()
    {
        Vector3 angles = full.transform.rotation.eulerAngles;
        angles.y = 180;
        full.transform.eulerAngles = angles;
    }
}
