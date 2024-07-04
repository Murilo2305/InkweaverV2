using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorSystem : MonoBehaviour
{
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
        }
    }


}
