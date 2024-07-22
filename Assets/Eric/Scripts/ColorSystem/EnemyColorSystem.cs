using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorSystem : MonoBehaviour
{
    [Header(" - Health Bar Refference (Has to be set)")]
    [SerializeField] private EnemyHealthBar HealthBarScriptRef;

    [Header(" - Overall Paramaters")]
    public byte debuffsActive;
    public byte multiplierCap; //max stacks

    [Header("Timer Parameters")]
    public float debuffTimer;
    public float maxDebuffTimer;

    [Header(" - Red parameters")]
    public byte redStacks;
    public float damageOverTime;
    public float activeDamage;

    [Header(" - Green parameters")]
    public byte greenStacks;
    public float healthRegen;
    public float healingBurst;

    [Header(" - Blue")]
    public byte blueStacks;
    public float slow;
    public float rootTime;
    public float hardSlow;


    [Header(" - Active Combos")]
    [SerializeField] private bool magentaRB;
    [SerializeField] private bool yellowRG;
    [SerializeField] private bool cyanGB;

    [Header(" - Combo parameters")]
    [SerializeField] private float baseEffectDuration;
    //magenta
    [SerializeField] private float magentaDefenseMultiplier;
    //Yellow
    [SerializeField] private float yellowLifeSteal;
    //Cyan
    public float cyanHealthRegen;
    public float cyanSlowEffect;

    private void Update()
    {
        if (debuffTimer > 0.0f)
        {
            debuffTimer -= Time.deltaTime;
        }
        else if (debuffTimer <= 0.0f)
        {
            debuffTimer = 0.0f;
            redStacks = 0;
            greenStacks = 0;
            blueStacks = 0;
            debuffsActive = 0;
            magentaRB = false;
            yellowRG = false;
            cyanGB = false;
            HealthBarScriptRef.SetBarColor(Color.white);
            HealthBarScriptRef.redColorIndicator.SetActive(false);
            HealthBarScriptRef.greenColorIndicator.SetActive(false);
            HealthBarScriptRef.blueColorIndicator.SetActive(false);
        }
    }

    public void AddStacks(string color, byte Stacks)
    {
        // Adds the stacks
        if(Equals(color.ToUpper(), "RED"))
        {
            redStacks += Stacks;
            if (!HealthBarScriptRef.redColorIndicator.activeSelf)
            {
                HealthBarScriptRef.redColorIndicator.SetActive(true);
            }
        }
        else if (Equals(color.ToUpper(), "GREEN"))
        {
            greenStacks += Stacks;
            if (!HealthBarScriptRef.greenColorIndicator.activeSelf)
            {
                HealthBarScriptRef.greenColorIndicator.SetActive(true);
            }
        }
        else if (Equals(color.ToUpper(), "BLUE"))
        {
            blueStacks += Stacks;
            if (!HealthBarScriptRef.blueColorIndicator.activeSelf)
            {
                HealthBarScriptRef.blueColorIndicator.SetActive(true);
            }
        }

        // Checks to see if the stacks added created any color combos
        if (redStacks > 0 && blueStacks > 0)
        {
            if (!magentaRB)
            {
                magentaRB = true;
            }
        }
        if (redStacks > 0 && greenStacks > 0)
        {
            if (!yellowRG)
            {
                yellowRG = true;
            }
        }
        if (blueStacks > 0 && greenStacks > 0)
        {
            if (!cyanGB)
            {
                cyanGB = true;
            }
        }
        
        UpdateHealthBarColor();
    }


    private void UpdateHealthBarColor()
    {
        if (redStacks > 0 && greenStacks > 0 && blueStacks > 0)
        {
            if (!Equals(HealthBarScriptRef.GetBarColor(), Color.white))
            {
                HealthBarScriptRef.SetBarColor(Color.white);
            }
        }
        else if (magentaRB)
        {
            if (!Equals(HealthBarScriptRef.GetBarColor(), Color.magenta))
            {
                HealthBarScriptRef.SetBarColor(Color.magenta);
            }
        }
        else if (yellowRG)
        {
            if (!Equals(HealthBarScriptRef.GetBarColor(), Color.yellow))
            {
                HealthBarScriptRef.SetBarColor(Color.yellow);
            }
        }
        else if (cyanGB)
        {
            if (!Equals(HealthBarScriptRef.GetBarColor(), Color.cyan))
            {
                HealthBarScriptRef.SetBarColor(Color.cyan);
            }
        }
        else if (redStacks > 0)
        {
            if (!Equals(HealthBarScriptRef.GetBarColor(), Color.red))
            {
                HealthBarScriptRef.SetBarColor(Color.red);
            }
        }
        else if (greenStacks > 0)
        {
            if (!Equals(HealthBarScriptRef.GetBarColor(), Color.green))
            {
                HealthBarScriptRef.SetBarColor(Color.green);
            }
        }
        else if (blueStacks > 0)
        {
            if (!Equals(HealthBarScriptRef.GetBarColor(), Color.blue))
            {
                HealthBarScriptRef.SetBarColor(Color.blue);
            }
        }
        else
        {
            if (!Equals(HealthBarScriptRef.GetBarColor(), Color.white))
            {
                HealthBarScriptRef.SetBarColor(Color.white);
            }
        }
    }









    //Shortcuts to other functions

    // when amount of stacks not specified default to 1
    public void AddStacks(string color)
    {
        AddStacks(color, 1);
    }
}
