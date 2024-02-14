using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPregressionController : MonoBehaviour
{
    [SerializeField]
    private int TotalLevels = 5;

    private int CurrentLevel = 1;

    [SerializeField]
    private GameEventWithStr EnemyDeathEvent;

    private Dictionary<EnemyType, int> KillCount;
    private void Start()
    {
        KillCount = new Dictionary<EnemyType, int>();
        EnemyDeathEvent.Event.AddListener(CheckLevelSuccess);
    }

    public void CheckLevelSuccess(string enemyType)
    {
        Dictionary<EnemyType, int> req = LevelProgressionSettings.LevelRequirement[CurrentLevel];
        //update dead enemy count
        int count = 0;
        EnemyType type = ParseEnum.Parse<EnemyType>(enemyType);
        if (KillCount.TryGetValue(type, out count))
        {
            count++;
            KillCount[type] = count;
        }
        else {
            count = 1;
            KillCount.Add(type, count);
        }
        //check if its added properly, remove later
        foreach (KeyValuePair<EnemyType, int> r in KillCount)
        {
            Debug.Log("KillCount Test " + r.Key + " " + r.Value);
            
        }

        bool gameOver = true;
        foreach (KeyValuePair<EnemyType, int> r in req)
        {
            Debug.Log("Game Over Test " + r.Key + " " + r.Value);
            if (KillCount[r.Key] != r.Value) {
                gameOver = false;
                break;
            }
        }
        if (gameOver)
            HandleSuccess();
    }

    void HandleSuccess() {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
