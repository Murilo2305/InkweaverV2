using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    [Header(" - EnemyParameters")]
    [SerializeField] private bool isStaggerable = true;
    [SerializeField] public float maxHealth = 20.0f;
    [SerializeField] private float staggerTimer;
    [SerializeField] private float maxStaggerTimer = 0.3f;
    [SerializeField] public bool isStaggered;

    [Header(" - EnemyAttackParameters")]

    [SerializeField] EnemyType enemyType;

    [Header("References")]
    [SerializeField] GameObject playerRef;

    [Header(" - DebugStuff")]
    [SerializeField] public float healthPoints;
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
        print("test");
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            //Staggers the enemy if possible
            if (isStaggerable)
            {
                isStaggered = true;
                staggerTimer = maxStaggerTimer;
            }

            //Subtracts hp from the attack received
            healthPoints -= playerCombatScriptRef.damage;

            //Stacks the ink the player had equipped at the time
            if (playerColorSystemRef.red && enemyColorSystemRef.redStacks < enemyColorSystemRef.multiplierCap)
            {
                if (enemyColorSystemRef.redStacks == 0)
                {
                    enemyColorSystemRef.debuffsActive += 1;
                }


                enemyColorSystemRef.redStacks += 1;

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

                enemyColorSystemRef.greenStacks += 1;
            }
            else if (playerColorSystemRef.blue && enemyColorSystemRef.blueStacks < enemyColorSystemRef.multiplierCap)
            {
                if (enemyColorSystemRef.blueStacks == 0)
                {
                    enemyColorSystemRef.debuffsActive += 1;
                }

                enemyColorSystemRef.blueStacks += 1;
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
        print("test2");
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
            healthPoints -= (enemyColorSystemRef.damageOverTime / enemyColorSystemRef.debuffsActive);
        }
        else
        {
            healthPoints -= enemyColorSystemRef.damageOverTime;
        }


        if (enemyColorSystemRef.redStacks > 0)
        {
            StartCoroutine(RedDoT());
        }

    }

}
