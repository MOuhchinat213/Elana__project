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
    [SerializeField] private int  bonusJump = 2; //double jump start
    private float xInput;
    private bool facingRight = true;
    [Header ("Dashing")]
    [SerializeField] private float dashForce = 35f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    private float originalGravity;
    [Header("Collision details")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private TrailRenderer tr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stats  = GetComponent<Combat_player>();
        tr=GetComponentInChildren<TrailRenderer>();
        originalGravity=1;
    }

    void Update()
    {
        if (!isGrounded && bonusJump!=0)
            bonusJump = 1;

        if(stats.isAttacking)
            return;
        if(stats.Health<=0)
        {
            HandleDeath();
            return;
        }
        if(isDashing)
            return;
        if(stats.isHealing)
            return;

        HandleDash();
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleFlip();
        HandleAnimations();
    }

    #region Animation

        private void HandleDeath()
        {
            if(stats.Health<=0)
            {
                anim.SetTrigger("isDead");
            }
        }
        private void HandleAnimations()
        {
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("xVelocity", rb.linearVelocityX);
            anim.SetFloat("yVelocity", rb.linearVelocityY);
        }
    #endregion



    #region Movement gameplay
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

    private void HandleMovement()
    {
        if(isDashing) 
            return;
        if(!isGrounded)
            anim.SetBool("isGrounded",false);
        rb.linearVelocityX = xInput * moveSpeed;
    }

    private void Jump()
    {
        if (isGrounded || bonusJump>0)
        {
            bonusJump -=1;
            rb.linearVelocityY = jumpForce;
        }
    }


    private void HandleCollision()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, whatIsGround);
        if(isGrounded)
            bonusJump = 2;
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
        rb.gravityScale = 0f;
        anim.SetTrigger("isDashing");
        float dashDirection = facingRight ? 1f : -1f;
        float dashTime = 0f;
        tr.emitting=true;
        while(dashTime < dashDuration)
        {
            rb.linearVelocity = new Vector2(dashDirection * dashForce, 0f);
            dashTime += Time.deltaTime;
            yield return null; 
        }
        rb.gravityScale = originalGravity;
        isDashing = false;
        tr.emitting=false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    #endregion
}
