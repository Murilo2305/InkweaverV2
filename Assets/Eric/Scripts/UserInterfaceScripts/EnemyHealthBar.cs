using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//Versao alterada da barra de progresso do Llamacademy

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private float DefaultSpeed = 1f;
    [SerializeField] private Image ProgressImage;
    

    [Header(" - Reference to the color indicators")]
    public GameObject redColorIndicator;
    public GameObject greenColorIndicator;
    public GameObject blueColorIndicator;
    public GameObject magentaDefenseDownIndicator;
    public GameObject cyanSlowIndicator;
    public GameObject yellowLifestealIndicator;
    public GameObject blueSlowIndicator;
    public GameObject bleedIndicator;

    private Coroutine AnimationCoroutine;

    private void Start()
    {
        redColorIndicator = gameObject.transform.GetChild(0).gameObject;
        greenColorIndicator = gameObject.transform.GetChild(1).gameObject;
        blueColorIndicator = gameObject.transform.GetChild(2).gameObject;
        magentaDefenseDownIndicator = gameObject.transform.GetChild(3).gameObject;
        cyanSlowIndicator = gameObject.transform.GetChild(4).gameObject;
        yellowLifestealIndicator = gameObject.transform.GetChild(5).gameObject;
        blueSlowIndicator = gameObject.transform.GetChild(6).gameObject;
        bleedIndicator = gameObject.transform.GetChild(7).gameObject;

        yellowLifestealIndicator.SetActive(false);
        cyanSlowIndicator.SetActive(false);
        magentaDefenseDownIndicator.SetActive(false);
    }

    

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

    public void TurnOnEffectIndicator(string color)
    {
        color = color.ToUpper();

        if (color.Equals("RED"))
        {
            if (!redColorIndicator.activeSelf)
            {
                redColorIndicator.SetActive(true);
            }
            if (!bleedIndicator.activeSelf)
            {
                bleedIndicator.SetActive(true);
            }
        } 
        else if (color.Equals("GREEN"))
        {
            if (!greenColorIndicator.activeSelf)
            {
                greenColorIndicator.SetActive(true);
            }
        }
        else if (color.Equals("BLUE"))
        {
            if (!blueColorIndicator.activeSelf)
            {
                blueColorIndicator.SetActive(true);
            }
            if (!blueSlowIndicator.activeSelf)
            {
                blueSlowIndicator.SetActive(true);
            }
        }
        else if (color.Equals("MAGENTA"))
        {
            magentaDefenseDownIndicator.SetActive(true);
        }
        else if (color.Equals("CYAN"))
        {
            cyanSlowIndicator.SetActive(true);
        }
        else if (color.Equals("YELLOW"))
        {
            yellowLifestealIndicator.SetActive(true);
        }
    }
    public void TurnOffEffectIndicator(string color)
    {
        color = color.ToUpper();

        if (color.Equals("RED"))
        {
            if (!redColorIndicator.activeSelf)
            {
                redColorIndicator.SetActive(false);
            }
            if (!bleedIndicator.activeSelf)
            {
                bleedIndicator.SetActive(false);
            }
        } 
        else if (color.Equals("GREEN"))
        {
            if (!greenColorIndicator.activeSelf)
            {
                greenColorIndicator.SetActive(false);
            }
        }
        else if (color.Equals("BLUE"))
        {
            if (!blueColorIndicator.activeSelf)
            {
                blueColorIndicator.SetActive(false);
            }
            if (!blueSlowIndicator.activeSelf)
            {
                blueSlowIndicator.SetActive(false);
            }
        }
        else if (color.Equals("MAGENTA"))
        {
            magentaDefenseDownIndicator.SetActive(false);
        }
        else if (color.Equals("CYAN"))
        {
            cyanSlowIndicator.SetActive(false);
        }
        else if (color.Equals("YELLOW"))
        {
            yellowLifestealIndicator.SetActive(false);
        }
    }
}
