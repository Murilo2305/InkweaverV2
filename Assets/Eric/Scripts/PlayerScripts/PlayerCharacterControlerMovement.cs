using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterControlerMovement : MonoBehaviour
{

    [Header(" - Movement Paramenters:")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] public bool canMove;

    [SerializeField] public Vector3 movement;

    [Header(" - Dash Parameters:")]
    [SerializeField] private Transform dash;

    [SerializeField] public bool isDashing;
    [SerializeField] public bool dashOnCooldown;
    [SerializeField] private bool canDash;

    [SerializeField] private float dashCooldown = 0.33f;
    [SerializeField] private float dashSpeed;

    /*
    [Header("Miscellaneous Variables")]
    [SerializeField] private bool hasCollided;
    */

    [Header(" - Refs:")]
    //Diretcional Indicator transform
    [SerializeField] private Transform DITransform;

    [Header(" - DebugStuff:")]
    [SerializeField] private CharacterController cc;
    [SerializeField] private PlayerCombatScript combatScriptRef;
    [SerializeField] private float horizontalmovement;
    [SerializeField] private float verticalmovement;

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
    }


    void Update()
    {
        //Move script set to its own function
        PlayerMove();

        //dashing
        if (Input.GetButtonDown("Jump") && canDash && movement != new Vector3(0.0f, 0.0f, 0.0f) && !combatScriptRef.isHeavyAttacking && !combatScriptRef.isCharging)
        {
            print("test");
            isDashing = true;
            canDash = false;
            canMove = false;
            StartCoroutine(Dash());
        }

    }






    void PlayerMove()
    {
        horizontalmovement = Input.GetAxisRaw("Horizontal");
        verticalmovement = Input.GetAxisRaw("Vertical");


        if (canMove && !combatScriptRef.isHeavyAttacking)
        {
            if (combatScriptRef.isCharging)
            {
                movement = new Vector3(horizontalmovement, 0.0f, verticalmovement);
                movement = Vector3.Normalize(movement);

                cc.Move(movement * speed * Time.deltaTime * 0.05f);
            }
            else
            {
                movement = new Vector3(horizontalmovement, 0.0f, verticalmovement);
                movement = Vector3.Normalize(movement);

                cc.Move(movement * speed * Time.deltaTime);

            }

        }
    }

    IEnumerator Dash()
    {
        dash.SetParent(null);

        while (Vector3.Distance(transform.position, dash.position) > 0)
        {
            //remember to alter the direction of raycast bc it isnt working properly
            if (Physics.Raycast(transform.position, movement, 1.0f))
            {
                break;
            }


            transform.position = (Vector3.MoveTowards(transform.position, dash.position, dashSpeed * Time.deltaTime));
            yield return new WaitForSeconds(0.0005f);

        }

        isDashing = false;
        dashOnCooldown = true;

        yield return new WaitForSeconds(dashCooldown);

        dashOnCooldown = false;
        canDash = true;
        canMove = true;

        dash.SetParent(transform);
    }



}
