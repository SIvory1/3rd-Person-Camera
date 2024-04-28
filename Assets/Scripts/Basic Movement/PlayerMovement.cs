using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Animator animator;
    Rigidbody rb;

    [Header("Movement")]
    float moveX;
    float moveY;
    float currentSpeed;
    [SerializeField] float walkSpeed;
    Vector3 moveVector;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float groundDrag;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentSpeed = walkSpeed;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
;
        PlayerAxis();
        AdjustDrag();
   
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

        if (IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }
        else
             animator.SetBool("IsJumping", true);

        if ( Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {    
        MovePlayer();
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.1f, Ground))
        return true;
        else 
            return false;  
    }

    void AdjustDrag()
    {
        // changes the player drag based on if the player is touching the ground or not
        if (IsGrounded())
        {
            rb.drag = groundDrag;
        }
        else
        {
            // the longer the player is in the air the faster they wil fall
            if (rb.drag > 0.3)
            {
                rb.drag -= Time.deltaTime * 3;
            }
        }
    }

    void PlayerAxis()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
    }

    // vecotr3 direction = new vector3 (moveX, 0f, moveY).normalized;
    void MovePlayer()
    {
        moveVector = orientation.forward * moveY + orientation.right * moveX;

        rb.AddForce(10f * walkSpeed * moveVector.normalized, ForceMode.Force);
    }

    [SerializeField] float jumpForce;

    void Jump()
    {
        if (IsGrounded())
        {               
           rb.AddForce(Vector3.up * jumpForce * 100f, ForceMode.Force);
        }
    }

    public void Implused()
    {
        rb.mass = 1;
    }
}
