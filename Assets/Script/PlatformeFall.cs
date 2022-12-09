using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformeFall : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    private Vector3 StartPosition;

    private void Start()
    {
        StartPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            rb.gravityScale = 1;
        }
        
        if (collision.CompareTag("Stop"))
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            transform.position = StartPosition;
        }
    }
}
