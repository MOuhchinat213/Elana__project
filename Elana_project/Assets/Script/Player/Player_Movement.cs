using UnityEngine;



public class Player_Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public int last_direction;
    public float jump_speed=6;
    public float RunSpeed=6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr= GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        Horizontal_Move();
    }
    void Jump()
    {
        float input= Input.GetAxisRaw("Horizontal");
        rb.linearVelocityY = jump_speed;
        rb.linearVelocityX=RunSpeed*input;     
        
    }
    
    void Horizontal_Move()
    {
        float input = Input.GetAxis("Horizontal");
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
