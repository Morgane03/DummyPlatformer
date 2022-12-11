using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeadTeller : MonoBehaviour
{
    public int number;
    

    public TMPro.TMP_Text text;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kill"))
        {
           if (number >= 0)
            {
                number++;
                text.text = number.ToString();
            }
        }
    }
}
//playerpref.get string --> garder en mémoire 