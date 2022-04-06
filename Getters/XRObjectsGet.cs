using Unity.XR.CoreUtils;
using UnityEngine;

public class XRObjectsGet : MonoBehaviour
{
    public static GameObject Instance;
    public static XROrigin XRPlayer;
    public static ControllerStuff LeftController;
    public static ControllerStuff RightController;
    public static GameObject PlayerMouth;

    private void Awake()
    {
        Instance = gameObject;
        XRPlayer = GetComponentInChildren<XROrigin>();
        LeftController = XRPlayer.transform.GetChild(0).Find("LeftHand Controller").GetComponent<ControllerStuff>();
        RightController = XRPlayer.transform.GetChild(0).Find("RightHand Controller").GetComponent<ControllerStuff>();
        PlayerMouth = XRPlayer.Camera.transform.Find("Mouth Area").gameObject;
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnApplicationQuit()
    {
        if (!PlayerStats.Died)
            SaveSystem.SaveAll();
    }
}
