using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    public Vector2 moveInput;
    [SerializeField] private float _speed=2f;
    [SerializeField]private int faceDirection=1;


    private void Update()
    {
        Flip();
    }


    void FixedUpdate()
    {
        float targetedSpeed = moveInput.x * _speed;
        rb.linearVelocity = new Vector2(targetedSpeed, rb.linearVelocity.y);
        
    }

    
    public void OnMove(InputValue inputValue)
    {
        moveInput=inputValue.Get<Vector2>();
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
}
