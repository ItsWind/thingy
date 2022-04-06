using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackpackData
{
    public ItemData[] GameObjectsInBackpack;

    public BackpackData()
    {
        List<ItemData> list = new List<ItemData>();
        foreach (Transform t in Backpack.Instance.BackpackObjs)
        {
            GameObject obj = t.gameObject;
            list.Add(new ItemData(obj));
        }
        GameObjectsInBackpack = list.ToArray();
    }
}
