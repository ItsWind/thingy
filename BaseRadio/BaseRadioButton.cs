using UnityEngine;

public class BaseRadioButton : MonoBehaviour
{
    public static BaseRadioButton Instance;

    public bool CanBePressed { get; set; } = true;

    private bool IsPressed = false;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CanBePressed) { return; }
        if (IsPressed) { return; }

        bool isHand = other.transform.parent.GetComponent<ControllerStuff>() != null;
        if (isHand)
        {
            IsPressed = true;
            MissionManager.Instance.DoNextMainStage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsPressed) { return; }

        bool isHand = other.transform.parent.GetComponent<ControllerStuff>() != null;
        if (isHand)
            IsPressed = false;
    }
}
