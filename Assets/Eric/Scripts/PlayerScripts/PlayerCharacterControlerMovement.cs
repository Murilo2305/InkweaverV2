using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterControlerMovement : MonoBehaviour
{

    [Header(" - Movement Paramenters:")]
    [SerializeField] private float speed = 10.0f;
    public bool canMove;

    public Vector3 movement;

    [Header(" - Dash Parameters:")]
    [SerializeField] private Transform dash;
    [SerializeField] private float dashRedundancy;

    public bool isDashing;
    public bool dashOnCooldown;
    public bool canDash;

    public float dashCooldownMax = 0.33f;
    public float dashCooldownTimer = 0.0f;
    [SerializeField] private float dashSpeed;

    /*
    [SerializeField] private bool hasCollided;
    */
    [Header("Miscellaneous Variables")]
    public bool playerIsOutOfBounds;

    [Header(" - Refs:")]
    //Diretcional Indicator transform
    [SerializeField] private Transform DITransform;

    [Header(" - DebugStuff:")]
    [SerializeField] private CharacterController cc;
    [SerializeField] private PlayerCombatScript combatScriptRef;
    [SerializeField] private float horizontalmovement;
    [SerializeField] private float verticalmovement;
    [SerializeField] private PlayerUIDashCooldownScript playerUIDashCooldownScriptRef;

    /*
     * Obsolete stuff
    [SerializeField] private Transform current;
    [SerializeField] private bool collision;
    [SerializeField] private Vector3 movespeed;
    */


    void Start()
    {
        //setting refs
        cc = GetComponent<CharacterController>();
        combatScriptRef = GetComponent<PlayerCombatScript>();

        //redundancies
        dash.SetParent(transform);
        canDash = true;
        canMove = true;
        playerIsOutOfBounds = false;
    }


    void Update()
    {
        if (playerUIDashCooldownScriptRef == null)
        {
            playerUIDashCooldownScriptRef = combatScriptRef.PlayerUIRef.transform.GetChild(1).GetChild(0).GetComponent<PlayerUIDashCooldownScript>();
        }

        //Move script set to its own function
        PlayerMove();

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        //dashing
        if (Input.GetButtonDown("Jump") && canDash && movement != new Vector3(0.0f, 0.0f, 0.0f) && !combatScriptRef.isHeavyAttacking && !combatScriptRef.isCharging)
        {
            isDashing = true;
            canDash = false;
            canMove = false;
            combatScriptRef.canAttack = false;
            StartCoroutine(Dash());

            dashRedundancy = 0.3f;
        }

        DashRedundancyFunction();
        DashCooldownFunction();
    }






    void PlayerMove()
    {
        if (!combatScriptRef.IsDying)
        {
            horizontalmovement = Input.GetAxisRaw("Horizontal");
            verticalmovement = Input.GetAxisRaw("Vertical");


            if (canMove)
            {
                if (combatScriptRef.isCharging)
                {
                    movement = new Vector3(horizontalmovement, 0.0f, verticalmovement);
                    movement = Vector3.Normalize(movement);

                    if (!playerIsOutOfBounds)
                    {
                        cc.Move(movement * speed * Time.deltaTime * 0.05f);
                    }
                }
                else
                {
                    movement = new Vector3(horizontalmovement, 0.0f, verticalmovement);
                    movement = Vector3.Normalize(movement);

                    if (!playerIsOutOfBounds)
                    {
                        cc.Move(movement * speed * Time.deltaTime);
                    }
                }
            }
        }
    }

    IEnumerator Dash()
    {
        dash.SetParent(null);

        combatScriptRef.isInvulnerable = true;

        while (Vector3.Distance(transform.position, dash.position) > 0)
        {

            //remember to alter the direction of raycast bc it isnt working properly
            if (Physics.Raycast(transform.position, movement, 1.5f, LayerMask.GetMask("Wall")))
            {
                break;
            }

            Debug.DrawRay(transform.position, movement * 1.5f);


            transform.position = (Vector3.MoveTowards(transform.position, dash.position, dashSpeed * Time.deltaTime));
            yield return new WaitForSeconds(0.0005f);

        }

        isDashing = false;
        dashOnCooldown = true;
        combatScriptRef.isInvulnerable = false;
        dashRedundancy = 0.0f;

        canMove = true;

        dashCooldownTimer = dashCooldownMax;
        /*
        #region oldDashCooldownCode
        yield return new WaitForSeconds(dashCooldownMax);

        dashOnCooldown = false;
        canDash = true;
        canMove = true;
        combatScriptRef.canAttack = true;

        dash.SetParent(transform);
        dash.transform.localPosition = Vector3.zero;
        #endregion
        */
    }

    public void InterruptDash()
    {
        StopCoroutine(Dash());

        isDashing = false;
        dashOnCooldown = true;
        combatScriptRef.isInvulnerable = false;

        canMove = true;

        dashOnCooldown = false;
        canDash = true;

        combatScriptRef.canAttack = true;

        dash.SetParent(transform);
        dash.transform.localPosition = Vector3.zero;
    }

    public void AttackStepForward()
    {
        cc.Move(new Vector3(horizontalmovement, 0.0f, verticalmovement).normalized * Time.deltaTime * 75f);
    }

    private void DashRedundancyFunction()
    {
        //this function serves the purpose of interrupting the dash in case something goes wrong and the dash lasts longer than normal
        if (dashRedundancy > 0.0f)
        {
            dashRedundancy -= Time.deltaTime;
        }
        else if (dashRedundancy <= 0.0f && isDashing)
        {
            InterruptDash();
            dashRedundancy = 0.0f;
        }
    }

    private void DashCooldownFunction()
    {
        if (dashOnCooldown)
        {
            if (dashCooldownTimer > 0.0f)
            {
                dashCooldownTimer -= Time.deltaTime;
            }
            else if (dashCooldownTimer < 0.0f)
            {
                dashCooldownTimer = 0.0f;
                dashOnCooldown = false;
                canDash = true;
                canMove = true;
                combatScriptRef.canAttack = true;

                dash.SetParent(transform);
                dash.transform.localPosition = Vector3.zero;
            }
            else if (dashCooldownTimer == 0.0f)
            {
                dashOnCooldown = false;
                canDash = true;
                canMove = true;
                combatScriptRef.canAttack = true;

                dash.SetParent(transform);
                dash.transform.localPosition = Vector3.zero;
            }
        }
        if(playerUIDashCooldownScriptRef != null)
        {
            playerUIDashCooldownScriptRef.SetCooldownDisplay(dashCooldownTimer/ dashCooldownMax);
        }
    }

    private void SetupPlayerUIRef()
    {
        if (playerUIDashCooldownScriptRef == null)
        {
            playerUIDashCooldownScriptRef = combatScriptRef.PlayerUIRef.transform.GetChild(1).GetChild(0).GetComponent<PlayerUIDashCooldownScript>();
        }
    }
}
