using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Créer un jeu en 2D facilement avec Unity (#19 : Menu Pause) TutoUnity2D

    public static bool gameIsPause = false;

    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPause)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }

        
    }
    void Paused()
    {
        // PlayerMovement.instance.enable = false;
        // activer le menu pause
        PauseMenuUI.SetActive(true);
        // arreter le temps
        Time.timeScale = 0.0f;
        // changer le statut du jeu
        gameIsPause = true;
    }

    void Resume()
    {
        // PlayerMovement.instance.enable = true;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPause = false;
    }
}
