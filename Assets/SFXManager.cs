using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource EnemyKillSound;

    [SerializeField]
    private GameEventWithStr CastleBreachEvent;

    private void Start()
    {
        CastleBreachEvent.Event.AddListener(PlayEnemyKillSound);
    }

    public void PlayEnemyKillSound(string enemyType)
    {
        EnemyKillSound.Play();
    }
}
