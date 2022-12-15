using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    Animator animator = null;
    bool Pressed = false;
    public float Wait;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ChangeScene(string _sceneName)
    {
        StartCoroutine(WaitAnimation(_sceneName));
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ReloadLvl(string _sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(_sceneName);
    }
    IEnumerator WaitAnimation(string _sceneName)
    {
        Pressed = true;
        animator.SetBool("Pressed", true);
        yield return new WaitForSeconds(Wait);
        SceneManager.LoadScene(_sceneName);
        Pressed = false;
        animator.SetBool("Pressed", false);
    }
}
