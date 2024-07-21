using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//Versao alterada da barra de progresso do Llamacademy

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float DefaultSpeed = 1f;
    [SerializeField] private Image ProgressImage;

    [Header(" - Reference to the color indicators")]
    public GameObject redColorIndicator;
    public GameObject greenColorIndicator;
    public GameObject blueColorIndicator;

    private Coroutine AnimationCoroutine;


    //funcao de conveniencia
    public void SetProgress(float Progress)
    {
        SetProgress(Progress, DefaultSpeed);
    }

    // funcao real
    public void SetProgress(float Progress, float Speed)
    {
        //avisa caso seja passado um valor invalido (so aceita valores de 0 a 1) por meio de um clamp
        if (Progress < 0 || Progress > 1)
        {
            Progress = Mathf.Clamp01(Progress);
        }
        //faz a animacao da barra mudar
        if (Progress != ProgressImage.fillAmount)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(AnimateProgress(Progress, Speed));
        }
    }


    //corrotina que controla a animacao. eu tirei as partes que alteravam o gradiente de cores pois as cores seriam muadas baseado nas stacks de tinta
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

    public void ImmediateSetProgress(float Progress)
    {
        if (Progress < 0 || Progress > 1)
        {
            Progress = Mathf.Clamp01(Progress);
        }
        ProgressImage.fillAmount = Progress;
    }

    public void SetBarColor(Color color)
    {
        ProgressImage.color = color;
    }

    public Color GetBarColor()
    {
        return (ProgressImage.color);
    }
}
