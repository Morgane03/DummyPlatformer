using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 movement = Vector2.zero;
    Rigidbody2D rb = null;
    SpriteRenderer Renderer = null;
    public int maxNumberOfJumps = 2;
    int remainingNumberOfJumps = 0;
    public float JumpForce = 9.0f;
    Animator animator = null;
    bool Jump = false;


    public float DashSpeed = 25f;
    public int MaxNumberOfDash = 4;
    private int NumberOfDash = 0;
    public float DashTime;
    bool IsDash = false;
    private Vector2 DashDirection;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingNumberOfJumps = maxNumberOfJumps;
        Renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float WolkLeftRight = Mathf.Abs(rb.velocity.x); //permet de jouer l'animation du joueur lorsqu'il va à gauche

        if (rb == null)
        {
            return;
        }

        if (!IsDash) //Si Is Dash = false
        {
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y); // mouvement
            animator.SetFloat("Wolk", WolkLeftRight);
        }
        else
        {
            rb.velocity = DashDirection * DashSpeed; //Multiplie la velocity du joueur pour dash
            
        }


        if (movement.x != 0 && Time.timeScale != 0)
        {
            Renderer.flipX = movement.x < 0;
            animator.SetFloat("Wolk", WolkLeftRight);
        }

    }

    public void OnDash()
    {

        StartCoroutine(ExecuteDash()); //commencer le Dash      Merci Julien
        NumberOfDash = NumberOfDash + 1; //Ajoute un Dash, si le joueur atteind 3 Dash, il doit attendre 5s
    }
    public void OnJump(InputValue jumpValue)
    {
        Jump = true;
        StartCoroutine(ExecuteJump());

        float pressed = jumpValue.Get<float>();
        if (rb == null || pressed == 0 || remainingNumberOfJumps == 0)
        {
            return;
        }
        remainingNumberOfJumps--;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);

    }

    public void OnMove(InputValue moveValue)
    {
        movement = moveValue.Get<Vector2>();

    }

    public void OnDialogue()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0.8f)
        {
            remainingNumberOfJumps = maxNumberOfJumps;
        }
    }

    IEnumerator ExecuteDash()
    {
        if (NumberOfDash < MaxNumberOfDash)
        {
            IsDash = true;
            animator.SetBool("IsDash", true);
            DashDirection = movement; //sauvegarde du mouvement actuel du joueur pour continuer a dash même en ayant lacher le bouton
            yield return new WaitForSeconds(0.5f); //temps du Dash 
            IsDash = false; //fin du Dash
            animator.SetBool("IsDash", false);
        }

        if (NumberOfDash == MaxNumberOfDash) //Si le joueur a atteind ses 3 dash, il doit attendre 5s avant de dash a nouveau
        {
            yield return new WaitForSeconds(5);
            NumberOfDash = 0;
        }
    }
    IEnumerator ExecuteJump()
    {
        if (Jump)
        {
            animator.SetBool("Jump", true);
            yield return new WaitForSeconds(1);
            Jump = false;
            animator.SetBool("Jump", false);
        }
    }
}
