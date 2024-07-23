using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerAnimationScript : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject playerRef;
    [SerializeField] private GameObject verticalLightAttackGFXGameObject;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator lightAttackGFXAnimator;
    [SerializeField] private Animator heavyAttackGFXAnimator;
    [SerializeField] private SpriteRenderer playerSpriterender;
    
    [Header("DebugStuff")]
    [SerializeField] private SpriteRenderer lightlyChargedWarning;
    [SerializeField] private SpriteRenderer fullyChargedWarning;



    private bool isDashing;
    private bool dashOnCooldown;
    private PlayerCharacterControlerMovement movementScriptRef;

    private void Start()
    {
        movementScriptRef = playerRef.GetComponent<PlayerCharacterControlerMovement>();
    }
        


    private void LateUpdate()
    {
        isDashing = movementScriptRef.isDashing;
        dashOnCooldown = movementScriptRef.dashOnCooldown;
        
        //movement animator parameters
        playerAnimator.SetBool("IsDashing", isDashing);
        playerAnimator.SetBool("DashCooldown", dashOnCooldown);

        //Movement animations
        if (playerRef.GetComponent<PlayerCharacterControlerMovement>().movement != new Vector3(0, 0, 0))
        {
            playerAnimator.SetBool("IsMoving", true);
            playerAnimator.SetFloat("PosX", playerRef.GetComponent<PlayerCharacterControlerMovement>().movement.x);
        }
        else
        {
            playerAnimator.SetBool("IsMoving", false);
        }
        if (playerRef.GetComponent<PlayerCharacterControlerMovement>().movement.x > 0)
        {
            playerSpriterender.flipX = false;
            lightlyChargedWarning.flipX = false;
            fullyChargedWarning.flipX = false;
        }
        else if (playerRef.GetComponent<PlayerCharacterControlerMovement>().movement.x < 0)
        {
            playerSpriterender.flipX = true;
            lightlyChargedWarning.flipX = true;
            fullyChargedWarning.flipX = true;
        }
    }

    public void SetParameterInPlayerAnimator(string id, bool value)
    {
        playerAnimator.SetBool(id, value);
    }
    public void SetParameterInPlayerAnimator(string id, int value)
    {
        playerAnimator.SetInteger(id, value);
    }
    public void SetTriggerInPlayerAnimator(string id)
    {
        playerAnimator.SetTrigger(id);
    }


    public void SetTriggerInPlayerLightAttackEffectAnimator()
    {
        SetTriggerInPlayerLightAttackEffectAnimator(false);
    }
    public void SetTriggerInPlayerLightAttackEffectAnimator(bool isVerticalAttack)
    {
        if (!isVerticalAttack)
        {
            lightAttackGFXAnimator.SetTrigger("OnAttackTrigger");
        }
        else
        {
            verticalLightAttackGFXGameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("OnAttackTrigger");
            verticalLightAttackGFXGameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("OnAttackTrigger");
        }
    }
    public void SetTriggerInPlayerHeavyAttackEffectAnimator()
    {
        heavyAttackGFXAnimator.SetTrigger("OnAttackTrigger");
    }
}

