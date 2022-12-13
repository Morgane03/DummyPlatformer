using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeadTeller : MonoBehaviour
{
    public int number;
    public TMPro.TMP_Text text;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Die")) //permet de savoir si la variable Die à déjà été utilisé et donc a déjà un nombre de mort assigné lors d'une partie d'avant
        {
            number = PlayerPrefs.GetInt("Die"); //met number sui affiche le nombre de mort du joueur au nombre de mort déjà sauvegarder
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