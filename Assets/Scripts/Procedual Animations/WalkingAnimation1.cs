using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This script is responseble for the procedual walking animation.
public class WalkingAnimation1 : MonoBehaviour
{
    [Header("Waypoints")]

    [SerializeField] private GameObject leftLegForwardWaypoint;
    [SerializeField] private GameObject rightLegForwardWaypoint;

    [SerializeField] private GameObject leftLegBackwardWaypoint;
    [SerializeField] private GameObject rightLegBackwardWaypoint;

    private float distanceToleftLegForwardWaypoint;
    private float distanceToRightLegForwardWaypoint;

    private float distanceToLeftLegbackwardWaypoint;
    private float distanceToRightLegbackwordWaypoint;

    [SerializeField] private float legSpeed;


    [Header("Rig Components")]

    [SerializeField] private GameObject leftLegTarget;
    [SerializeField] private GameObject rightLegTarget;


    [Header("Input")]

    [SerializeField] private PlayerInput playerInput;



    private bool moveRightLegforward;
    private bool moveLeftLegBackwards;

    [SerializeField] private bool iswalking;



    private void Awake()
    {
        iswalking = false;

        playerInput = new PlayerInput();


        playerInput.XboxInput.ContollerMovement.performed += context => iswalking = true;
        playerInput.XboxInput.ContollerMovement.canceled += context => iswalking = false;
    }


    private void OnEnable()
    {
        playerInput.XboxInput.ContollerMovement.Enable();
    }


    // Runs the code each time the action has been canceled.
    private void OnDisable()
    {
        playerInput.XboxInput.ContollerMovement.Disable();
    }


    private void Update()
    {
        if (iswalking == true)
        {
            WalkAnimation();
        }
    }


    private void WalkAnimation()
    {
        if(moveLeftLegBackwards == false)
        {
            // Checks the distance to between the leftLegTarget(the target the conected bone has to move towards) and the leftLegForwardWaypoint.
            distanceToleftLegForwardWaypoint = Vector3.Distance(leftLegTarget.transform.position, leftLegForwardWaypoint.transform.position);

            // moves the leftArmTarget towards the leftArmExteendedWaypoint.
            leftLegTarget.transform.position = Vector3.MoveTowards(leftLegTarget.transform.position, leftLegForwardWaypoint.transform.position, Time.deltaTime * legSpeed);


            if (distanceToleftLegForwardWaypoint < 0.05f)
            {
                moveLeftLegBackwards = true;
            }
        }


        if (moveLeftLegBackwards == true)
        {
            // Checks the distance to between the leftLegTarget(the target the conected bone has to move towards) and the leftLegForwardWaypoint.
            distanceToLeftLegbackwardWaypoint = Vector3.Distance(leftLegTarget.transform.position, leftLegBackwardWaypoint.transform.position);

            // moves the leftArmTarget towards the leftArmExteendedWaypoint.
            leftLegTarget.transform.position = Vector3.MoveTowards(leftLegTarget.transform.position, leftLegBackwardWaypoint.transform.position, Time.deltaTime * legSpeed);


            if (distanceToLeftLegbackwardWaypoint < 0.05f)
            {
                moveLeftLegBackwards = false;
            }
        }


        if(moveRightLegforward == false)
        {
            // Checks the distance to between the rightLegTarget(the target the conected bone has to move towards) and the leftLegForwardWaypoint.
            distanceToRightLegbackwordWaypoint = Vector3.Distance(rightLegTarget.transform.position, rightLegBackwardWaypoint.transform.position);

            // moves the leftArmTarget towards the leftArmExteendedWaypoint.
            rightLegTarget.transform.position = Vector3.MoveTowards(rightLegTarget.transform.position, rightLegBackwardWaypoint.transform.position, Time.deltaTime * legSpeed);



            if (distanceToRightLegbackwordWaypoint < 0.05f)
            {
                moveRightLegforward = true;
            }
        }


        if (moveRightLegforward == true)
        {
            // Checks the distance to between the leftLegTarget(the target the conected bone has to move towards) and the leftLegForwardWaypoint.
            distanceToRightLegForwardWaypoint = Vector3.Distance(rightLegTarget.transform.position, rightLegForwardWaypoint.transform.position);

            // moves the leftArmTarget towards the leftArmExteendedWaypoint.
            rightLegTarget.transform.position = Vector3.MoveTowards(rightLegTarget.transform.position, rightLegForwardWaypoint.transform.position, Time.deltaTime * legSpeed);


            if (distanceToRightLegForwardWaypoint < 0.05f)
            {
                moveRightLegforward = false;
            }
        }

    }
}
