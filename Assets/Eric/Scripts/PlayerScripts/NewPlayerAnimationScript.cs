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



    private bool deathAnimationHasBeenTriggered = false;
    private bool isDashing;
    private bool dashOnCooldown;
    private PlayerCharacterControlerMovement movementScriptRef;

    //By Murilo

    [SerializeField] private bool IsDying;
    private PlayerCombatScript CombatScript;
    public GameObject DeathMenuSystem;
    public Animator BrushTipAnimator;
    [SerializeField] GameObject BrushTipGFX;

    void Start()
    {

        BrushTipGFX = playerRef.transform.GetChild(3).gameObject;

        transform.position = new Vector3(0f,1.73f,transform.position.z);
        transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        BrushTipGFX.transform.position = new Vector3(0f,1.73f,BrushTipGFX.transform.position.z);
        BrushTipGFX.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        
        movementScriptRef = playerRef.GetComponent<PlayerCharacterControlerMovement>();

        DeathMenuSystem = GameObject.FindGameObjectWithTag("CanvasParentObject").transform.GetChild(2).gameObject;

        //By Murilo
        CombatScript = playerRef.GetComponent<PlayerCombatScript>();

        BrushTipAnimator = playerRef.transform.GetChild(3).gameObject.GetComponent<Animator>();

        playerAnimator.SetBool("IsDying", false);

    }
        


    private void LateUpdate()
    {
        isDashing = movementScriptRef.isDashing;
        dashOnCooldown = movementScriptRef.dashOnCooldown;

        //by Murilo
        IsDying = CombatScript.IsDying;
        
        //movement animator parameters
        playerAnimator.SetBool("IsDashing", isDashing);
        playerAnimator.SetBool("DashCooldown", dashOnCooldown);

        //By Murilo

        if(IsDying == true)
        {

            Death();

        }

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

        //By Murilo

        if(IsAnimationAtEnd(playerAnimator,"DeathAnim"))
        {

            playerRef.SetActive(false);
            DeathMenuSystem.SetActive(true);

        }

    }

    public void SetParameterInPlayerAnimator(string id, bool value)
    {
        if (!value.Equals("IsDying") && !playerAnimator.GetBool("IsDying"))
        {
            playerAnimator.SetBool(id, value);
        }
    }
    public void SetParameterInPlayerAnimator(string id, int value)
    {
        if (!playerAnimator.GetBool("IsDying"))
        {   
            playerAnimator.SetInteger(id, value);
        }
    }
    public void SetTriggerInPlayerAnimator(string id)
    {
        if (!playerAnimator.GetBool("IsDying"))
        {
            playerAnimator.SetTrigger(id);
        }
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


    //By Murilo
    public void Death()
    {
        playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        Time.timeScale = 0.0f;

        if (!deathAnimationHasBeenTriggered)
        {
            deathAnimationHasBeenTriggered = true;
            if(playerSpriterender.flipX == false)
            {
            transform.position = new Vector3(transform.position.x+0.97f,transform.position.y-0.41f,transform.position.z);
            }else
            {
            transform.position = new Vector3(transform.position.x-0.97f,transform.position.y-0.41f,transform.position.z);
            }
            transform.localScale = new Vector3(0.23f,0.23f,0.2f);
            if(playerSpriterender.flipX == true)
            {
            BrushTipGFX.transform.position = new Vector3(BrushTipGFX.transform.position.x-0.97f,BrushTipGFX.transform.position.y-0.41f,BrushTipGFX.transform.position.z);
            }else
            {
            BrushTipGFX.transform.position = new Vector3(BrushTipGFX.transform.position.x+0.97f,BrushTipGFX.transform.position.y-0.41f,BrushTipGFX.transform.position.z);
            }
            BrushTipGFX.transform.localScale = new Vector3(0.23f,0.23f,0.2f);
            playerAnimator.SetBool("IsDying", true);
            BrushTipAnimator.SetTrigger("IsDying");
        }

    
    }

    //By Murilo
    bool IsAnimationAtEnd(Animator anim, string animName)
    {

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(animName))
        {
    
            if (stateInfo.normalizedTime >= 1.0f)
            {
                
                return true;

            }

        }

        return false;

    }



    public void AnimationEventSetBrushPosition(int id)
    {
        BrushTipGFX.GetComponent<BrushTipScript>().SetPosition(id);
    }

}