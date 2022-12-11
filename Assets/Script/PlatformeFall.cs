using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformeFall : MonoBehaviour
{
    public float Gravity;
    [SerializeField]Rigidbody2D rb; //pour drag n drop le rigidbody de la platefomr pour qu'il soit pris en compte      Merci Thomas
    private Vector3 StartPosition; 

    private void Start()
    {
        StartPosition = transform.position; //sauvegarde la position de départ de la plateforme
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            rb.gravityScale = Gravity; //met la gravité à 1 pour que la plateforme tombe après le contact avec le joueur
        }
        
        if (collision.CompareTag("Stop")) 
        {
            rb.gravityScale = 0f; //remet la gravité à 0 pour que la plateforme arrête de tomber
            rb.velocity = Vector2.zero; //arrête le déplacement en y donner par la masse du joueur
            transform.position = StartPosition; //renvoie la plateform a sa position de départ
        }
    } 
}
