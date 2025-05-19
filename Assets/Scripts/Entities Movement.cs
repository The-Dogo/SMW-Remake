using Unity.VisualScripting;
using UnityEngine;


public class EntitiesMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;
   
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    private bool grounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallCheckRadius = 0.1f;

    private Animator animator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled=false;
    }

    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
       
    }

    private void OnDisable()
    {
        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;





        bool hittingWallLeft = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, groundLayer);
        bool hittingWallRight = Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, groundLayer);

     

        if (hittingWallLeft && grounded)
        {
            velocity.x = 1;
            direction.x = -direction.x;

            transform.eulerAngles = new Vector3(0f, 180f, 0f);

            

        }
        else if (hittingWallRight && grounded) {

            velocity.x = -1;
            transform.eulerAngles = Vector3.zero;
            
        }

       
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        if (grounded)
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }

    private void LateUpdate()
    {
        animator.SetBool("ground", grounded);
        
    }


}
