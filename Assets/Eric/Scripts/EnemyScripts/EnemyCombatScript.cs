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
    [SerializeField] private float lightStaggerDuration = 0.3f;
    [SerializeField] private float mediumStaggerDuration = 0.5f;
    [SerializeField] private float HeavyStaggerDuration = 1f;
    [SerializeField] private float damageTakenMultiplier = 1.0f;
    public bool isStaggered;
    public bool isHittable;
    public bool isDead;

    [Header(" - EnemyHealthBar")]
    [SerializeField] public EnemyHealthBar HealthBarScriptRef;
    [SerializeField] private GameObject HealthBarParentObjectRef;

    [Header(" - EnemyAttackParameters")]

    public EnemyType enemyType;

    [Header("References")]
    public GameObject playerRef;

    [Header(" - DebugStuff")]
    [SerializeField] EnemyColorSystem enemyColorSystemRef;
    [SerializeField] StaggerScript staggerScriptRef;
    [SerializeField] private PlayerCombatScript playerCombatScriptRef;

    // By Murilo

    [SerializeField] GameObject Cam;
    [SerializeField] CamShake_Script CamManager;
    [SerializeField] Animator CamAnim;
    [SerializeField] GameObject ActualCamera;
    public BullAnimationScript bullAnimScript;
    public SniperAnimationScript SniperAnimScript;
    public MinionAnimationScript MinionAnimScript;
    [SerializeField] PlayerColorSystem PlayerColorSystemRef;


    //enemyTypeSetup
    public enum EnemyType
    {
        dummy,
        contactdamage,
        melee,
        rangedprojectile
    }

    private void Start()
    {

        if(enemyType.ToString().ToUpper().Equals("CONTACTDAMAGE"))
        {

            bullAnimScript = gameObject.transform.GetChild(0).GetComponent<BullAnimationScript>();

        }else if(enemyType.ToString().ToUpper().Equals("RANGEDPROJECTILE"))
        {

            SniperAnimScript = gameObject.transform.GetChild(0).GetComponent<SniperAnimationScript>();

        }else if(enemyType.ToString().ToUpper().Equals("MELEE"))
        {

            MinionAnimScript = gameObject.transform.GetChild(0).GetComponent<MinionAnimationScript>();

        }else
        {

            SniperAnimScript = null;
            bullAnimScript = null;
            MinionAnimScript = null;

        }

        PlayerColorSystemRef = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerColorSystem>();

        enemyColorSystemRef = GetComponent<EnemyColorSystem>();
        staggerScriptRef = GetComponent<StaggerScript>();
        playerCombatScriptRef = playerRef.GetComponent<PlayerCombatScript>();
        
        healthPoints = maxHealth;
        isHittable = true;

        staggerScriptRef.enemyType = enemyType.ToString().ToUpper();
        Cam = GameObject.FindGameObjectWithTag("CamManager");
        ActualCamera = Cam.transform.GetChild(0).gameObject;
        CamManager = Cam.GetComponent<CamShake_Script>();
        CamAnim = ActualCamera.GetComponent<Animator>();

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
            if (isStaggered) 
            { 
                staggerTimer = 0.0f;
                isStaggered = false;
                staggerScriptRef.StaggerEnd();
            }
        }

        if(healthPoints <= 0.0f && !enemyType.ToString().ToUpper().Equals("DUMMY"))
        {
            isDead = true;
            if(enemyType.ToString().ToUpper().Equals("CONTACTDAMAGE"))
            {
                
                if(bullAnimScript.CanDestroySelf == true)
                {

                    EnemyDied();

                }
            
            }

            if(enemyType.ToString().ToUpper().Equals("RANGEDPROJECTILE"))
            {
                isDead = true;
                if(SniperAnimScript.CanDestroySelf == true)
                {

                    EnemyDied();

                }
            
            }

            if(enemyType.ToString().ToUpper().Equals("MELEE"))
            {
                isDead = true;
                if(MinionAnimScript.CanDestroySelf == true)
                {

                    EnemyDied();

                }
            
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerHitboxScript HitboxScriptRef = other.GetComponent<PlayerHitboxScript>();

        if (other.gameObject.CompareTag("PlayerAttack") && isHittable)
        {
            //Staggers the enemy if possible
            if (isStaggerable)
            {
                isStaggered = true;
                //staggerTimer = maxStaggerTimer;
                if(HitboxScriptRef.damage <= 15)
                {
                    if(lightStaggerDuration > staggerTimer)
                    {
                        if(PlayerColorSystemRef.blue == true)
                        {

                        StaggerEnemy(lightStaggerDuration);
                        
                        }
                    }
                }
                else if (HitboxScriptRef.damage > 15 && HitboxScriptRef.damage <= 20)
                {
                    if(mediumStaggerDuration > staggerTimer)
                    {
                        if(PlayerColorSystemRef.blue == true)
                        {

                        StaggerEnemy(mediumStaggerDuration);

                        }
                    }
                }
                else
                {
                    if(HeavyStaggerDuration > staggerTimer)
                    {
                        if(PlayerColorSystemRef.blue == true)
                        {

                        StaggerEnemy(HeavyStaggerDuration);

                        }
                    }
                }
                
                if(PlayerColorSystemRef.blue == true)
                {

                staggerScriptRef.OnStagger();
                
                }

            }

            //Subtracts hp from the attack received
            DamageEnemy(HitboxScriptRef.damage);
            if(CamAnim.GetBool("Shake1") == false && CamAnim.GetBool("Shake2") == false && CamAnim.GetBool("Shake3") == false)
            {
                
            CamManager.Shake();

            }

            //Stacks the ink the player had equipped at the time of the hit
            AddInk(HitboxScriptRef.isRed, HitboxScriptRef.isGreen, HitboxScriptRef.isBlue, HitboxScriptRef.stacksOfInk);

            //Refreshes the ink debuff duration
            enemyColorSystemRef.debuffTimer = enemyColorSystemRef.maxDebuffTimer;
        }
    }

    private void OnDestroy()
    {
        Destroy(HealthBarParentObjectRef);
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
            DamageEnemy(enemyColorSystemRef.damageOverTime / enemyColorSystemRef.debuffsActive, true);
        }
        else
        {
            DamageEnemy(enemyColorSystemRef.damageOverTime, true);
        }


        if (enemyColorSystemRef.redStacks > 0)
        {
            StartCoroutine(RedDoT());
        }

    }

    public void DamageEnemy(float dmg, bool imediate)
    {
        healthPoints -= dmg * damageTakenMultiplier;

        if (enemyColorSystemRef.isWithYellowLifestealMark)
        {
            StartCoroutine(enemyColorSystemRef.YellowLifestealHeal(dmg * enemyColorSystemRef.yellowLifeStealPercentage));
        }


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

    public void StaggerEnemy(float staggerDuration)
    {
        staggerTimer = staggerDuration;
    }

    private void EnemyDied()
    {
        isDead = true;
        Destroy(gameObject, 1f);
    }

    public void SetDamageTakenMultiplier(float multiplier)
    {
        damageTakenMultiplier = multiplier;
    }






    





    //Shortcuts to other functions

    //DamageTaken shortcut without the specification if the health bar should be animated
    public void DamageEnemy(float dmg)
    {
        DamageEnemy(dmg, false);
    }

    //Updates the hit bar with the "draining" animation; also if speed not provided the speed defaults to 10
    public void UpdateHealthBar()
    {
        UpdateHealthBar(10);
    }

    //if SetDamageTakenMukltiplier is called without a float it goes back to 1.0f
    public void SetDamageTakenMultiplier()
    {
        SetDamageTakenMultiplier(1.0f);
    }

}
