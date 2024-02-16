using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ReloadCurrentScene() {
        string scene = SceneManager.GetActiveScene().name;
        LoadSceneWithName(scene);

        Time.timeScale = 1;
    }

    public static void LoadSceneWithName(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
