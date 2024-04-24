using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerMovement : MonoBehaviour
{
    private PlayerInput playerInput;


    [Header("MOvement And Rotation")]

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private Vector3 rotationDirection;


    [Header("jump")]

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float maximalJumpHight;
    [SerializeField] private float jumpForce;

    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.ControllerMovementInput.ContollerMovement.performed += context => movementDirection = context.ReadValue<Vector2>();
        playerInput.ControllerMovementInput.ContollerMovement.canceled += context => movementDirection = Vector2.zero;


        playerInput.ControllerMovementInput.ContollerMovement.performed += context => rotationDirection = context.ReadValue<Vector2>();
        playerInput.ControllerMovementInput.ContollerMovement.canceled += context => rotationDirection = Vector2.zero;
    }

    private void OnEnable()
    {
        playerInput.ControllerMovementInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.ControllerMovementInput.Disable();
    }

    private void Update()
    {
        Movement();
        
    }

    private void Movement()
    {
        Vector2 movementInput = playerInput.ControllerMovementInput.ContollerMovement.ReadValue<Vector2>();

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
        print("123456789");
        if (Physics.Raycast(transform.position, Vector3.down, maximalJumpHight))
        {
            rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            //isGrounded = true;
        }
    }

}
