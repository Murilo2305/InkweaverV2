using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    [Header(" - Health")]
    public float maxHealth = 20.0f;
    public float healthPoints;

    [Header(" - EnemyParameters")]
    [SerializeField] private bool isStaggerable = true;
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
        enemyColorSystemRef = GetComponent<EnemyColorSystem>();
        healthPoints = maxHealth;
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerHitboxScript HitboxScriptRef = other.GetComponent<PlayerHitboxScript>();

        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            //Staggers the enemy if possible
            if (isStaggerable)
            {
                isStaggered = true;
                staggerTimer = maxStaggerTimer;
            }

            //Subtracts hp from the attack received
            DamageTaken(HitboxScriptRef.damage);

            //Stacks the ink the player had equipped at the time of the hit
            AddInk(HitboxScriptRef.isRed, HitboxScriptRef.isGreen, HitboxScriptRef.isBlue, HitboxScriptRef.stacksOfInk);

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

    public IEnumerator RedDoT()
    {
        enemyColorSystemRef.redDoTApplied = true;

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

    public void DamageTaken(float dmg, bool imediate)
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
    private void AddInk(bool isRed, bool isGreen, bool isBlue, byte Stacks)
    {
        if (isRed && enemyColorSystemRef.redStacks < enemyColorSystemRef.multiplierCap)
        {
            if (enemyColorSystemRef.redStacks == 0)
            {
                enemyColorSystemRef.debuffsActive += 1;
            }
            enemyColorSystemRef.AddStacks("RED", Stacks);
        }
        else if (isGreen && enemyColorSystemRef.greenStacks < enemyColorSystemRef.multiplierCap)
        {
            if (enemyColorSystemRef.greenStacks == 0)
            {
                enemyColorSystemRef.debuffsActive += 1;
            }

            enemyColorSystemRef.AddStacks("GREEN", Stacks);
        }
        else if (isBlue && enemyColorSystemRef.blueStacks < enemyColorSystemRef.multiplierCap)
        {
            if (enemyColorSystemRef.blueStacks == 0)
            {
                enemyColorSystemRef.debuffsActive += 1;
            }

            enemyColorSystemRef.AddStacks("BLUE", Stacks);
        }
    }








    //Shortcuts to other functions

    //DamageTaken shortcut without the specification if the health bar should be animated
    public void DamageTaken(float dmg)
    {
        DamageTaken(dmg, false);
    }

    //Updates the hit bar with the "draining" animation; also if speed not provided the speed defaults to 10
    public void UpdateHealthBar()
    {
        UpdateHealthBar(10);
    }
}
