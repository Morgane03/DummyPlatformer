using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Transform playerSpawn;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) // si collision verifier si le tag est player
        {
            // position du playerspawn est maintenant celle lié au checkpont
            playerSpawn.position = transform.position;
            Destroy(gameObject); // eviter que  le joueur retourne en arriere meurt et reapparait à un checkpoint plus ancien car retouché
            // si on met un sprit remplacer gameObject par box collider
        }
    }
}
