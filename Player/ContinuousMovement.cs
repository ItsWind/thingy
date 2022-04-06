using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class ContinuousMovement : MonoBehaviour
{
    public XRNode InputSource;
    public XROrigin rig;
    public float speed = 1.0f;
    public float gravity = -9.81f;
    public LayerMask groundLayer;

    private float fallingSpeed;
    private CharacterController character;
    private Vector2 inputAxis;
    private InputDevice device;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(InputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        CharFollowHeadset();

        Quaternion rigYaw = Quaternion.Euler(0, rig.Camera.gameObject.transform.eulerAngles.y, 0);
        Vector3 direction = rigYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * Time.fixedDeltaTime * speed);

        //bool isGrounded = CheckIfGrounded();
        if (character.isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    private void CharFollowHeadset()
    {
        character.height = rig.CameraInOriginSpaceHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.gameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth, capsuleCenter.z);
    }

    /*RaycastHit hitInfo;
    public bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;

        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        if (hasHit)
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            if (leftGrip.ObjectIsHeld() != null && hitObject.Equals(leftGrip.ObjectIsHeld()))
            {
                return false;
            }
            if (rightGrip.ObjectIsHeld() != null && hitObject.Equals(rightGrip.ObjectIsHeld()))
            {
                return false;
            }
        }

        return hasHit;
    }*/
}
