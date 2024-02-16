using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyWaveSettings
{
    public static readonly Dictionary<Levels, Dictionary<EnemyType, int>>
        LevelRequirement;

    static EnemyWaveSettings()
    {
        LevelRequirement = new Dictionary<Levels, Dictionary<EnemyType, int>>();

        LevelRequirement[Levels.Level0] = new Dictionary<EnemyType, int>
        {
            { EnemyType.WALKER, 10 }
        };

        LevelRequirement[Levels.Level1] = new Dictionary<EnemyType, int>
        {
            { EnemyType.WALKER, 6 },
            { EnemyType.HIGH_SPEED_WALKER, 4 }
        };
    }
}
