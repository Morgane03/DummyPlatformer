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
        Jumping = true; //pour l'animation d'intéraction
        animator.SetBool("Jumping", true);
        playerMouvement.JumpForce = 13; //augmente la force de saut du joueur
        yield return new WaitForSeconds(2); //pendant 2s
        playerMouvement.JumpForce = 8; //retour à sa force de base
        Jumping = false; //fin de l'animation
        animator.SetBool("Jumping", false);
    }
}
