using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private new Rigidbody2D rigidbody;

    public float movespeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight)/(maxJumpTime/2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f , 2);

   

    private Vector2 velocity; 

    private float inputAxis;

    public Transform grounCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;

  
    public bool jumping { get; private set; }

    private void Awake()
    {

        rigidbody = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        inputAxis = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(grounCheck.position, groundCheckRadius, groundLayer);

        HorizontalMovement();
        

        if (isGrounded) {

            groundedMovement();
        }

        

         ApplyGravity();
    }

    private void HorizontalMovement()
    {

        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movespeed, movespeed* Time.deltaTime);

    }

    private void groundedMovement() 
    {
        jumping = velocity.y > 0f;


        if (Input.GetButtonDown("Jump")) 
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f ; 
        velocity.y += gravity * multiplier * Time.deltaTime;

        



    }

    private void FixedUpdate()
    {
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = 0f;
        }

        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        rigidbody.MovePosition(position);

    }

}

