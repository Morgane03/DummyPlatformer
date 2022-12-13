using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeadTeller : MonoBehaviour
{
    public int number;
    public TMPro.TMP_Text text;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Die")) //permet de savoir si la variable Die � d�j� �t� utilis� et donc a d�j� un nombre de mort assign� lors d'une partie d'avant
        {
            number = PlayerPrefs.GetInt("Die"); //met number sui affiche le nombre de mort du joueur au nombre de mort d�j� sauvegarder
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kill"))
        {
            number++;
            text.text = number.ToString();
            PlayerPrefs.SetInt("Die", number); //sauvegarde le nombre de mort dans le playerpref
        }
    }
}