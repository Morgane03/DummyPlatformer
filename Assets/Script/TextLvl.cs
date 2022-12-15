using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TextLvl : MonoBehaviour
{
    public TMPro.TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.GetSceneByName(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
