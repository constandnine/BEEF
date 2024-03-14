using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction move;

    [SerializeField] private Rigidbody hipsRigedbody;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float maximalMovementSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Vector3 movement;
    [SerializeField] private Vector3 speed;

    [SerializeField] private bool isGrounded;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();

        move = playerInput.MovementInput.KeyboardMovement;
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void FixedUpdate()
    {
        Movement(move.ReadValue<Vector2>() * Time.deltaTime);
        SpeedController();
        Jumping();
    }

    private void Movement(Vector2 movementVector2)
    {
        if (movementVector2 != null)
        {
            Vector3 movementDirection = (transform.forward * movementVector2.y + transform.right * movementVector2.x);
            movementDirection = new Vector3(movementDirection.x, 0, movementDirection.z);

            hipsRigedbody.AddForce(movementDirection * movementSpeed);

        }

        else
        {
            hipsRigedbody.velocity = new Vector3(0, hipsRigedbody.velocity.y, 0);
        }
    }

    private void SpeedController()
    {
         speed = new Vector3(hipsRigedbody.velocity.x, 0, hipsRigedbody.velocity.z);

        if (speed.magnitude > maximalMovementSpeed)
        {
            Vector3 speedLimit = speed.normalized * maximalMovementSpeed;
            hipsRigedbody.velocity = new Vector3(speedLimit.x, hipsRigedbody.velocity.y, speedLimit.z);
        }
    }

    private void Jumping()
    {

    }
}
