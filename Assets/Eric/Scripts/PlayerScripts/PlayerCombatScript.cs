using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCombatScript : MonoBehaviour
{
    //Control Variables
    [Header(" - Control Variables")]
    [SerializeField] private bool canAttack;

    //Health Parameters
    [Header(" - Health Parameters")]
    [SerializeField] private float maxHealth = 100f;
    public float healthPoints = 50f;

    //Attack related parameters
    [Header(" - General Attack Parameters")]
    [SerializeField] private float baseDamage = 5.0f;

    [Header(" - Light Attack Motion Values (dmg multiplier for each attack in combo)")]
    [SerializeField] private float mvLightAttack1 = 1.0f;
    [SerializeField] private float mvLightAttack2 = 1.2f;
    [SerializeField] private float mvLightAttack3 = 2.0f;

    [Header(" - Cooldown for light attacks")]
    [SerializeField] private float cdLightAttack1 = 0.3f;
    [SerializeField] private float cdLightAttack2 = 0.35f;
    [SerializeField] private float cdLightAttack3 = 0.75f;

    [Header(" - Heavy attack Parameters")]
    [SerializeField] private float timeTillCharged = 2.0f;
    [SerializeField] private float chargeTimer;
    [SerializeField] private bool isLightlyCharged = false;
    public bool isFullyCharged = false;
    public bool isHeavyAttacking = false;
    public bool isCharging = false;

    [Header(" - Heavy Attack Motion Values")]
    [SerializeField] private float mvHeavyAttack = 1.5f;
    [SerializeField] private float mvChargedAttack = 2.5f;

    [Header(" - Combo related parameters")]
    [SerializeField] private float maxTimer = 0.75f;
    [SerializeField] private float currentTimer;
    public int comboTracker;


    //References
    [Header(" - References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lightAttackHitboxGameObjectRef;
    [SerializeField] private GameObject heavyAttackHitboxGameObjectRef;
    [SerializeField] private GameObject attackGFXGameObjectRef;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private SpriteRenderer lightlyChargedWarning;
    [SerializeField] private SpriteRenderer fullyChargedWarning;
    [SerializeField] private Transform ProjectileSpawnerTransform;
    [SerializeField] private Transform ProjectileSpawnerTransform1;
    [SerializeField] private Transform ProjectileSpawnerTransform2;
    public GameObject PlayerUIRef;


    //Debugging
    [Header(" - Debug stuff (dont interact)")]
    [SerializeField] private PlayerCharacterControlerMovement moveScriptRef;
    [SerializeField] private PlayerColorSystem playerColorSystemRef;
    [SerializeField] private BoxCollider lightAttackHitbox;
    [SerializeField] private BoxCollider heavyAttackHitbox;
    [SerializeField] private SpriteRenderer playerSpriteRef;
    [SerializeField] private SpriteRenderer lightAttackGFXSprite;
    [SerializeField] private SpriteRenderer heavyAttackGFXSprite;
    [SerializeField] private PlayerHitboxScript lightAttackHitboxScriptRef;
    [SerializeField] private PlayerHitboxScript heavyAttackHitboxScriptRef;
    [SerializeField] private NewPlayerAnimationScript playerAnimationScriptRef;
    public PlayerHealthBarScript PlayerHealthBarScriptRef;


    private void Start()
    {
        //SettingOtherRefs
        //refs of player scripts
        moveScriptRef = player.GetComponent<PlayerCharacterControlerMovement>();
        playerColorSystemRef = player.GetComponent<PlayerColorSystem>();
        playerAnimationScriptRef = player.transform.GetChild(0).GetComponent<NewPlayerAnimationScript>();
        PlayerHealthBarScriptRef = PlayerUIRef.transform.GetChild(3).GetComponent<PlayerHealthBarScript>();
        //refs of attack related things
        lightAttackHitbox = lightAttackHitboxGameObjectRef.GetComponent<BoxCollider>();
        heavyAttackHitbox = heavyAttackHitboxGameObjectRef.GetComponent<BoxCollider>();
        playerSpriteRef = player.transform.GetChild(0).GetComponent<SpriteRenderer>();
        lightAttackGFXSprite = attackGFXGameObjectRef.transform.GetChild(0).GetComponent<SpriteRenderer>();
        heavyAttackGFXSprite = attackGFXGameObjectRef.transform.GetChild(1).GetComponent<SpriteRenderer>();
        lightAttackHitboxScriptRef = lightAttackHitboxGameObjectRef.GetComponent<PlayerHitboxScript>();
        heavyAttackHitboxScriptRef = heavyAttackHitboxGameObjectRef.GetComponent<PlayerHitboxScript>();



        //redundancies
        canAttack = true;
        comboTracker = 0;
        playerAnimationScriptRef.SetParameterInPlayerAnimator("ComboTracker", 0);
        lightAttackHitbox.enabled = false;
        heavyAttackHitbox.enabled = false;
        lightlyChargedWarning.enabled = false;
        fullyChargedWarning.enabled = false;


    }

    private void Update()
    {
        //Decays the timer for the combo
        ComboDecay();

        //Attacks if button is pressed down and released quickly
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            canAttack = false;

            LightAttack();
        }
        //If the player holds down, calls up the charging function
        if (Input.GetButton("Fire1"))
        {
            ChargingAttack();
        }
        //on release, calls the charge attack function
        if (Input.GetButtonUp("Fire1"))
        {
            ReleaseChargeAttack();
        }

        // when health is actually implemented remove the folowing
        PlayerHealthBarScriptRef.UpdateHealthBar(healthPoints / maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyAttackHitboxScript HitboxScriptRef = other.GetComponent<EnemyAttackHitboxScript>();

        if (other.CompareTag("EnemyAttack"))
        {
            DamagePlayer(HitboxScriptRef.damage);
            print("player damaged for " + HitboxScriptRef.damage);
        }
    }




        //attack function
        private void LightAttack()
    {
        currentTimer = maxTimer;
        playerAnimationScriptRef.SetParameterInPlayerAnimator("isAttacking", true);
        moveScriptRef.canMove = false;

        //first attack in light attack chain
        if (comboTracker == 0)
        {
            //control variables
            comboTracker = 1;
            playerAnimationScriptRef.SetParameterInPlayerAnimator("ComboTracker", 1);
            
            if (playerSpriteRef.flipX)
            {
                lightAttackGFXSprite.flipX = false;
            }
            else
            {
                lightAttackGFXSprite.flipX = true;
            }

            //Calls the function that triggers the necessary functions and coroutines to attack
            GeneralPurposeAtttackFunction(mvLightAttack1, 0.25f, cdLightAttack1);
        }
        //second attack in combo
        else if (comboTracker == 1)
        {
            comboTracker = 2;
            playerAnimationScriptRef.SetParameterInPlayerAnimator("ComboTracker", 2);

            if (playerSpriteRef.flipX)
            {
                lightAttackGFXSprite.flipX = true;
            }
            else
            {
                lightAttackGFXSprite.flipX = false;
            }

            GeneralPurposeAtttackFunction(mvLightAttack2, 0.3f, cdLightAttack2, 0.05f);
        }
        //last attack in the three-hit combo
        else if (comboTracker == 2)
        {
            comboTracker = 0;
            playerAnimationScriptRef.SetParameterInPlayerAnimator("ComboTracker", 0);

            GeneralPurposeAtttackFunction(mvLightAttack3, 0.5f, cdLightAttack3, 0.1f);
        }
    }

    //Combo DecayFunction
    private void ComboDecay()
    {
        // as long as the player isnt attacking, the "three hit combo" will stop and reset backto the first attack
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        else if (currentTimer <= 0)
        {
            currentTimer = 0.0f;
            comboTracker = 0;
            playerAnimationScriptRef.SetParameterInPlayerAnimator("ComboTracker", 0);
        }
    }

    //Charge Attack related funtions
    private void ChargingAttack()
    {
        //As long as the player is holding the attack button, a timer counts up
        if (canAttack)
        {
            chargeTimer += Time.deltaTime;
        }

        //once the timer reaches half a second, determines that the player actually means to charge the attack and enters into "charging" state
        if (chargeTimer >= 0.5f)
        {
            playerAnimationScriptRef.SetParameterInPlayerAnimator("isCharging", true);
            isCharging = true;

        }


        //On certain time threshholds, calls the funtion to tell the player which charge level they are at
        if (chargeTimer >= (timeTillCharged / 2) && !isLightlyCharged)
        {
            isLightlyCharged = true;
            StartCoroutine(warnPlayerOfChargeLevel("LIGHT"));
        }
        else if (chargeTimer >= timeTillCharged && !isFullyCharged)
        {
            isFullyCharged = true;
            playerAnimationScriptRef.SetParameterInPlayerAnimator("isFullyCharged", true);
            StartCoroutine(warnPlayerOfChargeLevel("FULL"));
        }
    }
    private void ReleaseChargeAttack()
    {
        //On release, resets the timer back to 0 and says that the player is no longer charging the attack
        chargeTimer = 0.0f;

        playerAnimationScriptRef.SetParameterInPlayerAnimator("isCharging", false);
        isCharging = false;

        //Depending on the level of charge, releases a strong attack
        if (isFullyCharged)
        {
            canAttack = false;
            playerAnimationScriptRef.SetParameterInPlayerAnimator("isHeavyAttacking", true);
            isHeavyAttacking = true;

            SpawnProjectiles(true);
            GeneralPurposeAtttackFunction(mvChargedAttack, 0.5f, 0.75f, true);
        }
        else if (isLightlyCharged)
        {
            canAttack = false;
            playerAnimationScriptRef.SetParameterInPlayerAnimator("isHeavyAttacking", true);
            isHeavyAttacking = true;

            SpawnProjectiles(false);
            GeneralPurposeAtttackFunction(mvHeavyAttack, 0.5f, 0.75f, true);
        }

        isLightlyCharged = false;
        isFullyCharged = false;
        playerAnimationScriptRef.SetParameterInPlayerAnimator("isFullyCharged", false);
    }


    // Warns the player of which charge level theyre at
    private IEnumerator warnPlayerOfChargeLevel(string levelOfCharge)
    {
        if (levelOfCharge == "LIGHT")
        {
            lightlyChargedWarning.enabled = true;
            yield return new WaitForSeconds(0.05f);
            lightlyChargedWarning.enabled = false;
        }
        else if (levelOfCharge == "FULL")
        {
            fullyChargedWarning.enabled = true;
            yield return new WaitForSeconds(0.05f);
            fullyChargedWarning.enabled = false;
        }
    }


    // Attack Function that triggers the other functions and routines
    private void GeneralPurposeAtttackFunction(float motionValue, float hitBoxDuration, float cooldownForTheAttack, float delayBeforeAttacking, bool isHeavyAttack)
    {
        //damage setup
        if (isHeavyAttack)
        {
            heavyAttackHitboxScriptRef.damage = baseDamage * motionValue;
        }
        else
        {
            lightAttackHitboxScriptRef.damage = baseDamage * motionValue;
        }


        //parameter in question is Time the hitbox is Enabled for
        StartCoroutine(EnableAndDisableHitbox(hitBoxDuration, delayBeforeAttacking, isHeavyAttack));
        //time before the player can attack again
        StartCoroutine(attackCooldown(cooldownForTheAttack));
    }



    //Attack related Coroutines
    private IEnumerator EnableAndDisableHitbox(float duration, float delay, bool isHeavyAttack)
    {
        if (playerColorSystemRef.red)
        {
            if (isHeavyAttack)
            {
                heavyAttackHitboxScriptRef.isRed = true;
                heavyAttackHitboxScriptRef.isGreen = false;
                heavyAttackHitboxScriptRef.isBlue = false;
            }
            else
            {
                lightAttackHitboxScriptRef.isRed = true;
                lightAttackHitboxScriptRef.isGreen = false;
                lightAttackHitboxScriptRef.isBlue = false;
            }
        }
        else if (playerColorSystemRef.green)
        {
            if (isHeavyAttack)
            {
                heavyAttackHitboxScriptRef.isRed = false;
                heavyAttackHitboxScriptRef.isGreen = true;
                heavyAttackHitboxScriptRef.isBlue = false;
            }
            else
            {
                lightAttackHitboxScriptRef.isRed = false;
                lightAttackHitboxScriptRef.isGreen = true;
                lightAttackHitboxScriptRef.isBlue = false;
            }
        }
        else if (playerColorSystemRef.blue)
        {
            if (isHeavyAttack)
            {
                heavyAttackHitboxScriptRef.isRed = false;
                heavyAttackHitboxScriptRef.isGreen = false;
                heavyAttackHitboxScriptRef.isBlue = true;
            }
            else
            {
                lightAttackHitboxScriptRef.isRed = false;
                lightAttackHitboxScriptRef.isGreen = false;
                lightAttackHitboxScriptRef.isBlue = true;
            }
        }
        yield return new WaitForSeconds(delay);
        if (isHeavyAttack)
        {
            heavyAttackHitbox.enabled = true;
        }
        else
        {
            lightAttackHitbox.enabled = true;
        }


        TriggerEffect(isHeavyAttack);

        yield return new WaitForSeconds(duration);
        lightAttackHitbox.enabled = false;
        heavyAttackHitbox.enabled = false;
    }

    //tracksTime until the player can attack again
    private IEnumerator attackCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
        playerAnimationScriptRef.SetParameterInPlayerAnimator("isAttacking", false);
        playerAnimationScriptRef.SetParameterInPlayerAnimator("isHeavyAttacking", false);
        isHeavyAttacking = false;
        moveScriptRef.canMove = true;
    }


    // Attack Special Effects thing (I guess its practical effects but idk)
    private void TriggerEffect(bool isHeavyAttack)
    {
        if (playerColorSystemRef.red)
        {
            lightAttackGFXSprite.color = Color.red;
            heavyAttackGFXSprite.color = Color.red;
            attackGFXGameObjectRef.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            attackGFXGameObjectRef.transform.GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (playerColorSystemRef.green)
        {
            lightAttackGFXSprite.color = Color.green;
            heavyAttackGFXSprite.color = Color.green;
            attackGFXGameObjectRef.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
            attackGFXGameObjectRef.transform.GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (playerColorSystemRef.blue)
        {
            lightAttackGFXSprite.color = Color.blue;
            heavyAttackGFXSprite.color = Color.blue;
            attackGFXGameObjectRef.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
            attackGFXGameObjectRef.transform.GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().color = Color.blue;
        }

        if (isHeavyAttack)
        {
            playerAnimationScriptRef.SetTriggerInPlayerHeavyAttackEffectAnimator();
        }
        else
        {
            if(comboTracker == 0)
            {
               playerAnimationScriptRef.SetTriggerInPlayerLightAttackEffectAnimator(true);
            }
            else
            {
               playerAnimationScriptRef.SetTriggerInPlayerLightAttackEffectAnimator();
            }
        }
    }

    // Function that spawns the Projectiles on the heavy attacks
    private void SpawnProjectiles(bool hasBeenFullyCharged)
    {
        GameObject middleProjectile = Instantiate(projectilePrefab, ProjectileSpawnerTransform);
        middleProjectile.transform.SetParent(null);

        if (playerColorSystemRef.red)
        {
            middleProjectile.GetComponent<PlayerHitboxScript>().isRed = true;
            middleProjectile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (playerColorSystemRef.green)
        {
            middleProjectile.GetComponent<PlayerHitboxScript>().isGreen = true;
            middleProjectile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (playerColorSystemRef.blue)
        {
            middleProjectile.GetComponent<PlayerHitboxScript>().isBlue = true;
            middleProjectile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
        }

        if (hasBeenFullyCharged)
        {
            GameObject rightProjectile = Instantiate(projectilePrefab, ProjectileSpawnerTransform2);
            rightProjectile.transform.SetParent(null);
            GameObject leftProjectile = Instantiate(projectilePrefab, ProjectileSpawnerTransform1);
            leftProjectile.transform.SetParent(null);

            if (playerColorSystemRef.red)
            {
                leftProjectile.GetComponent<PlayerHitboxScript>().isRed = true;
                rightProjectile.GetComponent<PlayerHitboxScript>().isRed = true;
                leftProjectile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                rightProjectile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (playerColorSystemRef.green)
            {
                leftProjectile.GetComponent<PlayerHitboxScript>().isGreen = true;
                rightProjectile.GetComponent<PlayerHitboxScript>().isGreen = true;
                leftProjectile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                rightProjectile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (playerColorSystemRef.blue)
            {
                leftProjectile.GetComponent<PlayerHitboxScript>().isBlue = true;
                rightProjectile.GetComponent<PlayerHitboxScript>().isBlue = true;
                leftProjectile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
                rightProjectile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
    }

    public void HealPlayer(float amountOfHealing)
    {
        healthPoints += amountOfHealing;
        healthPoints = Mathf.Clamp(healthPoints, Mathf.NegativeInfinity, maxHealth);
    }
    public void DamagePlayer(float amountOfDamage)
    {
        healthPoints -= amountOfDamage;
        healthPoints = Mathf.Clamp(healthPoints, 0.0f, maxHealth);
    }


















    //Shortcuts to other functions

    //GeneralPurposeAttackFunction shortcuts:
    //case in which the function is called without delay or HeavyAttack specification -> defaults to no delay and light attack
    private void GeneralPurposeAtttackFunction(float motionValue, float hitBoxDuration, float cooldownForTheAttack)
    {
        GeneralPurposeAtttackFunction(motionValue, hitBoxDuration, cooldownForTheAttack, 0.0f, false);
    }
    //case in which the function is called without delay-> defaults to no delay
    private void GeneralPurposeAtttackFunction(float motionValue, float hitBoxDuration, float cooldownForTheAttack, bool isHeavyAttack)
    {
        GeneralPurposeAtttackFunction(motionValue, hitBoxDuration, cooldownForTheAttack, 0.0f, isHeavyAttack);
    }
    //case in which the function is called without HeavyAttack specification -> defaults to light attack
    private void GeneralPurposeAtttackFunction(float motionValue, float hitBoxDuration, float cooldownForTheAttack, float delayBeforeAttacking)
    {
        GeneralPurposeAtttackFunction(motionValue, hitBoxDuration, cooldownForTheAttack, delayBeforeAttacking, false);
    }
   

}

        
