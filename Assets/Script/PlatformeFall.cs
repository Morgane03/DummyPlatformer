using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformeFall : MonoBehaviour
{
    Rigidbody2D rb;
    private Transform playerSpawn;
    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.gravityScale = 1;
        }

        if (collision.CompareTag("Kill"))
        {
            collision.transform.position = playerSpawn.position;

        }
    }
    
}
