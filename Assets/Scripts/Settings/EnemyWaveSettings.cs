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
            { EnemyType.SLOW_SPEED_WALKER, 5 }
        };

        LevelRequirement[Levels.Level1] = new Dictionary<EnemyType, int>
        {
            { EnemyType.SLOW_SPEED_WALKER, 5 },
            { EnemyType.MEDIUM_SPEED_WALKER, 3 }
        };

        LevelRequirement[Levels.Level2] = new Dictionary<EnemyType, int>
        {
            { EnemyType.MEDIUM_SPEED_WALKER, 6 },
            { EnemyType.HIGH_SPEED_WALKER, 3 },
            { EnemyType.SLOW_AIR, 3 }
        };
    }
}
