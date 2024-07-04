using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerAnimationScript : MonoBehaviour
{
    [SerializeField] GameObject playerRef;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriterender;
    //[SerializeField] float moveX;
    [SerializeField] private SpriteRenderer lightlyChargedWarning;
    [SerializeField] private SpriteRenderer fullyChargedWarning;

    private bool isDashing;
    private bool dashOnCooldown;
    private PlayerCharacterControlerMovement movementScriptRef;
    private PlayerCombatScript combatScriptRef;

    private void Start()
    {
        movementScriptRef = playerRef.GetComponent<PlayerCharacterControlerMovement>();
        combatScriptRef = playerRef.GetComponent<PlayerCombatScript>();
    }
        


    private void LateUpdate()
    {
        isDashing = movementScriptRef.isDashing;
        dashOnCooldown = movementScriptRef.dashOnCooldown;
        
        //movement animator parameters
        animator.SetBool("IsDashing", isDashing);
        animator.SetBool("DashCooldown", dashOnCooldown);

        //combat animator parameters
        animator.SetBool("isAttacking", combatScriptRef.isAttacking);
        animator.SetBool("isHeavyAttacking", combatScriptRef.isHeavyAttacking);
        animator.SetBool("isCharging", combatScriptRef.isCharging);
        animator.SetBool("isFullyCharged", combatScriptRef.isFullyCharged);
        animator.SetInteger("ComboTracker", combatScriptRef.comboTracker);


        if (playerRef.GetComponent<PlayerCharacterControlerMovement>().movement != new Vector3(0, 0, 0))
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("PosX", playerRef.GetComponent<PlayerCharacterControlerMovement>().movement.x);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        if (playerRef.GetComponent<PlayerCharacterControlerMovement>().movement.x > 0)
        {
            spriterender.flipX = false;
            lightlyChargedWarning.flipX = false;
            fullyChargedWarning.flipX = false;
        }
        else if (playerRef.GetComponent<PlayerCharacterControlerMovement>().movement.x < 0)
        {
            spriterender.flipX = true;
            lightlyChargedWarning.flipX = true;
            fullyChargedWarning.flipX = true;
        }
    }
}

