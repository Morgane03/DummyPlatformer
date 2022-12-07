using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleport : MonoBehaviour
{
    public string playerTag = "Player";
    public Teleport otherTeleport;
    [HideInInspector]
    public bool canTeleport, isOccupied;


    public UnityEvent teleportEvent;
    private void Start()
    {
        canTeleport = true;
    }

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && canTeleport && !otherTeleport.isOccupied)
        {
            canTeleport = false;
            otherTeleport.canTeleport = false;
            collision.transform.position = otherTeleport.transform.position;
            teleportEvent?.Invoke();
        }
        if (collision.CompareTag(playerTag))
        {
            canTeleport = true;
            //otherTeleport.canTeleport = true;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(otherTeleport == this)
        {
            otherTeleport = null;
            Debug.LogError("Other Teleport needs to be another than this one");
        }
    }
#endif
}
