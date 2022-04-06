using System.Collections.Generic;
using UnityEngine;

public class Edible : Grabbable
{
    public string StatToModName;
    public float StatGain = 0.12f;
    public List<AudioClip> EatingSounds;

    private float distFromPlayerMouth = -1.0f; // 0.1
    private AudioSource mouthSndSrc;

    private AudioClip getRandomEatingSound()
    {
        if (EatingSounds.Count > 0)
            return EatingSounds[RandomNumGen.Instance.Next(0, EatingSounds.Count)];
        else
            return null;
    }

    private void eatThis()
    {
        PlayerStats.ModifyFloatStat(StatToModName, StatGain);
        AudioClip toUse = getRandomEatingSound();
        if (toUse != null) mouthSndSrc.PlayOneShot(toUse);
        TryDelete();
    }

    public override void OnActivate()
    {
        if (InteractorHolding != null && distFromPlayerMouth <= 0.1)
        {
            eatThis();
        }
    }

    public override void DoAwake()
    {
        mouthSndSrc = XRObjectsGet.PlayerMouth.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (InteractorHolding != null)
        {
            distFromPlayerMouth = Vector3.Distance(transform.position, mouthSndSrc.transform.position);
        }
    }
}
