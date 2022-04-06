using Unity.XR.CoreUtils;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int sceneIndex;
    public float[] headPos;
    public float lookRot;

    public float health;
    public float sanity;
    public float hunger;
    public int dataScore;

    public PlayerData()
    {
        XROrigin player = XRObjectsGet.Instance.GetComponentInChildren<XROrigin>();

        sceneIndex = SceneLoadManager.CurrentSceneIndex;
        headPos = PauseManager.IsPaused ? SaveSystem.GetPositionFloatArray(PauseManager.LastPlayerLocation) : 
            SaveSystem.GetPositionFloatArray(new Vector3(player.Camera.transform.position.x, player.transform.position.y, player.Camera.transform.position.z));
        lookRot = player.Camera.transform.rotation.eulerAngles.y;

        health = PlayerStats.Health;
        sanity = PlayerStats.Sanity;
        hunger = PlayerStats.Hunger;
        dataScore = PlayerStats.DataScore;
    }
}
