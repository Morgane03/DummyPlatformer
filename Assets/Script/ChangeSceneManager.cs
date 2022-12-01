using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{

    private static ChangeSceneManager instance = null;
    public static ChangeSceneManager Instance => instance;

    public TMPro.TMP_Text text;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(text != null)
            text.text = SceneManager.GetActiveScene().buildIndex + "/" + (SceneManager.sceneCountInBuildSettings - 1);
    }

    public void ChangeScene(int numberSkip)
    {
        SceneManager.LoadScene(Mathf.Clamp((SceneManager.GetActiveScene().buildIndex + numberSkip), 0, SceneManager.sceneCountInBuildSettings - 1));
    }
}
