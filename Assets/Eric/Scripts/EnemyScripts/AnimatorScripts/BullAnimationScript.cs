using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BullAnimationScript : MonoBehaviour
{
    [SerializeField] private Animator animatorRef;
    [SerializeField] private SpriteRenderer spriteRendererRef;
    [SerializeField] private NavMeshAgent navMeshAgentRef;
    [SerializeField] private bull_script bullScriptRef;
    [SerializeField] private StaggerScript staggerScriptRef;
    [SerializeField] private EnemyCombatScript enemyCombatScriptRef;
    [SerializeField] enemy_player_detection PlayerDetection;
    [SerializeField] GameObject Self;
    public bool CanDestroySelf;

    private void Start()
    {

        CanDestroySelf = false;
        animatorRef = gameObject.GetComponent<Animator>();
        spriteRendererRef = gameObject.GetComponent<SpriteRenderer>();
        navMeshAgentRef = gameObject.transform.parent.GetComponent<NavMeshAgent>();
        bullScriptRef = gameObject.transform.parent.GetComponent<bull_script>();
        Self = bullScriptRef.Self;
        staggerScriptRef = gameObject.transform.parent.GetComponent<StaggerScript>();
        enemyCombatScriptRef = gameObject.transform.parent.GetComponent<EnemyCombatScript>();
        PlayerDetection = Self.transform.GetChild(1).gameObject.GetComponent<enemy_player_detection>();

    }

    private void Update()
    {

        print (IsAnimationAtEnd(animatorRef,"BullDeath"));

        //flips the sprite in the direction the enemy is moving
        if (navMeshAgentRef.velocity.x > 0)
        {
            spriteRendererRef.flipX = true;
        }
        else if (navMeshAgentRef.velocity.x < 0)
        {
            spriteRendererRef.flipX = false;
        }

        SetAnimatorParameter("isStaggered", staggerScriptRef.isStaggered);
        SetAnimatorParameter("isAttacking", bullScriptRef.isAttacking);
        SetAnimatorParameter("isMoving", PlayerDetection.isMoving);
        SetAnimatorParameter("isDead", enemyCombatScriptRef.isDead);

        if (enemyCombatScriptRef.isDead == true)
        {

            SetAnimatorParameter("isDead", true);

        }
        if(IsAnimationAtEnd(animatorRef,"Death"))
        {

            CanDestroySelf = true;

        }

    }

    public void SetAnimatorParameter(string id, bool value)
    {
        animatorRef.SetBool(id, value);
    }


    public void SetAnimatorTrigger(string id)
    {
        animatorRef.SetTrigger(id);
    }

    
    public void AnimationEvent(string id)
    {
        if (id.ToUpper().Equals("RUSH START"))
        {
            bullScriptRef.AnimationEventRushStart();
        }
        if (id.ToUpper().Equals("RUSH END"))
        {
            bullScriptRef.AnimationEventRushEnd();
        }
    }

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
    
}
//SetAnimatorParameter("isDead", enemyCombatScriptRef.isDead);