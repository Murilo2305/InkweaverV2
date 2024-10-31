using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIDashCooldownScript : MonoBehaviour
{
    [SerializeField] private Image cooldownImageRef;

    private void Start()
    {
        cooldownImageRef = gameObject.GetComponent<Image>();
    }


    public void SetCooldownDisplay(float ratio)
    {
        cooldownImageRef.fillAmount = 1f - ratio;

        if (ratio == 0f)
        {
            gameObject.transform.parent.gameObject.GetComponent<Image>().enabled = false;
            cooldownImageRef.enabled = false;
        }
        else
        {
            gameObject.transform.parent.gameObject.GetComponent<Image>().enabled = true;
            cooldownImageRef.enabled = true;
        }
    }
}
