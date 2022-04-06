using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade Instance;
    public bool fadeOnStart = true;
    public float fadeDuration = 2.0f;
    public Color fadeColor;
    private Renderer rend;

    private AudioSource sndSrc;
    public AudioClip sndGameOver;

    private void Awake()
    {
        Instance = this;
        sndSrc = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
            FadeIn();
    }

    public void PlaySndGameOver()
    {
        sndSrc.PlayOneShot(sndGameOver);
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0.0f;
        while(timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;

        rend.material.SetColor("_Color", newColor2);
    }
}
