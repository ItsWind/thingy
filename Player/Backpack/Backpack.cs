using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public static Backpack Instance { get; private set; }
    // added item transform, original parent transform
    public List<Transform> BackpackObjs { get; private set; } = new List<Transform>();

    public void TryLoad()
    {
        BackpackData data = SaveSystem.LoadBackpack();
        if (data != null)
        {
            foreach (ItemData itemData in data.GameObjectsInBackpack)
            {
                Object o = Resources.Load("Prefabs/Grabbables/" + itemData.name);
                if (o != null)
                {
                    GameObject prefabObj = Instantiate(o) as GameObject;
                    GameObject obj = prefabObj.transform.GetChild(0).gameObject;

                    BackpackObjs.Add(obj.transform);
                    obj.transform.SetParent(transform, false);
                    Destroy(prefabObj);

                    Rigidbody rb = obj.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.constraints = RigidbodyConstraints.FreezeAll;

                    obj.transform.localPosition = SaveSystem.GetPositionVectorFromFloat(itemData.position);
                    obj.transform.localRotation = Quaternion.Euler(SaveSystem.GetPositionVectorFromFloat(itemData.rotation));
                }
            }
        }
    }

    private void Awake()
    {
        TryLoad();
        Instance = this;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Grabbable grabObj = other.GetComponent<Grabbable>();
        if (!BackpackObjs.Contains(other.transform) && grabObj != null && grabObj.InteractorHolding != null)
        {
            BackpackObjs.Add(other.transform);
            other.transform.SetParent(transform, true);

            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Grabbable grabObj = other.GetComponent<Grabbable>();
        if (BackpackObjs.Contains(other.transform))
        {
            other.transform.SetParent(grabObj.InteractorHolding != null ?
                XRObjectsGet.Instance.GetComponentInChildren<XROrigin>().transform : SceneLoadManager.CurrentLevelAllObjectsTransform,
            true);

            BackpackObjs.Remove(other.transform);

            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
