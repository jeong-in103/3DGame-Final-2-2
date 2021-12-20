using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public string SceneName = "Gameover";
    public void GameOver()
    {
        SceneManager.LoadScene(SceneName);
    }
}
