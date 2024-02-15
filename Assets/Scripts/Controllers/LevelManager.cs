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

        //if all enemies are killed/destroyed
        //level success

        if (Health.Value == 0 || AmmoCount.Value <= 0) {
            HandleLevelFailure();
        }
    }

    void HandleLevelFailure()
    {
        SceneManager.LoadScene("LevelOver", LoadSceneMode.Single);
    }
}
