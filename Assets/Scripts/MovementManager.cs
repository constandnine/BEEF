using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using UnityEngine.UIElements;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction move;
    private InputAction rotate;
    private InputAction jump;


    [SerializeField] private Rigidbody hipsRigedbody;


    [SerializeField] private GameObject ground;


    [SerializeField] private float movementSpeed;
    [SerializeField] private float maximalMovementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maximalJumpHight;
    [SerializeField] private float yAxisRotation;
    [SerializeField] private float mouseSensetivety;


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

        jump = playerInput.MovementInput.Jump;

        move = playerInput.MovementInput.KeyboardMovement;
        rotate = playerInput.MovementInput.mouse;

        jump.started += Jumping;

    }

    private void OnDisable()
    {
        playerInput.Disable();

        jump.started -= Jumping;


    }

    private void FixedUpdate()
    {
        Movement(move.ReadValue<Vector2>() * Time.deltaTime);
        CharachterRotation(rotate.ReadValue<Vector2>() * Time.deltaTime);
        SpeedController();
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

    private void Jumping(InputAction.CallbackContext context)
    {
        if(isGrounded == true)
        {
        }

        if (Physics.Raycast(transform.position, Vector3.down,maximalJumpHight))
        {
            hipsRigedbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            //isGrounded = true;
        }

    }

    private void CharachterRotation(Vector2 rotationVector2)
    {
        yAxisRotation += rotationVector2.x * mouseSensetivety;

        transform.rotation = Quaternion.Euler(0, yAxisRotation, 0);
        //_back._camera.transform.localRotation = Quaternion.Euler(_y, 0, 0);
    }
}
