using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticDisplay : MonoBehaviour
{
    public static StaticDisplay Instance;

    public Text TextDisplay;

    public void TurnOff()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SetText(string textToDisplay)
    {
        TurnOn();
        TextDisplay.text = textToDisplay;
    }

    private void Awake()
    {
        Instance = this;
        TurnOff();
    }
}
