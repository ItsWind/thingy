using Unity.XR.CoreUtils;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public int SceneToLoadIndex;

    private GameObject XRObjects;
    private XROrigin XRPlayer;
    private CharacterController character;

    private void Awake()
    {
        XRObjects = XRObjectsGet.Instance;
        XRPlayer = XRObjects.GetComponentInChildren<XROrigin>();
        character = XRPlayer.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (character.Equals(other))
        {
            SceneLoadManager.Instance.LoadScene(SceneToLoadIndex);
        }
    }
}
