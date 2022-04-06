using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BackpackPutBack : MonoBehaviour
{
    public BackpackGrab BackpackGrab;

    public bool BackpackGrabInCol { get; private set; } = false;

    private XRDirectInteractor interactorSaved = null;
    private void Update()
    {
        if (interactorSaved != null && BackpackGrabInCol
            && interactorSaved.transform.parent.GetComponent<ControllerStuff>().GripFloat == 0.0f)
                BackpackGrab.DisableBackpack();
        interactorSaved = BackpackGrab.BackpackGrabbedBy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(BackpackGrab.GetComponent<Collider>()))
            BackpackGrabInCol = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.Equals(BackpackGrab.GetComponent<Collider>()))
            BackpackGrabInCol = false;
    }
}
