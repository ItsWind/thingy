using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    private void TryLoad()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            transform.position = SaveSystem.GetPositionVectorFromFloat(data.headPos);
            transform.rotation = Quaternion.Euler(0, data.lookRot, 0);
        }
    }

    void Awake()
    {
        TryLoad();
    }
}
