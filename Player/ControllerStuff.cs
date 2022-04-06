using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerStuff : MonoBehaviour
{
    public XRNode InputNode;
    private InputDevice Device;

    private Vector3 lastPos = Vector3.zero;
    private Vector3 currentPos = Vector3.zero;

    public ActionBasedController Controller { get; private set; }
    public XRDirectInteractor Interactor { get; private set; }
    public Animator HandAnim { get; private set; }
    public Vector3 Velocity { get; private set; }
    public float GripFloat { get; private set; }
    public float TriggerFloat { get; private set; }
    public bool AxisClicked { get; private set; }
    public XRGrabInteractable ObjectHeld { get; private set; } = null;
    public bool ForceHolding { get; private set; } = false;

    public void ForceHold(IXRSelectInteractable obj)
    {
        ForceHolding = true;
        Interactor.StartManualInteraction(obj);
    }
    public void DropForceHold()
    {
        ForceHolding = false;
        CheckDropForceHold();
    }
    public void CheckDropForceHold()
    {
        if (Interactor.isPerformingManualInteraction && ObjectHeld != null && !ForceHolding && GripFloat < 0.25f)
        {
            Interactor.EndManualInteraction();
            ObjectHeld = null;
        }
    }
    public void ForceHoldIfObjectHeld()
    {
        if (ObjectHeld != null)
            ForceHold((IXRSelectInteractable)ObjectHeld);
    }

    private void Awake()
    {
        Controller = GetComponent<ActionBasedController>();
        Interactor = GetComponentInChildren<XRDirectInteractor>();
        HandAnim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        Interactor.selectEntered.AddListener((SelectEnterEventArgs args) =>
        {
            XRGrabInteractable objSelected = args.interactableObject as XRGrabInteractable;
            ObjectHeld = objSelected;
        });
        Interactor.selectExited.AddListener((SelectExitEventArgs args) =>
        {
            ObjectHeld = null;
        });
    }

    private void Update()
    {
        Device = InputDevices.GetDeviceAtXRNode(InputNode);

        Device.TryGetFeatureValue(CommonUsages.grip, out float gripflt);
        GripFloat = gripflt;

        Device.TryGetFeatureValue(CommonUsages.trigger, out float triggerflt);
        TriggerFloat = triggerflt;

        Device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool axisclk);
        AxisClicked = axisclk;

        // Velocity
        lastPos = currentPos;
        currentPos = Controller.transform.position;

        Velocity = ((currentPos - lastPos)) / Time.deltaTime;

        CheckDropForceHold();
    }
}
