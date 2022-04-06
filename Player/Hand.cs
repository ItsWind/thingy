using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour
{
    public ControllerStuff ConStuff;

    public Animator Animator { get; private set; }

    public const string ANIMATOR_GRIP_PARAM = "Grip";
    public const string ANIMATOR_TRIGGER_PARAM = "Trigger";
    public string LastHeldItemParam = "";

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ConStuff.ObjectHeld == null)
        {
            if (LastHeldItemParam != "")
            {
                Animator.SetFloat(LastHeldItemParam, 0.0f);
                LastHeldItemParam = "";
            }

            Animator.SetFloat(ANIMATOR_GRIP_PARAM, ConStuff.GripFloat);
            Animator.SetFloat(ANIMATOR_TRIGGER_PARAM, ConStuff.TriggerFloat);
        }
        else
        {
            if (Animator.GetFloat(ANIMATOR_GRIP_PARAM) != 0.0f)
            {
                Animator.SetFloat(ANIMATOR_GRIP_PARAM, 0.0f);
                Animator.SetFloat(ANIMATOR_TRIGGER_PARAM, 0.0f);
            }

            LastHeldItemParam = ConStuff.ObjectHeld.name;
            Animator.SetFloat(ConStuff.ObjectHeld.name, 1.0f);
        }
    }
}
