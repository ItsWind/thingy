using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public int LevelIndex;
    public GameObject AllEnemiesObj;

    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

    public void SpawnEnemy(string enemyName, Vector3 pos)
    {
        SpawnEnemy(enemyName, pos, new Vector3(0, 0, 0), new Vector3(1, 1, 1));
    }
    public void SpawnEnemy(string enemyName, Vector3 pos, Vector3 rot, Vector3 scale)
    {
        Object o = Resources.Load("Prefabs/Enemies/" + enemyName);
        if (o != null)
        {
            GameObject prefabObj = Instantiate(o) as GameObject;
            GameObject obj = prefabObj.transform.GetChild(0).gameObject;
            obj.transform.SetParent(AllEnemiesObj.transform, false);
            Destroy(prefabObj);

            obj.transform.localPosition = pos;
            obj.transform.localRotation = Quaternion.Euler(rot);
            obj.transform.localScale = scale;

            Enemies.Add(obj.GetComponent<Enemy>());
        }
    }

    public void RemoveEnemy(Enemy ene)
    {
        Enemies.Remove(ene);
        Destroy(ene.gameObject);
    }
}
