using UnityEngine;

public class BaseRadio : MonoBehaviour
{
    public static BaseRadio Instance;

    public AudioClip SndMainMissionEntry;
    public AudioClip SndMainMissionHelpUs;
    public AudioClip SndMainMissionHelpUsConfirm;
    public AudioClip SndMainMissionFirstMission;

    private AudioSource sndSrc;

    public void PlayExplainFirstMission()
    {
        sndSrc.PlayOneShot(SndMainMissionFirstMission);
    }

    public void PlayHelpUs()
    {
        sndSrc.PlayOneShot(SndMainMissionHelpUs);
    }

    public void StopAllSounds()
    {
        sndSrc.Stop();
    }

    private void Awake()
    {
        Instance = this;
        sndSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!sndSrc.isPlaying)
        {
            switch (MissionManager.Instance.MainStage)
            {
                case 0:
                    {
                        sndSrc.PlayOneShot(SndMainMissionEntry);
                    }
                    break;
                case 1:
                    {
                        MissionManager.Instance.DoNextMainStage();
                    }
                    break;
                case 2:
                    {
                        sndSrc.PlayOneShot(SndMainMissionHelpUsConfirm);
                    }
                    break;
            }
        }
    }
}
