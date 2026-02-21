using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player components")]
    public Animator anim;
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    
    [Header("Movement")]
    
    [SerializeField] private float _speed=2f;
    public float jumpForce;
    public float jumpCut = 0.5f;
    [SerializeField]private int faceDirection=1;
    
    [Header("Input Tracking")]
    public Vector2 moveInput;
    private bool jumpPresed;
    private bool jumpReleased;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        
        Flip();
        HandleAnimation();
    }


    void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement(); 
        HandleJump();
        
    }

    #region Handling Region

        private void HandleMovement()
        {
            float targetedSpeed = moveInput.x * _speed;
            rb.linearVelocity = new Vector2(targetedSpeed, rb.linearVelocity.y);
    
        }
    
        private void HandleJump()
        {
            if (jumpPresed && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
                jumpPresed = false;
                jumpReleased = false;
            }
    
            if (jumpReleased)
            {
                if (rb.linearVelocity.y > 0)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocity.y*jumpCut);
                } 
                jumpReleased = false;
            }
        }

        void HandleAnimation()
        {
            anim.SetBool("isIdle",Mathf.Abs(moveInput.x)<0.1f && isGrounded);
            anim.SetBool("isWalking",Mathf.Abs(moveInput.x)>0.1f && isGrounded);
        }

        void CheckGrounded()
        {
            isGrounded=Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
    
        void Flip()
        {
            if (moveInput.x > 0.1f)
            {
                faceDirection = 1;
            }
    
            else if (moveInput.x < -0.1f)
            {
                faceDirection = -1;
            }
            transform.localScale = new Vector3(faceDirection, 1f, 1f);
        }


    #endregion



    #region InputCheck

        public void OnMove(InputValue inputValue)
        {
            moveInput=inputValue.Get<Vector2>();
        }
    
        public void OnJump(InputValue inputValue)
        {
            if (inputValue.isPressed)
            {
                jumpPresed = true;
                jumpReleased = false;
            }
            else
            {
                jumpReleased = true;
            }
        }

    #endregion

    
}
