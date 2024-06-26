using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaystationControllerMovement : MonoBehaviour
{
    private PlayerInput playerInput;


    [Header("Movement And Rotation")]

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private Vector3 rotationDirection;


    [Header("jump")]

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float maximalJumpHight;
    [SerializeField] private float jumpForce;



    [Header("Player ID's")]

    public ControllerManager controllerManager;

    private Gamepad playerOneGamepad;


    private void Awake()
    {
        playerInput = new PlayerInput();


        playerInput.ControllerInput.ContollerMovement.performed += context => movementDirection = context.ReadValue<Vector2>();
        playerInput.ControllerInput.ContollerMovement.canceled += context => movementDirection = Vector2.zero;


        playerInput.ControllerInput.ContollerMovement.performed += context => rotationDirection = context.ReadValue<Vector2>();
        playerInput.ControllerInput.ContollerMovement.canceled += context => rotationDirection = Vector2.zero;
    }


    private void OnEnable()
    {
        playerInput.PlaystationInput.Enable();
    }


    private void OnDisable()
    {
        playerInput.PlaystationInput.Disable();
    }


    /// This void is called before the first framee is called.
    private void Start()
    {
        // Makes sure that the controllerManager is conected to the ControllerManager script.
        controllerManager = FindObjectOfType<ControllerManager>();
    }


    private void Update()
    {
        Movement();
    }


    private void Movement()
    {
        Vector2 movementInput = playerInput.PlaystationInput.ContollerMovement.ReadValue<Vector2>();

        if (movementInput != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(-movementInput.y, movementInput.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            float angleRad = targetAngle * Mathf.Deg2Rad;

            Vector3 moveDirection = new Vector3(Mathf.Sin(angleRad), 0, Mathf.Cos(angleRad));

            transform.Translate(moveDirection * Time.deltaTime * movementSpeed, Space.World);
        }
    }


    private void OnJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, maximalJumpHight))
        {
            rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            //isGrounded = true;
        }
    }

}