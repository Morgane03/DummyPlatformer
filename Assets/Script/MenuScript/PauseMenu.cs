using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Time.TimeScale tiré d'un livre sur le code en C#

    public static bool gameIsPause = false;

    public GameObject PauseMenuPrefab;
    public GameObject ExistingUi;

    private void Awake()
    {
        DontDestroyOnLoad(ExistingUi);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPause)
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
        ExistingUi.SetActive(true);
        // arreter le temps
        Time.timeScale = 0.0f;
        // changer le statut du jeu
        gameIsPause = true;
    }

    void Resume()
    {
        ExistingUi.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPause = false;
    }
}
