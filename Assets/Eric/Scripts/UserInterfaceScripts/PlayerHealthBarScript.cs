using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarScript : MonoBehaviour
{
    [SerializeField] private Image ProgressImage;
    private Coroutine AnimationCoroutine;

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
}
