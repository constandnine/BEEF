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
    [SerializeField] private float jumpForce;

    [SerializeField] private Vector3 movement;

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
    }

    private void Jumping()
    {

    }
}
