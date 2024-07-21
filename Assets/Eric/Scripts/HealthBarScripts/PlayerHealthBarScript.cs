using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarScript : MonoBehaviour
{
    [SerializeField] private Image ProgressImage;

    public void UpdateHealthBar(float healthToMaxHealthRatio)
    {
        Mathf.Clamp01(healthToMaxHealthRatio);
        ProgressImage.fillAmount = healthToMaxHealthRatio;
    }
}
