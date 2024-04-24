using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField] float movementSpeed;
    [SerializeField] float movementRotationSpeed;
    [SerializeField] Vector2 move;


    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), movementRotationSpeed);
        }

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
    }

    public void OnKeyboardMovement(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

}
