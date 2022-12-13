using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanteJump : MonoBehaviour
{
    Animator animator = null;
    bool Jumping = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    

    public PlayerMovement playerMouvement; //appel le script PlayerMouvement

   public void OnTriggerEnter2D (Collider2D collision)
    {

        if (collision.CompareTag("Player"))  //Seul le player peut l'utiliser
        {
            StartCoroutine(JumpPlante()); //Commence la coroutine
        }
    }

   IEnumerator JumpPlante()
    {
        Jumping = true;
        animator.SetBool("Jumping", true);
        playerMouvement.JumpForce = 13;
        yield return new WaitForSeconds(2);
        playerMouvement.JumpForce = 8;
        Jumping = false;
        animator.SetBool("Jumping", false);
    }
}
