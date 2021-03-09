using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private fields
    // The InputMaster class
    private InputMaster inputMaster;

    // The rigidbody component
    private Rigidbody _rigidbody;

    // Stores whether or not the player is grounded
    private bool isGrounded;

    // Sideways movement
    private float movementInput = 0f;

    // Public fields
    // The speed at which the player moves
    public float speed;

    // How much force the player has with a jump
    public float jumpForce;

    // Reference to the GroundCheck object
    public Transform groundCheck;

    // The radius of the sphere to check if the player is grounded
    public float groundDistance;

    // Control which objects count as "ground"
    public LayerMask groundMask;

    // Awake calls before start
    void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Player.Jump.performed += context => Jump();
    }

    // OnEnable is called whenever thee GameObject has been enabled
    void OnEnable()
    {
        inputMaster.Enable();    
    }

    // OnDisable is called whenever the GameObject has been disabled
    void OnDisable()
    {
        inputMaster.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Check if the player is grounded. The player is not allowed to change direction mid-air
        if (isGrounded)
        {
            // Get the movement input from the inputmaster. A = -1, D = 1
            movementInput = inputMaster.Player.Movement.ReadValue<float>();
        }

        // Translate the player in the correct direction
        transform.Translate(new Vector3(movementInput, 0, 0) * speed * Time.deltaTime);
    }

    // Update is called in sync with the physics engine
    void FixedUpdate()
    {
        // Increase the gravitational strength
        _rigidbody.AddForce(Physics.gravity * _rigidbody.mass);
    }

    void Jump()
    {
        // Check if the player is grounded. The player is not allowed to jump mid-air
        if (isGrounded)
        {
            // Add some upward force to the rigidbody
            _rigidbody.AddForce(transform.up * jumpForce);
        }
    }
}
