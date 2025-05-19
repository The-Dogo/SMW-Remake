using UnityEditor.Timeline;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private new Rigidbody2D rigidbody;

    public float movespeed = 8f;

    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2);

    private Animator animator;


    private Vector2 velocity;
    private float inputAxis;

    public Transform grounCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallCheckRadius = 0.1f;

    private bool isGrounded;
   

    public Transform tetoCheck;
    public float tetoCheckRadius = 0.2f;



    public bool jumping { get; private set; }

    public bool running => Mathf.Abs(velocity.x) >= 7f; 

    public bool walking => Mathf.Abs(velocity.x) > 0.25f && Mathf.Abs(velocity.x) < 7f; 
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    private void Awake()
    {

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        HorizontalMovement();


        isGrounded = Physics2D.OverlapCircle(grounCheck.position, groundCheckRadius, groundLayer);


        if (isGrounded)
        {

            groundedMovement();
        }



        ApplyGravity();
    }

    private void HorizontalMovement()
    {

        inputAxis = Input.GetAxis("Horizontal");

        bool hittingWallLeft = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, groundLayer);
        bool hittingWallRight = Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, groundLayer);

        bool againstWall = (inputAxis < 0 && hittingWallLeft) || (inputAxis > 0 && hittingWallRight);

          if (againstWall)
         {
             velocity.x = 0f;
         }
         else
         {
             velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movespeed, movespeed * Time.deltaTime);
         }
        

        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movespeed, movespeed * Time.deltaTime);
        if (velocity.x > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (velocity.x < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else { }
    }

    private void groundedMovement()
    {

        velocity.y = Mathf.Max(velocity.y, 0);
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
        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2);




    }

  

    private void FixedUpdate()
    {

        bool cabecadaCheck = Physics2D.OverlapCircle(tetoCheck.position, tetoCheckRadius, groundLayer);
        if (cabecadaCheck && velocity.y > 0f)
        {
            velocity.y = 0f;
        }

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = 0f;
        }

        rigidbody.linearVelocity = velocity;

    }

    private void LateUpdate()
    {
        animator.SetBool("walking", walking);
        animator.SetBool("running", running);
        animator.SetBool("jumping", jumping);
        animator.SetBool("sliding", sliding);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f ;
                jumping = true; 
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUps"))
        {
            if(transform.DotTest(collision.transform,Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }




}