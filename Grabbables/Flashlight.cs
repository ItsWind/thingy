using UnityEngine;

public class Flashlight : Grabbable
{
    public Light lightSrc = null;

    public AudioSource sndSrc { get; private set; } = null;
    public AudioClip sndClickOn = null;
    public AudioClip sndClickOff = null;

    public bool IsOn { get; private set; } = false;

    public override void OnActivate()
    {
        IsOn = !IsOn;
        lightSrc.enabled = IsOn;
        sndSrc.PlayOneShot(IsOn ? sndClickOn : sndClickOff);
    }

    public override void DoAwake()
    {
        sndSrc = GetComponent<AudioSource>();
    }
}
