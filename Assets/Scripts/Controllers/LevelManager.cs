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

    [SerializeField]
    private IntVariable MaxSpawnedEnemy, TotalKilledEnemy;
    public void CheckGameOver() {

        //if all enemies are killed/destroyed
        //level success
        //Debug.Log("TotalKilledEnemy " + TotalKilledEnemy.Value);
        if (MaxSpawnedEnemy.Value == TotalKilledEnemy.Value 
            && Health.Value != 0) {
            Debug.Log("Level Successful");
        }

        if (Health.Value == 0 || AmmoCount.Value <= 0) {
            HandleLevelFailure();
        }
    }

    void HandleLevelFailure()
    {
        SceneManager.LoadScene("LevelOver", LoadSceneMode.Single);
    }
}
