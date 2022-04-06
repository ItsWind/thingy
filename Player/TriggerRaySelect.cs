using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerRaySelect : MonoBehaviour
{
    public XRInteractionManager InteractionManager;
    public ControllerStuff ControllerStuff;
    public XRDirectInteractor Interactor;

    public Sprite FocusedSprite;
    public Sprite TriggeredSprite;

    public SpriteRenderer SpriteRenderer { get; private set; }

    public List<Grabbable> FocusedGrabbables { get; private set; } = new List<Grabbable>();
    public Grabbable ClosestInFocus { get; private set; } = null;
    public Grabbable TriggeredGrabbable { get; private set; } = null;
    //public IXRSelectInteractable InteractableHeld { get; private set; } = null;

    private void setTriggeredGrabbable(Grabbable grabObj)
    {
        ClosestInFocus.SetSprite(null);
        ClosestInFocus = null;
        TriggeredGrabbable = grabObj;
    }

    private float counterGetClosestGrabbable = 0.0f;
    private void setClosestGrabbable()
    {
        counterGetClosestGrabbable += Time.deltaTime;
        if (counterGetClosestGrabbable >= 0.1f)
        {
            counterGetClosestGrabbable = 0.0f;
            if (TriggeredGrabbable == null && FocusedGrabbables.Count > 0)
            {
                float minDist = 100.0f;
                Grabbable minGrabble = FocusedGrabbables[0];
                foreach (Grabbable obj in FocusedGrabbables)
                {
                    if (obj != null)
                    {
                        float dist = Vector3.Distance(obj.transform.position, Interactor.transform.position);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            minGrabble = obj;
                        }
                    }
                    else
                    {
                        FocusedGrabbables.Remove(obj);
                    }
                }
                if (ClosestInFocus != null && !minGrabble.Equals(ClosestInFocus))
                    ClosestInFocus.SetSprite(null);
                ClosestInFocus = minGrabble;
                ClosestInFocus.SetSprite(FocusedSprite);
            }
            else
            {
                if (ClosestInFocus != null) { ClosestInFocus.SetSprite(null); }
                ClosestInFocus = null;
            }
        }
    }

    private void Update()
    {
        if (ControllerStuff.ObjectHeld != null && (ClosestInFocus != null || TriggeredGrabbable != null))
        {
            ClosestInFocus = null;
            TriggeredGrabbable = null;
        }

        if (TriggeredGrabbable == null && ControllerStuff.ObjectHeld == null)
        {
            setClosestGrabbable();
        }

        if (ClosestInFocus != null)
        {
            if (ControllerStuff.TriggerFloat >= 0.25f)
            {
                setTriggeredGrabbable(ClosestInFocus);
            }
        }

        if (TriggeredGrabbable != null)
        {
            TriggeredGrabbable.SetSprite(TriggeredSprite);
            if (ControllerStuff.GripFloat >= 0.25f)
            {
                IXRSelectInteractable thing = TriggeredGrabbable.GetComponent<XRGrabInteractable>();
                Interactor.StartManualInteraction(thing);
                //InteractableHeld = thing;

                TriggeredGrabbable.SetSprite(null);
                TriggeredGrabbable = null;
            }
            else if (ControllerStuff.TriggerFloat < 0.25f)
            {
                TriggeredGrabbable.SetSprite(null);
                TriggeredGrabbable = null;
            }
        }

        /*if (InteractableHeld != null)
        {
            if (!ControllerStuff.ForceHolding && ControllerStuff.GripFloat < 0.25f)
            {
                Interactor.EndManualInteraction();
                InteractableHeld = null;
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        //Interactor.StartManualInteraction
        Grabbable grabObj = other.GetComponent<Grabbable>();
        if (grabObj != null && !FocusedGrabbables.Contains(grabObj))
        {
            FocusedGrabbables.Add(grabObj);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Grabbable grabObj = other.GetComponent<Grabbable>();
        if (FocusedGrabbables.Contains(grabObj))
        {
            FocusedGrabbables.Remove(grabObj);
        }
    }
}
