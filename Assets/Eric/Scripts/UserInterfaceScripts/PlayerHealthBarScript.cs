using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarScript : MonoBehaviour
{
    [SerializeField] private Image ProgressImage;
    [SerializeField] private Color tempColor;
    [SerializeField] private GameObject GreenHealthRegenIndicator;
    [SerializeField] private GameObject CyanHealthRegenIndicator;
    [SerializeField] private GameObject YellowHealingIndicator;
    private Coroutine AnimationCoroutine;

    // timer made so the health bar doesnt violently flash yellow when an enemy with the lifesteal mark receives a dot
    public float yellowHealthBarControlTimer;

    private void Start()
    {
        GreenHealthRegenIndicator = gameObject.transform.parent.GetChild(4).gameObject;
        CyanHealthRegenIndicator = gameObject.transform.parent.GetChild(5).gameObject;
        YellowHealingIndicator = gameObject.transform.parent.GetChild(6).gameObject;

        tempColor = Color.white;
    }

    private void Update()
    {
        if (yellowHealthBarControlTimer > 0.0f)
        {
            yellowHealthBarControlTimer -= Time.deltaTime;
        }
        else if (yellowHealthBarControlTimer <= 0)
        {
            yellowHealthBarControlTimer = 0;
            if (YellowHealingIndicator.activeSelf)
            {
                YellowHealingIndicator.SetActive(false);
            }
        }
    }

    public void UpdateHealthBar(float healthToMaxHealthRatio)
    {
        //avisa caso seja passado um valor invalido (so aceita valores de 0 a 1) por meio de um clamp
        if (healthToMaxHealthRatio < 0 || healthToMaxHealthRatio > 1)
        {
            healthToMaxHealthRatio = Mathf.Clamp01(healthToMaxHealthRatio);
        }
        //faz a animacao da barra mudar
        if (healthToMaxHealthRatio != ProgressImage.fillAmount)
        {
            StopCoroutine(StartCoroutine(AnimateProgress(healthToMaxHealthRatio, 10f)));

            AnimationCoroutine = StartCoroutine(AnimateProgress(healthToMaxHealthRatio, 10f));
        }
    }

    private IEnumerator AnimateProgress(float Progress, float Speed)
    {
        float time = 0;
        float initialProgress = ProgressImage.fillAmount;

        while (time < 1)
        {
            ProgressImage.fillAmount = Mathf.Lerp(initialProgress, Progress, time);
            time += Time.deltaTime * Speed;
            yield return null;
        }

        ProgressImage.fillAmount = Progress;
    }


    public void ChangeHealthBarColor(Color color)
    {
        if(!ProgressImage.color.Equals(color))
        {
            ProgressImage.color = color;
        }
    }

    public Color GetHealthBarColor()
    {
        return ProgressImage.color;
    }

    public void TurnOnIndicator(string color)
    {
        color = color.ToUpper();

        if (color.Equals("GREEN"))
        {
            GreenHealthRegenIndicator.SetActive(true);
        }
        if (color.Equals("CYAN"))
        {
            CyanHealthRegenIndicator.SetActive(true);
        }
        if (color.Equals("YELLOW"))
        {
            YellowHealingIndicator.SetActive(true);
            yellowHealthBarControlTimer = 0.15f;
        }
    }

    public void TurnOffIndicator(string color)
    {
        color = color.ToUpper();

        if (color.Equals("GREEN"))
        {
            GreenHealthRegenIndicator.SetActive(false);
        }
        if (color.Equals("CYAN"))
        {
            CyanHealthRegenIndicator.SetActive(false);
        }
        if (color.Equals("YELLOW"))
        {
            if (yellowHealthBarControlTimer <= 0.0f)
            {
                YellowHealingIndicator.SetActive(false);
            }
        }
    }
}
