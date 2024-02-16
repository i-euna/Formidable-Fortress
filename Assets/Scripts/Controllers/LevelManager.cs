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
    private Levels CurrentLevel;
    [SerializeField]
    private Levels NextLevel;

    private void Awake()
    {
        CurrentLevelSettings.CurrentLevel = CurrentLevel;
    }

    public static Levels GetCurrentLevel() {
        return CurrentLevelSettings.CurrentLevel;
    }
    public void CheckGameOver() {

        //if all enemies are killed/destroyed
        //level success
        //Debug.Log("TotalKilledEnemy " + TotalKilledEnemy.Value);
        if (TotalSpawnedEnemy.Value == TotalKilledEnemy.Value 
            && Health.Value != 0) {
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
        Debug.Log(CurrentLevel + " - Successful");
        Debug.Log("Loading Next Level " + NextLevel);
        SceneController.LoadSceneWithName(NextLevel.ToString());
    }
}
