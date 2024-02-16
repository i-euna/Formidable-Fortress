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
    private IntVariable TotalSpawnedEnemy, TotalKilledEnemy;

    [SerializeField]
    private GameObject LevelFailurePanel;

    [SerializeField]
    public static Levels CurrentLevel;
    [SerializeField]
    private Levels NextLevel;
    public void CheckGameOver() {

        //if all enemies are killed/destroyed
        //level success
        //Debug.Log("TotalKilledEnemy " + TotalKilledEnemy.Value);
        if (TotalSpawnedEnemy.Value == TotalKilledEnemy.Value 
            && Health.Value != 0) {
            Debug.Log("Level Successful");
            HandleSuccess();
        }

        if (Health.Value == 0 || AmmoCount.Value <= 0) {
            HandleLevelFailure();
        }
    }

    void HandleLevelFailure()
    {
        Time.timeScale = 0;
        LevelFailurePanel.SetActive(true);
    }

    void HandleSuccess() {
        SceneController.LoadSceneWithName(NextLevel.ToString());
    }
}
