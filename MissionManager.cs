using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    public int MainStage { get; private set; } = 0;
    // 0 - entry
    // 1 - responded, play pleasehelpus
    // 2 - confirmation to pleasehelpus
    // 3 - first mission explanation

    public void SetMainStage(int newStage)
    {
        BaseRadio.Instance.StopAllSounds();
        MainStage = newStage;
    }

    public void DoNextMainStage()
    {
        switch (MainStage)
        {
            // entry radio message
            case 0:
                {
                    SetMainStage(1);
                    BaseRadioButton.Instance.CanBePressed = false;
                    BaseRadio.Instance.PlayHelpUs();
                }
                break;
            case 1:
                {
                    BaseRadioButton.Instance.CanBePressed = true;
                    SetMainStage(2);
                }
                break;
            case 2:
                {
                    SetMainStage(3);
                    BaseRadioButton.Instance.CanBePressed = false;
                    BaseRadio.Instance.PlayExplainFirstMission();
                }
                break;
            case 3:
                {
                    // empty
                }
                break;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
