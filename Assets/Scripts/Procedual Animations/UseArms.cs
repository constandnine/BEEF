using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseArms : MonoBehaviour
{
    public PlayerInput playerInput;


    [Header("Timers")]

    [SerializeField] private float leftBumperPressedTime;
    [SerializeField] private float rightBumperPressedTime;

    private bool startLeftBumperPressedTimer;
    private bool startRightBumperPressedTimer;


    [Header("Punch And Grab")]

    [SerializeField] private float punchSpeed;


    [SerializeField] private GameObject leftArmTarget;
    [SerializeField] private GameObject rightArmTarget;

    [SerializeField] private GameObject rightArmExteendedWaypoint;
    [SerializeField] private GameObject rightArmRestwaypoint;
    [SerializeField] private GameObject leftArmExteendedWaypoint;
    [SerializeField] private GameObject leftArmRestwaypoint;

    private bool leftBumperPressed;
    private bool rightBumperPressed;
    private bool leftBumperHold;
    private bool rightBumperHold;

    private bool leftArmExtendedWaypointReached;
    private bool rightArmExtendedWaypointReached;

    public bool rightPunch;
    public bool leftPunch;
    private bool leftPunchIsDone;
    private bool rightPunchIsDone;

    private float distanceToArmExtendedWaypoint;
    private float distanceToArmRestWaypoint;



    // Runs the code before the first frame has been called
    private void Awake()
    {
        playerInput = new PlayerInput();


        // Looks if thew input action is being preformed or has been canceled canceled and sets a bool to true or fasle 
        playerInput.ControllerMovementInput.UseLeftArm.performed += context => startLeftBumperPressedTimer = true; 
        playerInput.ControllerMovementInput.UseLeftArm.canceled += context => startLeftBumperPressedTimer = false;


        // Looks if thew input action is being preformed or has been canceled canceled and sets a bool to true or fasle 
        playerInput.ControllerMovementInput.UseRightArm.performed += context => startRightBumperPressedTimer = true; 
        playerInput.ControllerMovementInput.UseRightArm.canceled += context => startRightBumperPressedTimer = false;
    }

    // Runs the code each time the action has been enabled
    private void OnEnable()
    {
        playerInput.ControllerMovementInput.Enable();
    }

    // Runs the code each time the action has been canceled
    private void OnDisable()
    {
        playerInput.ControllerMovementInput.Disable();
    }

    // Runs the code every rime a frame is called
    private void Update()
    {
        StartTImers();
        PunchAndGrabAnimation();
        PunchesAreDone();
    }

    // Runs the code eeverytime this void is called
    private void StartTImers()
    {
        // Checks if the left bumper has beeen pressed and if so will start the timer of how longit has been pressed
        if (startLeftBumperPressedTimer)
        {
            leftBumperPressedTime += Time.deltaTime;
        }

        // Resets the timer to 0 after the left bumper has been released
        else
        {
            leftBumperPressedTime = 0f;
        }


        // Checks if the right bumper has beeen pressed and if so will start the timer of how longit has been pressed
        if (startRightBumperPressedTimer)
        {
            rightBumperPressedTime += Time.deltaTime;
        }

        // Resets the timer to 0 after the right bumper has been released
        else
        {
            rightBumperPressedTime = 0f;
        }
    }

    private void PunchAndGrabAnimation()
    {
        if (leftBumperPressedTime > 0f && leftBumperPressedTime <= 0.25f)
        {
            leftPunch = true;
        }

        if (leftPunch == true)
        {

            if (leftArmExtendedWaypointReached == false)
            {
                 distanceToArmExtendedWaypoint = Vector3.Distance(leftArmTarget.transform.position, leftArmExteendedWaypoint.transform.position);

                 leftArmTarget.transform.position = Vector3.MoveTowards(leftArmTarget.transform.position, leftArmExteendedWaypoint.transform.position, Time.deltaTime * punchSpeed);
            }

            if (distanceToArmExtendedWaypoint <= 0.05f)
            {
                leftArmExtendedWaypointReached = true;

                if(leftArmExtendedWaypointReached == true)
                {
                    distanceToArmRestWaypoint = Vector3.Distance(leftArmTarget.transform.position, leftArmRestwaypoint.transform.position);

                    leftArmTarget.transform.position = Vector3.MoveTowards(leftArmTarget.transform.position, leftArmRestwaypoint.transform.position, Time.deltaTime * punchSpeed);
                }

                if (distanceToArmRestWaypoint <= 0.05f)
                {
                    leftPunchIsDone = true;
                }
            }
        }

        if (rightBumperPressedTime >0f && leftBumperPressedTime <= 0.25f)
        {
            rightPunch = true;
        }

        if (rightPunch == true)
        {

            if (rightArmExtendedWaypointReached == false)
            {
                distanceToArmExtendedWaypoint = Vector3.Distance(rightArmTarget.transform.position, rightArmExteendedWaypoint.transform.position);

                rightArmTarget.transform.position = Vector3.MoveTowards(rightArmTarget.transform.position, rightArmExteendedWaypoint.transform.position, Time.deltaTime * punchSpeed);
            }

            if (distanceToArmExtendedWaypoint <= 0.05f)
            {
                rightArmExtendedWaypointReached = true;

                if(rightArmExtendedWaypointReached == true)
                {
                    distanceToArmRestWaypoint = Vector3.Distance(rightArmTarget.transform.position, rightArmRestwaypoint.transform.position);

                    rightArmTarget.transform.position = Vector3.MoveTowards(rightArmTarget.transform.position, rightArmRestwaypoint.transform.position, Time.deltaTime * punchSpeed);
                }

                if (distanceToArmRestWaypoint <= 0.05f)
                {
                    rightPunchIsDone = true;
                }
            }
        }
    }

    private void PunchesAreDone()
    {
        if (leftPunchIsDone == true)
        {
            leftPunch = false;
            leftArmExtendedWaypointReached= false;
            leftPunchIsDone= false;
        }
        
        if (rightPunchIsDone == true)
        {
            rightPunch= false;
            rightArmExtendedWaypointReached= false;
            rightPunchIsDone= false;
        }
    }
}
