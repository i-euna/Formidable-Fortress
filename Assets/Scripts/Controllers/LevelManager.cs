using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private IntVariable Health;

    [SerializeField]
    private IntVariable AmmoCount;
    public void CheckGameOver() {
        if (Health.Value == 0) {
            HandleLevelFailure();
        }
    }

    void HandleLevelFailure()
    {
        SceneManager.LoadScene("LevelOver", LoadSceneMode.Single);
    }
}
