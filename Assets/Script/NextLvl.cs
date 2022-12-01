using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvl : MonoBehaviour
{
    public string SceneName;

    // Start is called before the first frame update
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ChangeScene();
    }
}
