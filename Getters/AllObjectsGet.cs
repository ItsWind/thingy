using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllObjectsGet : MonoBehaviour
{
    private void OnEnable()
    {
        SceneLoadManager.CurrentLevelAllObjectsTransform = transform;
    }
}
