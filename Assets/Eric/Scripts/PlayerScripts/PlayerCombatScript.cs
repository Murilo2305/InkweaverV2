using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCombatScript : MonoBehaviour
{
    //Control Variables
    [Header(" - Control Variables")]
    [SerializeField] private bool canAttack;
    [SerializeField] public bool isAttacking;

    //Health Parameters
    [Header(" - Health Parameters")]
    [SerializeField] private float MaxHealth = 100f;
    [SerializeField] private int HealthPoints;

    //Attack related parameters
    [Header(" - General Attack Parameters")]
    [SerializeField] private float baseDamage = 5.0f;
    [SerializeField] public float damage;

    [Header(" - Motion Values (dmg multiplier for each attack in combo)")]
    [SerializeField] private float mvLightAttack1 = 1.0f;
    [SerializeField] private float mvLightAttack2 = 1.2f;
    [SerializeField] private float mvLightAttack3 = 2.0f;

    [Header(" - Cooldown for each light attack")]
    [SerializeField] private float cdLightAttack1 = 0.3f;
    [SerializeField] private float cdLightAttack2 = 0.35f;
    [SerializeField] private float cdLightAttack3 = 0.75f;

    [Header(" - Heavy attack Parameters")]
    [SerializeField] private float mvHeavyAttack = 1.5f;
    [SerializeField] private float mvChargedAttack = 2.5f;
    [SerializeField] private float timeTillCharged = 2.0f;
    [SerializeField] private float chargeTimer;
    [SerializeField] public bool isCharging = false;
    [SerializeField] private bool isLightlyCharged = false;
    [SerializeField] public bool isFullyCharged = false;
    [SerializeField] public bool isHeavyAttacking;


    [Header(" - Combo related parameters")]
    [SerializeField] public int comboTracker;
    [SerializeField] private float currentTimer;
    [SerializeField] private float maxTimer = 0.75f;


    //References
    [Header(" - References")]
    [SerializeField] private BoxCollider attackHitbox;
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer lightlyChargedWarning;
    [SerializeField] private SpriteRenderer fullyChargedWarning;
    [SerializeField] private PlayerHealthBarScript PlayerHealthBarScriptRef;
    [SerializeField] private Animator attackVFXAnimator;
    [SerializeField] private SpriteRenderer attackVFXSprite;


    //Debugging
    [Header(" - Debug stuff (dont interact)")]
    [SerializeField] private PlayerCharacterControlerMovement moveScriptRef;
    [SerializeField] private PlayerColorSystem playerColorSystemRef;

    private void Start()
    {
        //SettingOtherRefs
        moveScriptRef = player.GetComponent<PlayerCharacterControlerMovement>();
        playerColorSystemRef = player.GetComponent<PlayerColorSystem>();

        //redundancies
        canAttack = true;
        comboTracker = 0;
        attackHitbox.enabled = false;
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
        PlayerHealthBarScriptRef.UpdateHealthBar(HealthPoints / MaxHealth);
    }

    //attack function
    private void LightAttack()
    {
        currentTimer = maxTimer;
        isAttacking = true;
        moveScriptRef.canMove = false;

        //first attack in light attack chain
        if (comboTracker == 0)
        {
            //control variables
            comboTracker = 1;

            //Calls the function that triggers the necessary functions and coroutines to attack
            GeneralPurposeAtttackFunction(mvLightAttack1, 0.25f, cdLightAttack1);
        }
        //second attack in combo
        else if (comboTracker == 1)
        {
            comboTracker = 2;

            GeneralPurposeAtttackFunction(mvLightAttack2, 0.3f, cdLightAttack2, 0.05f);

            damage = baseDamage * mvLightAttack2;
        }
        //last attack in the three-hit combo
        else if (comboTracker == 2)
        {
            comboTracker = 0;

            GeneralPurposeAtttackFunction(mvLightAttack3, 0.5f, cdLightAttack3, 0.15f);
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
            StartCoroutine(warnPlayerOfChargeLevel("FULL"));
        }
    }
    private void ReleaseChargeAttack()
    {
        //On release, resets the timer back to 0 and says that the player is no longer charging the attack
        chargeTimer = 0.0f;

        isCharging = false;

        //Depending on the level of charge, releases a strong attack
        if (isFullyCharged)
        {
            canAttack = false;
            isHeavyAttacking = true;

            GeneralPurposeAtttackFunction(mvChargedAttack, 0.5f, 0.75f);
        }
        else if (isLightlyCharged)
        {
            canAttack = false;
            isHeavyAttacking = true;

            GeneralPurposeAtttackFunction(mvHeavyAttack, 0.5f, 0.75f);
        }

        isLightlyCharged = false;
        isFullyCharged = false;
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

    private void GeneralPurposeAtttackFunction(float motionValue, float hitBoxDuration, float cooldownForTheAttack)
    {
        GeneralPurposeAtttackFunction(motionValue, hitBoxDuration, cooldownForTheAttack, 0.0f);
    }
    private void GeneralPurposeAtttackFunction(float motionValue, float hitBoxDuration, float cooldownForTheAttack, float delayBeforeAttacking)
    {
        //damage setup
        damage = baseDamage * motionValue;

        //parameter in question is Time the hitbox is Enabled for
        StartCoroutine(EnableAndDisableHitbox(hitBoxDuration, delayBeforeAttacking));
        //time before the player can attack again
        StartCoroutine(attackCooldown(cooldownForTheAttack));


    }


    //Attack related Coroutines
    private IEnumerator EnableAndDisableHitbox(float duration, float delay)
    {
        yield return new WaitForSeconds(delay);
        attackHitbox.enabled = true;

        TriggerEffect("OnAttackTrigger");

        yield return new WaitForSeconds(duration);
        attackHitbox.enabled = false;
    }

    //tracksTime until the player can attack again
    private IEnumerator attackCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
        isAttacking = false;
        isHeavyAttacking = false;
        moveScriptRef.canMove = true;
    }


    // Attack Special Effects thing (I guess its practical effects but idk)
    private void TriggerEffect(string id)
    {
        if (playerColorSystemRef.red)
        {
            attackVFXSprite.color = Color.red;
        }
        else if (playerColorSystemRef.green)
        {
            attackVFXSprite.color = Color.green;
        }
        else if (playerColorSystemRef.blue)
        {
            attackVFXSprite.color = Color.blue;
        } 
        attackVFXAnimator.SetTrigger(id);
    }

}

        
