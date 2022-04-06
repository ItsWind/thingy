using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused = false;
    public static Vector3 LastPlayerLocation;

    public ControllerStuff ClickableController;

    private bool offClick = true;
    void Update()
    {
        if (offClick && ClickableController.AxisClicked)
        {
            offClick = false;

            IsPaused = !IsPaused;
            if (IsPaused)
            {
                XRObjectsGet.LeftController.ForceHoldIfObjectHeld();
                XRObjectsGet.RightController.ForceHoldIfObjectHeld();

                // teleporting
                LastPlayerLocation = new Vector3(XRObjectsGet.XRPlayer.Camera.transform.position.x, XRObjectsGet.XRPlayer.transform.position.y, XRObjectsGet.XRPlayer.Camera.transform.position.z);
                XRObjectsGet.XRPlayer.transform.position = transform.position;

                // skybox and light
                RenderSettings.ambientLight = Color.grey;

                StaticDisplay.Instance.SetText("Paused");

                Time.timeScale = 0.0f;
            }
            else
            {
                // teleporting back
                XRObjectsGet.XRPlayer.transform.position = LastPlayerLocation;

                // skybox and light
                RenderSettings.ambientLight = Color.black;

                StaticDisplay.Instance.TurnOff();

                Time.timeScale = 1.0f;

                XRObjectsGet.LeftController.DropForceHold();
                XRObjectsGet.RightController.DropForceHold();
            }
        }

        if (!offClick && !ClickableController.AxisClicked)
            offClick = true;
    }
}
