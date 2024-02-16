using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyWaveSettings
{
    public static readonly Dictionary<int, Dictionary<EnemyType, int>>
        LevelRequirement;

    static EnemyWaveSettings()
    {
        LevelRequirement = new Dictionary<int, Dictionary<EnemyType, int>>();

        LevelRequirement[1] = new Dictionary<EnemyType, int>
        {
            { EnemyType.WALKER, 10 }//,
            //{ EnemyType.FLOATING, 3 }
        };
    }
}
