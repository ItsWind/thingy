using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string name;
    public float[] position;
    public float[] rotation;
    public float[] velocity;
    public float[] angularVelocity;

    public ItemData(GameObject obj)
    {
        name = obj.name;
        int lastIndex = name.LastIndexOf("Hold");
        name = name.Substring(0, lastIndex+4);

        Vector3 pos = obj.transform.localPosition;
        position = SaveSystem.GetPositionFloatArray(pos);

        Vector3 rot = obj.transform.localRotation.eulerAngles;
        rotation = SaveSystem.GetPositionFloatArray(rot);

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        Vector3 velo = rb.velocity;
        velocity = SaveSystem.GetPositionFloatArray(velo);

        Vector3 angVelo = rb.angularVelocity;
        angularVelocity = SaveSystem.GetPositionFloatArray(angVelo);
    }
}
