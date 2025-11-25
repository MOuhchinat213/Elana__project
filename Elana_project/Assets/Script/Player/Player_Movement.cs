using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private Combat_player stats;
    
    [Header("Movement details")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private bool bonusJump = true;
    private float xInput;
    private bool facingRight = true;
    [Header ("Dashing")]
    [SerializeField] private float dashForce = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    private float originalGravity;
    [Header("Collision details")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform GroundCheck;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stats  = GetComponent<Combat_player>();
        originalGravity=rb.gravityScale;
    }

    void Update()
    {
       if(stats.Health<=0)
        {
            HandleDeath();
            return;
        }
        if(isDashing)
            return;

        HandleDash();
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleFlip();
        HandleAnimations();
    }

    private void HandleDeath()
    {
        if(stats.Health<=0)
        {
            anim.SetTrigger("isDead");
        }
    }
    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void HandleDash()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine(Dash());
    }    
    private void HandleAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("xVelocity", rb.linearVelocityX);
        anim.SetFloat("yVelocity", rb.linearVelocityY);
    }

    private void HandleMovement()
    {
        if(!isGrounded)
            anim.SetBool("isGrounded",false);
        rb.linearVelocityX = xInput * moveSpeed;
    }

    private void Jump()
    {
        if (isGrounded || bonusJump)
        {
            bonusJump = false;
            rb.linearVelocityY = jumpForce;
        }
    }


    private void HandleCollision()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, whatIsGround);
        if(isGrounded)
            bonusJump = true;
    }

    private void HandleFlip()
    {
        if (rb.linearVelocityX > 0 && !facingRight)
            Flip();
        else if (rb.linearVelocityX < 0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
    
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        originalGravity = rb.gravityScale;
        Debug.Log("JE DASH TA MERE LA PUTE");
        rb.gravityScale = 0f;
        float direction = transform.localScale.x > 0 ? 1 : -1;
        rb.linearVelocityX = direction * dashForce;
        anim.SetTrigger("isDead");
        yield return new WaitForSeconds(0.2f);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
