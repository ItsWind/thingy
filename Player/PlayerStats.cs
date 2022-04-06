using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static float Health = 1.0f;
    public static float Sanity = 1.0f;
    public static float Hunger = 1.0f;
    public static int DataScore = 0;

    public Transform HealthIndicator;
    private Vector3 healthScale;
    public Transform SanityIndicator;
    private Vector3 sanityScale;
    public Transform HungerIndicator;
    private Vector3 hungerScale;
    public Text DataScoreText;

    private void TryLoad()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            Health = data.health;
            Sanity = data.sanity;
            Hunger = data.hunger;
            DataScore = data.dataScore;
        }
    }

    private void Awake()
    {
        TryLoad();
    }

    private void Start()
    {
        healthScale = HealthIndicator.localScale;
        sanityScale = SanityIndicator.localScale;
        hungerScale = HungerIndicator.localScale;
    }

    private float counterScale = 0.0f;
    private float counterHungerDeteriorate = 0.0f;
    void Update()
    {
        // Updating UI every 0.5 seconds
        counterScale += Time.deltaTime;
        if (counterScale >= 0.5f)
        {
            counterScale = 0.0f;
            if (HealthIndicator != null)
                HealthIndicator.localScale = new Vector3(healthScale.x * Health, healthScale.y * Health, healthScale.z * Health);

            if (SanityIndicator != null)
                SanityIndicator.localScale = new Vector3(sanityScale.x * Sanity, sanityScale.y * Sanity, sanityScale.z * Sanity);

            if (HungerIndicator != null)
                HungerIndicator.localScale = new Vector3(hungerScale.x * Hunger, hungerScale.y * Hunger, hungerScale.z * Hunger);

            if (DataScoreText != null)
            {
                string disp = "";
                for (int i = 0; i < DataScore; i++)
                {
                    disp += "I";
                }
                DataScoreText.text = disp;
            }
        }

        // Deteroriate hunger over time
        counterHungerDeteriorate += Time.deltaTime;
        if (counterHungerDeteriorate >= 1.0f)
        {
            counterHungerDeteriorate = 0.0f;
            Hunger -= 0.00083f;
        }

        if (Sanity <= 0.0f || Health <= 0.0f || Hunger <= 0.0f)
        {
            StartCoroutine(DoDeath());
        }
    }

    public static bool Died = false;
    IEnumerator DoDeath()
    {
        if (!Died)
        {
            Died = true;

            ScreenFade.Instance.FadeOut();
            ScreenFade.Instance.PlaySndGameOver();

            SaveSystem.ClearAll();

            yield return new WaitForSeconds(ScreenFade.Instance.fadeDuration + 2);

            Application.Quit();
        }
    }

    public static void ModifyFloatStat(string statName, float modBy)
    {
        switch (statName.ToLower())
        {
            case "health":
                Health += modBy;
                if (Health > 1.0f) Health = 1.0f;
                break;
            case "sanity":
                Sanity += modBy;
                if (Sanity > 1.0f) Sanity = 1.0f;
                break;
            case "hunger":
                Hunger += modBy;
                if (Hunger > 1.0f) Hunger = 1.0f;
                break;
            default:
                Debug.Log("you set something wrong wind fuck");
                break;
        }
    }
}
