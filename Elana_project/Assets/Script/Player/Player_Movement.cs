using System;
using System.Collections;
using UnityEngine;



public class Player_Movement : MonoBehaviour
{
    //COMPONENT
    public Rigidbody2D rb;
    public SpriteRenderer sr;
   
   //MOVEMENT
    public int last_direction;
    public float RunSpeed=6f;
       
    //public Animator animator;
    //DASH
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public bool isDashing = false;

    //JUMPING
    public bool isGrounded; 
    public LayerMask LayerGround;
    public Transform GroundCheck;
    public int maxJumps = 1;
    public int jumpsRemaining;
    public float jump_speed=6;


    void Start()
    {
        //animator= GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr= GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, LayerGround);
        if (isGrounded)
        {
            jumpsRemaining = maxJumps;
        }
        if(Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            
            Jump();
            
        }

            Horizontal_Move();
    }
    void Jump()
    {
            if(!isGrounded)
                jumpsRemaining--;

            float input= Input.GetAxisRaw("Horizontal");
            //animator.SetTrigger("Jump");
            rb.linearVelocityY = jump_speed;
            rb.linearVelocityX=RunSpeed*input;  
            isGrounded=false;  
    }


    void Horizontal_Move()
    {
        float input = Input.GetAxis("Horizontal");
        //animator.SetFloat("speed",Mathf.Abs(input));
        rb.linearVelocityX = RunSpeed*input;
                if (input < 0)
        {

            last_direction = -1;
        }
        else if (input > 0)
        {
            last_direction = 1;

        }
        sr.flipX = (last_direction == -1);
        
    }



}
