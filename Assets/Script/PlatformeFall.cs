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
        StartPosition = transform.position; //sauvegarde la position de d�part de la plateforme
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            rb.gravityScale = Gravity; //met la gravit� � 1 pour que la plateforme tombe apr�s le contact avec le joueur
        }
        
        if (collision.CompareTag("Stop")) 
        {
            rb.gravityScale = 0f; //remet la gravit� � 0 pour que la plateforme arr�te de tomber
            rb.velocity = Vector2.zero; //arr�te le d�placement en y donner par la masse du joueur
            transform.position = StartPosition; //renvoie la plateform a sa position de d�part
        }
    } 
}
