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
        GameObject ExistingUi = GameObject.FindGameObjectWithTag("PauseUI");
        if (ExistingUi == null)
        {
            GameObject UI = Instantiate(PauseMenuPrefab);
            DontDestroyOnLoad(UI);
        }
        else
        {
            DontDestroyOnLoad(ExistingUi);
        }
    }


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
        PauseMenuPrefab.SetActive(true);
        // arreter le temps
        Time.timeScale = 0.0f;
        // changer le statut du jeu
        gameIsPause = true;
    }

    void Resume()
    {
        PauseMenuPrefab.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPause = false;
    }
}
