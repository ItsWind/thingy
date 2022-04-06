using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class BackpackGrab : MonoBehaviour
{
    public XROrigin Player = null;
    public GameObject CameraAttachObj = null;
    public GameObject BackpackObj = null;

    public AudioClip SndOpen;
    public AudioClip SndClose;

    public XRDirectInteractor GripInCol { get; protected set; } = null;
    public XRDirectInteractor BackpackGrabbedBy { get; protected set; } = null;
    public bool BackpackIsOut { get; protected set; } = false;

    private AudioSource sndSrc;

    private void enableBackpack()
    {
        sndSrc.PlayOneShot(SndOpen);
        BackpackObj.SetActive(true);
        BackpackIsOut = true;
        transform.SetParent(null);
    }

    private void moveBackpack(Vector3 pos)
    {
        if (!BackpackObj.activeInHierarchy) { enableBackpack(); }
        transform.position = pos;
        Vector3 lookPos = Player.Camera.transform.position - transform.position;
        BackpackObj.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    public void DisableBackpack()
    {
        sndSrc.PlayOneShot(SndClose);
        BackpackObj.SetActive(false);
        BackpackIsOut = false;
        transform.SetParent(CameraAttachObj.transform, false);
        transform.localPosition = Vector3.zero;
    }

    private void Awake()
    {
        sndSrc = CameraAttachObj.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), Player.GetComponent<CharacterController>());
    }

    private void Update()
    {
        if (GripInCol != null)
        {
            //BackpackGrabbedBy = null;
            ControllerStuff conStuff = GripInCol.transform.parent.GetComponent<ControllerStuff>();
            if (conStuff.GripFloat > 0.0f && conStuff.ObjectHeld == null && !conStuff.Interactor.hasHover)
            {
                BackpackGrabbedBy = GripInCol;
                BackpackIsOut = true;
            }
            else
            {
                BackpackGrabbedBy = null;
            }
        }

        if (BackpackIsOut)
        {
            if (BackpackGrabbedBy != null)
            {
                moveBackpack(BackpackGrabbedBy.transform.position);
            }
            else
            {
                // if backpack too far
                if (Vector3.Distance(BackpackObj.transform.position, CameraAttachObj.transform.position) > 2.0f)
                {
                    DisableBackpack();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        XRDirectInteractor gripObj = other.GetComponent<XRDirectInteractor>();
        if (gripObj != null && GripInCol == null)
        {
            GripInCol = gripObj;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        XRDirectInteractor gripObj = other.GetComponent<XRDirectInteractor>();
        if (gripObj != null && gripObj.Equals(GripInCol))
        {
            GripInCol = null;
        }
    }
}
