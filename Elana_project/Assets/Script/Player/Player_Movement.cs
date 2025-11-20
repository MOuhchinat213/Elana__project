using UnityEngine;



public class Player_Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public int last_direction;

    public float RunSpeed=6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr= GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Horizontal_Move();
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
