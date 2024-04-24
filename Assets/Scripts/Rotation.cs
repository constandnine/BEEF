using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;
using UnityEngine.InputSystem;

public class Rotation : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 rotationDirection;

    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.ControllerMovementInput.ContollerMovement.performed += context => rotationDirection = context.ReadValue<Vector2>();
        playerInput.ControllerMovementInput.ContollerMovement.canceled += context => rotationDirection = Vector2.zero;
    }

    void Update()
    {
        rotationDirection.x = playerInput.ControllerMovementInput.ContollerMovement.ReadValue<Vector2>().x;
        rotationDirection.y = 0;
        rotationDirection.z = playerInput.ControllerMovementInput.ContollerMovement.ReadValue<Vector2>().y;

        transform.Translate(rotationDirection * Time.deltaTime * movementSpeed);
    }
}
