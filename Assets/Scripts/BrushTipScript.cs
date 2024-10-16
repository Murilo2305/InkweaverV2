using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushTipScript : MonoBehaviour
{
    public Animator anim;
    public GameObject playerRef;
    public Animator PlayerAnim;
    public SpriteRenderer BrushTipSR;

    [SerializeField] private SpriteRenderer PlayerSR;

    public PlayerColorSystem PlayerCS;

    [Header ("stupidSpriteThing")]
    [SerializeField]
    private Sprite Idle1, 
        ChargeAttack1, ChargeAttack2, ChargeAttack3, ChargeAttack4, ChargeAttack5, ChargeAttack6, ChargeAttack7, ChargeAttack8,
        ChargeAttack9, ChargeAttack10, ChargeAttack11, ChargeAttack12, ChargeAttack13, ChargeAttack14, ChargeAttack15, ChargeAttack16, ChargeAttack17,
        ChargeAttack18,
        Dash1,
        LightAttackOne1, LightAttackOne2,
        LightAttackTwo1, LightAttackTwo2,
        LightAttackThree1, LightAttackThree2,
        Death1, Death2, Death3, Death4, Death5, Death6, Death7, Death8, Death9, Death10, Death11, Death12, Death13, Death14, Death15, Death16,
        Run1, Run2, Run3, Run4, Run5, Run6, Run7, Run8;


    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        PlayerAnim = GameObject.FindGameObjectWithTag("PlayerAnimator").gameObject.GetComponent<Animator>();
        BrushTipSR = gameObject.GetComponent<SpriteRenderer>();
        PlayerSR = PlayerAnim.gameObject.GetComponent<SpriteRenderer>();
        PlayerCS = playerRef.GetComponent<PlayerColorSystem>();
    }

    void Update()
    {

        if (PlayerCS.red == true)
        {

            BrushTipSR.color = Color.red;

        }
        if (PlayerCS.blue == true)
        {

            BrushTipSR.color = Color.blue;

        }
        if (PlayerCS.green == true)
        {

            BrushTipSR.color = Color.green;

        }

        BrushTipSR.flipX = PlayerSR.flipX;

        #region old code
        /*
        



        if (anim.GetBool("IsHeavyAttacking"))
        {
            anim.SetBool("IsMoving", false);
        }
        else
        {
            anim.SetBool("IsMoving", PlayerAnim.GetBool("IsMoving"));
        }

        anim.SetBool("IsDashing",PlayerAnim.GetBool("IsDashing"));
        anim.SetBool("IsAttacking",PlayerAnim.GetBool("isAttacking"));
        anim.SetInteger("ComboTracker",PlayerAnim.GetInteger("ComboTracker"));
        anim.SetBool("IsCharging",PlayerAnim.GetBool("isCharging"));
        anim.SetBool("IsFullyCharged",PlayerAnim.GetBool("isFullyCharged"));
        anim.SetBool("IsHeavyAttacking",PlayerAnim.GetBool("isHeavyAttacking"));
    }

     public void SetTriggerInBrushTip(string id)
    {

        anim.SetTrigger(id);
    
    */
    #endregion


    }

    public void SetPosition(int ID)
    {
        switch (ID)
        {
            #region idle
            case 1:
                BrushTipSR.sprite = Idle1;
                break;
            #endregion
            #region ChargedAttack
            case 2:
                BrushTipSR.sprite = ChargeAttack1;
                break;
            case 3:
                BrushTipSR.sprite = ChargeAttack2;
                break;
            case 4:
                BrushTipSR.sprite = ChargeAttack3;
                break;
            case 5:
                BrushTipSR.sprite = ChargeAttack4;
                break;
            case 6:
                BrushTipSR.sprite = ChargeAttack5;
                break;
            case 7:
                BrushTipSR.sprite = ChargeAttack6;
                break;
            case 8:
                BrushTipSR.sprite = ChargeAttack7;
                break;
            case 9:
                BrushTipSR.sprite = ChargeAttack8;
                break;
            case 10:
                BrushTipSR.sprite = ChargeAttack9;
                break;
            case 11:
                BrushTipSR.sprite = ChargeAttack10;
                break;
            case 12:
                BrushTipSR.sprite = ChargeAttack11;
                break;
            case 13:
                BrushTipSR.sprite = ChargeAttack12;
                break;
            case 14:
                BrushTipSR.sprite = ChargeAttack13;
                break;
            case 15:
                BrushTipSR.sprite = ChargeAttack14;
                break;
            case 16:
                BrushTipSR.sprite = ChargeAttack15;
                break;
            case 17:
                BrushTipSR.sprite = ChargeAttack16;
                break;
            case 18:
                BrushTipSR.sprite = ChargeAttack17;
                break;
            case 19:
                BrushTipSR.sprite = ChargeAttack18;
                break;
            #endregion
            #region dash
            case 20:
                BrushTipSR.sprite = Dash1;
                break;
            #endregion
            #region LightAttackOne
            case 21:
                BrushTipSR.sprite = LightAttackOne1;
                break;
            case 22:
                BrushTipSR.sprite = LightAttackOne2;
                break;
                #endregion
            #region LightAttackTwo
            case 23:
                BrushTipSR.sprite = LightAttackTwo1;
                break;
            case 24:
                BrushTipSR.sprite = LightAttackTwo2;
                break;
                #endregion
            #region LightAttackThree
            case 25:
                BrushTipSR.sprite = LightAttackThree1;
                break;
            case 26:
                BrushTipSR.sprite = LightAttackThree2;
                break;
            #endregion
            #region death
            case 27:
                BrushTipSR.sprite = Death1;
                break;
            case 28:
                BrushTipSR.sprite = Death2;
                break;
            case 29:
                BrushTipSR.sprite = Death3;
                break;
            case 30:
                BrushTipSR.sprite = Death4;
                break;
            case 31:
                BrushTipSR.sprite = Death5;
                break;
            case 32:
                BrushTipSR.sprite = Death6;
                break;
            case 33:
                BrushTipSR.sprite = Death7;
                break;
            case 34:
                BrushTipSR.sprite = Death8;
                break;
            case 35:
                BrushTipSR.sprite = Death9;
                break;
            case 36:
                BrushTipSR.sprite = Death10;
                break;
            case 37:
                BrushTipSR.sprite = Death11;
                break;
            case 38:
                BrushTipSR.sprite = Death12;
                break;
            case 39:
                BrushTipSR.sprite = Death13;
                break;
            case 40:
                BrushTipSR.sprite = Death14;
                break;
            case 41:
                BrushTipSR.sprite = Death15;
                break;
            case 42:
                BrushTipSR.sprite = Death16;
                break;
            #endregion
            #region Run
            case 43:
                BrushTipSR.sprite = Run1;
                break;
            case 44:
                BrushTipSR.sprite = Run2;
                break;
            case 45:
                BrushTipSR.sprite = Run3;
                break;
            case 46:
                BrushTipSR.sprite = Run4;
                break;
            case 47:
                BrushTipSR.sprite = Run5;
                break;
            case 48:
                BrushTipSR.sprite = Run6;
                break;
            case 49:
                BrushTipSR.sprite = Run7;
                break;
            case 50:
                BrushTipSR.sprite = Run8;
                break;
                #endregion 
        }


    }
}
