using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int sceneIndex;
    public EnemyData[] enemies;

    public LevelData(int sceneIndex)
    {
        this.sceneIndex = sceneIndex;

        List<EnemyData> list = new List<EnemyData>();
        
    }
}
