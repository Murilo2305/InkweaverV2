using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIColorBurstCooldownScripts : MonoBehaviour
{
    [SerializeField] private Image cooldownImageRef;

    private void Start()
    {
        cooldownImageRef = gameObject.GetComponent<Image>();
    }


    public void SetCooldownDisplay(float ratio)
    {
        cooldownImageRef.fillAmount = 1f - ratio;
    }
}
