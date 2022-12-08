using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeadTeller : MonoBehaviour
{
    public string Dead;

    public TMPro.TMP_Text text;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kill"))
        {
            text.text = Dead + 1;
        }
    }
}
//playerpref.get string --> garder en mémoire 