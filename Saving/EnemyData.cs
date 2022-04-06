using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string name;
    public float[] position;
    public float[] rotation;
    public float[] scale;

    public EnemyData(Enemy ene)
    {
        name = ene.name;
        position = SaveSystem.GetPositionFloatArray(ene.transform.localPosition);
        rotation = SaveSystem.GetPositionFloatArray(ene.transform.localRotation.eulerAngles);
        scale = SaveSystem.GetPositionFloatArray(ene.transform.localScale);
    }
}
