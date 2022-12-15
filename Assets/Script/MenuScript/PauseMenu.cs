using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Time.TimeScale tiré d'un livre sur le code en C#

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
        
        // activer le menu pause
        PauseMenuUI.SetActive(true);
        // arreter le temps
        Time.timeScale = 0.0f;
        // changer le statut du jeu
        gameIsPause = true;
    }

    void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPause = false;
    }
}
