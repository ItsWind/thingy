using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public double ChanceToFlicker = 0.0001;
    public int SecondsToFlicker = 2;
    private float currentTimeFlickered = 0.0f;

    public Light lightSrc { get; private set; } = null;

    private void flickerLight()
    {
        currentTimeFlickered += Time.deltaTime;
        if (currentTimeFlickered < SecondsToFlicker)
        {
            lightSrc.enabled = RandomNumGen.Instance.NextDouble() >= 0.5 ? true : false;
        }
        else
        {
            currentTimeFlickered = 0.0f;
            lightSrc.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lightSrc = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimeFlickered == 0.0f)
        {
            double randNum = RandomNumGen.Instance.NextDouble();
            if (randNum <= ChanceToFlicker)
            {
                flickerLight();
            }
        }
        else
        {
            flickerLight();
        }
    }
}
