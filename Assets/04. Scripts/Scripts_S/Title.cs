using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "Play";

    public void ClickStart()
    {
        Debug.Log("����");
        SceneManager.LoadScene(sceneName);
    }

    public void ClickOption()
    {
        Debug.Log("�ɼ�");
    }

    public void ClickExit()
    {
        Debug.Log("����");
        Application.Quit();
    }
}
