using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPointsGet : MonoBehaviour
{
    private List<Transform> getAllPlayerSpawnPoints()
    {
        List<Transform> list = new List<Transform>();
        foreach (Transform child in transform)
        {
            list.Add(child);
        }
        return list;
    }

    void OnEnable()
    {
        SceneLoadManager.CurrentLevelPlayerSpawnPointTransforms = getAllPlayerSpawnPoints();
    }
}
