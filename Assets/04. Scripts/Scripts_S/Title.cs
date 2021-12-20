using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "Play";

    public void ClickStart()
    {
        Debug.Log("시작");
        SceneManager.LoadScene(sceneName);
    }

    public void ClickOption()
    {
        Debug.Log("옵션");
    }

    public void ClickExit()
    {
        Debug.Log("종료");
        Application.Quit();
    }
}
