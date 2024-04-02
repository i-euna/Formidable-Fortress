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
            { EnemyType.SLOW_WALKER, 5 }
        };

        LevelRequirement[Levels.Level1] = new Dictionary<EnemyType, int>
        {
            { EnemyType.SLOW_WALKER, 5 },
            { EnemyType.MEDIUM_WALKER, 3 }
        };

        LevelRequirement[Levels.Level2] = new Dictionary<EnemyType, int>
        {
            { EnemyType.MEDIUM_WALKER, 6 },
            { EnemyType.FAST_WALKER, 3 },
            { EnemyType.SLOW_AIR, 3 }
        };
        LevelRequirement[Levels.Level3E] = new Dictionary<EnemyType, int>
        {
            { EnemyType.SLOW_WALKER, 3 },
            { EnemyType.MEDIUM_WALKER, 5 },
            { EnemyType.FAST_WALKER, 3 },
            { EnemyType.MEDIUM_AIR, 4 }
        };
        LevelRequirement[Levels.Level3H] = new Dictionary<EnemyType, int>
        {
            { EnemyType.MEDIUM_WALKER, 3 },
            { EnemyType.FAST_WALKER, 8 },
            { EnemyType.FAST_AIR, 4 }
        };
    }
}
