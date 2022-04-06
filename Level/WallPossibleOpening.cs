using Unity.XR.CoreUtils;
using UnityEngine;

public class WallPossibleOpening : MonoBehaviour
{
    public GameObject DoorObj = null;

    public XROrigin XRPlayer { get; private set; }
    public float MinPlayerDistance = 16.0f;

    public bool AlwaysClosed = false;
    public bool AlwaysOpen = false;

    public MeshFilter MeshFilter { get; protected set; }
    public Collider Collider { get; protected set; }
    private Mesh meshUsed;

    public bool IsOpened { get; set; }

    private void RandomOpen(double chance)
    {
        if (AlwaysClosed) { Close(); return; }
        if (AlwaysOpen) { Open(); return; }

        double randNum = RandomNumGen.Instance.NextDouble();

        if (randNum <= chance)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    private void Open()
    {
        if (!IsOpened)
        {
            MeshFilter.mesh = null;
            Collider.enabled = false;
        }
        IsOpened = true;

        if (DoorObj != null)
        {
            DoorObj.SetActive(true);
        }
    }

    private void Close()
    {
        if (IsOpened)
        {
            MeshFilter.mesh = meshUsed;
            Collider.enabled = true;
        }
        IsOpened = false;

        if (DoorObj != null)
        {
            DoorObj.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        MeshFilter = GetComponent<MeshFilter>();
        meshUsed = MeshFilter.mesh;
        Collider = GetComponent<Collider>();
        XRPlayer = GameObject.Find("/XR Objects").GetComponentInChildren<XROrigin>();

        RandomOpen(0.75);
    }

    private float secondsToWait = 1.0f;
    private float counter = 0.0f;
    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= secondsToWait && !GetComponent<MeshRenderer>().isVisible && Vector3.Distance(transform.position, XRPlayer.Camera.transform.position) >= MinPlayerDistance)
        {
            counter = 0.0f;
            RandomOpen(0.75);
        }
    }
}
