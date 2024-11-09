using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class sceneController : MonoBehaviour
{
    private GameObject HelpPaenl;

    private void Start()
    {
        HelpPaenl = GameObject.Find("HelpCanvas");
        if (HelpPaenl)
        {
            ToggleCanvas();
        }
    }

    public void ToggleCanvas()
    {
        HelpPaenl.SetActive(!HelpPaenl.activeSelf);
    }

    public void LoadSceneByIndex()
    {
        SceneManager.LoadScene(1);
    }
}
