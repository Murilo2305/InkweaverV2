using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyColorSystem : MonoBehaviour
{
    [Header(" - Health Bar Refference (Has to be set)")]
    [SerializeField] private EnemyHealthBar HealthBarScriptRef;

    [Header(" - Overall Paramaters")]
    [SerializeField] private bool StacksReset;
    public byte debuffsActive;
    public byte multiplierCap = 10; //max stacks

    [Header("Timer Parameters")]
    public float debuffTimer;
    public float maxDebuffTimer = 5;

    [Header(" - Red parameters")]
    public byte redStacks;
    public bool redDoTApplied;
    public float damageOverTime = 0.1f;
    public float redActiveDamage = 15;

    [Header(" - Green parameters")]
    public byte greenStacks;
    public float greenHealthRegen = 0.2f;
    public float greenHealingBurst = 5.0f;

    [Header(" - Blue")]
    [SerializeField] private bool canBeRooted;
    public byte blueStacks;
    public float blueSlow = 0.05f;
    public float blueRootTime = 0.2f;
    public float blueHardSlow = 0.075f;
    public bool isRooted;


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

    [Header(" - DebugStuff")]
    [SerializeField] private EnemyCombatScript enemyCombatScriptRef;
    [SerializeField] private SpriteRenderer redColorburstVFXSprite;
    [SerializeField] private SpriteRenderer blueColorburstVFXSprite;
    [SerializeField] private GameObject playerRef;
    [SerializeField] private PlayerColorSystem playerColorSystemRef;
    [SerializeField] private PlayerHealthBarScript playerHealthBarScriptRef;
    [SerializeField] private NavMeshAgent navMeshAgentRef;
    [SerializeField] private float enemyDefaultSpeed;


    private void Start()
    {
        enemyCombatScriptRef = gameObject.GetComponent<EnemyCombatScript>();
        redColorburstVFXSprite = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        blueColorburstVFXSprite = gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
        playerRef = enemyCombatScriptRef.playerRef;
        playerColorSystemRef = playerRef.GetComponent<PlayerColorSystem>();
        playerHealthBarScriptRef = playerRef.GetComponent<PlayerCombatScript>().PlayerHealthBarScriptRef;
        navMeshAgentRef = gameObject.GetComponent<NavMeshAgent>();

        enemyDefaultSpeed = navMeshAgentRef.speed;
    }

    private void Update()
    {
        // improvised code to make the health bar work may delete later
        playerHealthBarScriptRef = playerRef.GetComponent<PlayerCombatScript>().PlayerHealthBarScriptRef;



        if (debuffTimer > 0.0f)
        {
            debuffTimer -= Time.deltaTime;
        }
        else if (debuffTimer <= 0.0f)
        {
            ResetStacks();
            if (playerColorSystemRef.colorburstTargets.Contains(gameObject))
            {
                playerColorSystemRef.colorburstTargets.Remove(gameObject);
            }
        }

        if (greenStacks > 0)
        {
            playerRef.GetComponent<PlayerCombatScript>().HealPlayer(greenStacks * greenHealthRegen * Time.deltaTime / debuffsActive);

            playerHealthBarScriptRef.ChangeHealthBarColor(Color.green);
        }

        if (blueStacks > 0)
        {
            if (enemyCombatScriptRef.enemyType.ToString().ToUpper().Equals("MELEE"))
            {
                navMeshAgentRef.speed = enemyDefaultSpeed * (1 - (blueSlow * blueStacks));
            }
        }
        if (isRooted)
        {
            navMeshAgentRef.speed = 0f;
        }
        else if (!isRooted && blueStacks == 0)
        {
            navMeshAgentRef.speed = enemyDefaultSpeed;
        }
    }

    public void AddStacks(string color, byte Stacks)
    {
        StacksReset = false;
        if (!playerColorSystemRef.colorburstTargets.Contains(gameObject))
        {
            playerColorSystemRef.colorburstTargets.Add(gameObject);
        }
       


        // Adds the stacks
        if (Equals(color.ToUpper(), "RED"))
        {
            redStacks += Stacks;
            if (redStacks > 10)
            {
                redStacks = 10;
            }

            if (!HealthBarScriptRef.redColorIndicator.activeSelf)
            {
                HealthBarScriptRef.redColorIndicator.SetActive(true);
            }
            
            if (!redDoTApplied)
            {
                StartCoroutine(enemyCombatScriptRef.RedDoT());
            }
        }
        else if (Equals(color.ToUpper(), "GREEN"))
        {
            greenStacks += Stacks;
            if (greenStacks > 10)
            {
                greenStacks = 10;
            }
            if (!HealthBarScriptRef.greenColorIndicator.activeSelf)
            {
                HealthBarScriptRef.greenColorIndicator.SetActive(true);
            }
        }
        else if (Equals(color.ToUpper(), "BLUE"))
        {
            blueStacks += Stacks;
            if (blueStacks > 10)
            {
                blueStacks = 10;
            }
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

        UpdateEnemyHealthBarColor();
    }


    private void UpdateEnemyHealthBarColor()
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

    public void OnColorburst()
    {
        if (redStacks > 0)
        {
            enemyCombatScriptRef.DamageEnemy(redStacks * redActiveDamage);

            RedColorburstStagger();

            if (!redColorburstVFXSprite.enabled)
            {
                StartCoroutine(RedColorburstVFX());
            }
        }

        if (blueStacks > 0)
        {
            // when enemy movement script is done make the enemy move slower based on the amount of stacks
            if (!blueColorburstVFXSprite.enabled && canBeRooted)
            {
                StartCoroutine(BlueColorburst(blueStacks * blueRootTime));
            }
        }
    }

    private void RedColorburstStagger()
    {
        enemyCombatScriptRef.isStaggered = true;
        enemyCombatScriptRef.StaggerEnemy(1.0f);
        gameObject.GetComponent<StaggerScript>().OnStagger();
    }

    private IEnumerator RedColorburstVFX()
    {
        redColorburstVFXSprite.enabled = true;
        yield return new WaitForSeconds(0.5f);
        redColorburstVFXSprite.enabled = false;
    }

    private IEnumerator BlueColorburst(float duration)
    {
        blueColorburstVFXSprite.enabled = true;
        isRooted = true;

        yield return new WaitForSeconds(duration);

        isRooted = false;
        blueColorburstVFXSprite.enabled = false;
    }

    public void ResetStacks()
    {
        if (!StacksReset)
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
            playerHealthBarScriptRef.ChangeHealthBarColor(Color.white);
            StacksReset = true;
            redDoTApplied = false;
            navMeshAgentRef.speed = enemyDefaultSpeed;
        }
    }






    //Shortcuts to other functions

    // when amount of stacks not specified default to 1
    public void AddStacks(string color)
    {
        AddStacks(color, 1);
    }
}
