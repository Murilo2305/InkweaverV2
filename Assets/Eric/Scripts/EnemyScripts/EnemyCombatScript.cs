using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    [Header(" - EnemyParameters")]
    [SerializeField] private bool isStaggerable = true;
    [SerializeField] public float maxHealth = 20.0f;
    [SerializeField] public float healthPoints;
    [SerializeField] private float staggerTimer;
    [SerializeField] private float maxStaggerTimer = 0.3f;
    public bool isStaggered;

    [Header(" - EnemyHealthBar")]
    [SerializeField] public EnemyHealthBar HealthBarScriptRef;
    [SerializeField] private GameObject HealthBarParentObjectRef;

    [Header(" - EnemyAttackParameters")]

    [SerializeField] EnemyType enemyType;

    [Header("References")]
    public GameObject playerRef;

    [Header(" - DebugStuff")]
    [SerializeField] PlayerCombatScript playerCombatScriptRef;
    [SerializeField] PlayerColorSystem playerColorSystemRef;
    [SerializeField] EnemyColorSystem enemyColorSystemRef;


    //enemyTypeSetup
    public enum EnemyType
    {
        dummy,
        contactDamage,
        melee,
        rangedProjectile
    }

    private void Start()
    {
        
        playerCombatScriptRef = playerRef.GetComponent<PlayerCombatScript>();
        playerColorSystemRef = playerRef.GetComponent<PlayerColorSystem>();
        enemyColorSystemRef = GetComponent<EnemyColorSystem>();
        healthPoints = maxHealth;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            //Staggers the enemy if possible
            if (isStaggerable)
            {
                isStaggered = true;
                staggerTimer = maxStaggerTimer;
            }

            //Subtracts hp from the attack received
            DamageTaken(playerCombatScriptRef.damage);

            //Stacks the ink the player had equipped at the time of the hit
            if (playerColorSystemRef.red && enemyColorSystemRef.redStacks < enemyColorSystemRef.multiplierCap)
            {
                if (enemyColorSystemRef.redStacks == 0)
                {
                    enemyColorSystemRef.debuffsActive += 1;
                }
                enemyColorSystemRef.AddStacks("RED");

                if (enemyColorSystemRef.redStacks == 1)
                {
                    StartCoroutine(RedDoT());
                }
            }
            else if (playerColorSystemRef.green && enemyColorSystemRef.greenStacks < enemyColorSystemRef.multiplierCap)
            {
                if (enemyColorSystemRef.greenStacks == 0)
                {
                    enemyColorSystemRef.debuffsActive += 1;
                }

                enemyColorSystemRef.AddStacks("GREEN");
            }
            else if (playerColorSystemRef.blue && enemyColorSystemRef.blueStacks < enemyColorSystemRef.multiplierCap)
            {
                if (enemyColorSystemRef.blueStacks == 0)
                {
                    enemyColorSystemRef.debuffsActive += 1;
                }

                enemyColorSystemRef.AddStacks("BLUE");
            }

            //Refreshes the ink debuff duration
            enemyColorSystemRef.debuffTimer = enemyColorSystemRef.maxDebuffTimer;
        }
    }

    private void Update()
    {
        //Counts down the time staggered until the enemy goes back to normal
        if (staggerTimer > 0)
        {
            staggerTimer -= Time.deltaTime;
        }
        else
        {
            staggerTimer = 0.0f;
            isStaggered = false;
        }
    }

    private IEnumerator RedDoT()
    {
        if (enemyColorSystemRef.redStacks > 0)
        {
            yield return new WaitForSeconds((0.1f / enemyColorSystemRef.redStacks));
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (enemyColorSystemRef.debuffsActive > 0)
        { 
            DamageTaken(enemyColorSystemRef.damageOverTime / enemyColorSystemRef.debuffsActive, true);
        }
        else
        {
            DamageTaken(enemyColorSystemRef.damageOverTime, true);
        }


        if (enemyColorSystemRef.redStacks > 0)
        {
            StartCoroutine(RedDoT());
        }

    }

    private void DamageTaken(float dmg)
    {
        DamageTaken(dmg, false);
    }
    private void DamageTaken(float dmg, bool imediate)
    {
        healthPoints -= dmg;
        if (imediate)
        {
            InstantUpdateHealthBar();
        }
        else
        {
            UpdateHealthBar();
        }
    }



    //Updates the hit bar with the "draining" animation; also if speed not provided the speed defaults to 10
    public void UpdateHealthBar()
    {
        UpdateHealthBar(10);
    }
    public void UpdateHealthBar(int speed)
    {
        HealthBarScriptRef.SetProgress(healthPoints / maxHealth, speed);
    }

    //Immediatly changes the healthbar value
    public void InstantUpdateHealthBar()
    {
        HealthBarScriptRef.ImmediateSetProgress(healthPoints / maxHealth);
    }


    public void SetupHealthBar(Canvas Canvas)
    {
        HealthBarParentObjectRef.transform.SetParent(Canvas.transform);
    }
}
       