using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanteJump : MonoBehaviour
{
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
        playerMouvement.JumpForce = 13;
        yield return new WaitForSeconds(2);
        playerMouvement.JumpForce = 8;
    }
}
