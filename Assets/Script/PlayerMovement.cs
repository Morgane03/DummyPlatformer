using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.0f;
    private Vector2 movement = Vector2.zero;
    Rigidbody2D rb = null;
    SpriteRenderer Renderer = null;
    public int maxNumberOfJumps = 2;
    int remainingNumberOfJumps = 0;
    public float JumpForce = 9.0f;
    Animator animator = null;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingNumberOfJumps = maxNumberOfJumps;
        Renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (rb == null)
        {
            return;
        }

        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
        animator.SetBool("IsWalking", movement.x != 0);

        if (movement.x != 0)
        {
            Renderer.flipX = movement.x < 0;
        }

     

    }
    public void OnJump(InputValue jumpValue)
    {
        float pressed = jumpValue.Get<float>();
        if (rb == null || pressed == 0 || remainingNumberOfJumps == 0)
        {
            return;
        }
        remainingNumberOfJumps--;
        animator.SetTrigger("AsJump");
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
       
    }

    public void OnMove(InputValue moveValue)
    {
        movement = moveValue.Get<Vector2>();

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0.8f)
        {
            remainingNumberOfJumps = maxNumberOfJumps;
        }
    }

}
