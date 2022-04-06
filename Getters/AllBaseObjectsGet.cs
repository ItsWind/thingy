using UnityEngine;

public class AllBaseObjectsGet : MonoBehaviour
{
    public static GameObject Instance;
    private void TryLoad()
    {
        BaseData data = SaveSystem.LoadBase();
        if (data != null)
        {
            foreach (ItemData itemData in data.GameObjectsInBase)
            {
                Object o = Resources.Load("Prefabs/Grabbables/" + itemData.name);
                if (o != null)
                {
                    GameObject prefabObj = Instantiate(o) as GameObject;
                    GameObject obj = prefabObj.transform.GetChild(0).gameObject;

                    Rigidbody rb = obj.GetComponent<Rigidbody>();

                    obj.transform.SetParent(transform, false);
                    Destroy(prefabObj);

                    rb.isKinematic = true;

                    obj.transform.localPosition = SaveSystem.GetPositionVectorFromFloat(itemData.position);
                    obj.transform.localRotation = Quaternion.Euler(SaveSystem.GetPositionVectorFromFloat(itemData.rotation));

                    rb.isKinematic = false;

                    rb.velocity = SaveSystem.GetPositionVectorFromFloat(itemData.velocity);
                    rb.angularVelocity = SaveSystem.GetPositionVectorFromFloat(itemData.angularVelocity);
                }
            }
        }
    }

    private void Awake()
    {
        TryLoad();
        Instance = gameObject;
    }

    private void Update()
    {
        if (SceneLoadManager.CurrentLevelAllObjectsTransform == null && SceneLoadManager.CurrentSceneIndex == 3)
        {
            SceneLoadManager.CurrentLevelAllObjectsTransform = transform;
        }
    }
}
