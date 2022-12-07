using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //pour utiliser les text dans unity

public class DeadTeller : MonoBehaviour
{
    public DeathZone deathZone;
    public string Dead;
    public Text text;
    void Start()
    {
        if (deathZone.DeadWait == true) //appel le bool DeadWait pour ajouter une mort au conteur
        {
            Dead = Dead + 1; 
            text.text = Dead;
        }
    }
}
