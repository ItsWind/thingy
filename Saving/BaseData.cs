using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseData
{
    public ItemData[] GameObjectsInBase;

    public BaseData()
    {
        List<ItemData> list = new List<ItemData>();
        foreach (Transform t in AllBaseObjectsGet.Instance.transform)
        {
            GameObject obj = t.gameObject;
            list.Add(new ItemData(obj));
        }
        GameObjectsInBase = list.ToArray();
    }
}
