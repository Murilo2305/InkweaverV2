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

        if(PlayerCS.red == true)
        {

            BrushTipSR.color = Color.red;

        }
        if(PlayerCS.blue == true)
        {

            BrushTipSR.color = Color.blue;

        }
        if(PlayerCS.green == true)
        {

            BrushTipSR.color = Color.green;

        }

        /*
        if (playerRef.GetComponent<PlayerCharacterControlerMovement>().movement.x > 0 && playerRef != null)
        {
            BrushTipSR.flipX = false;
            if(anim.GetBool("IsMoving")  == false)
            {
                anim.SetBool("IsMoving",true);
            }
            
        }else if (playerRef.GetComponent<PlayerCharacterControlerMovement>().movement.x < 0 && playerRef != null)
        {
            BrushTipSR.flipX = true;
            if(anim.GetBool("IsMoving")  == false)
            {
                anim.SetBool("IsMoving",true);
            }
            
        }else if (playerRef.GetComponent<PlayerCharacterControlerMovement>().movement.z > 0 && playerRef != null)
        {
            
            if(anim.GetBool("IsMoving")  == false)
            {
                anim.SetBool("IsMoving",true);
            }
            
        }else if (playerRef.GetComponent<PlayerCharacterControlerMovement>().movement.z < 0 && playerRef != null)
        {
            
            if(anim.GetBool("IsMoving")  == false)
            {
                anim.SetBool("IsMoving",true);
            }
            
        }else
        {

            anim.SetBool("IsMoving",false);

        }
        */

        BrushTipSR.flipX = PlayerSR.flipX;


        anim.SetBool("IsMoving", PlayerAnim.GetBool("IsMoving"));

        anim.SetBool("IsDashing",PlayerAnim.GetBool("IsDashing"));
        anim.SetBool("IsAttacking",PlayerAnim.GetBool("isAttacking"));
        anim.SetInteger("ComboTracker",PlayerAnim.GetInteger("ComboTracker"));
        anim.SetBool("IsCharging",PlayerAnim.GetBool("isCharging"));
        anim.SetBool("IsFullyCharged",PlayerAnim.GetBool("isFullyCharged"));
        anim.SetBool("IsHeavyAttacking",PlayerAnim.GetBool("isHeavyAttacking"));


        print(anim.speed);
        print(PlayerAnim.speed);
    }

     public void SetTriggerInBrushTip(string id)
    {

        anim.SetTrigger(id);
    
    }
}
