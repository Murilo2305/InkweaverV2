using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SniperAnimationScript : MonoBehaviour
{
    [SerializeField] private Animator animatorRef;
    [SerializeField] private SpriteRenderer spriteRendererRef;
    [SerializeField] private NavMeshAgent navMeshAgentRef;
    [SerializeField] private Sniper_script sniperScriptRef;
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
        sniperScriptRef = gameObject.transform.parent.GetComponent<Sniper_script>();
        staggerScriptRef = gameObject.transform.parent.GetComponent<StaggerScript>();
        enemyCombatScriptRef = gameObject.transform.parent.GetComponent<EnemyCombatScript>();
        PlayerDetection = gameObject.transform.parent.gameObject.transform.GetChild(1).gameObject.GetComponent<enemy_player_detection>();
    }

    private void Update()
    {
        if (navMeshAgentRef.velocity.magnitude != 0)
        {
            SetAnimatorParameter("isMoving", true);
        }
        else
        {
            SetAnimatorParameter("isMoving", false);
        }

        if (navMeshAgentRef.velocity.x > 0)
        {
            spriteRendererRef.flipX = true;
        }
        else if (navMeshAgentRef.velocity.x < 0)
        {
            spriteRendererRef.flipX = false;
        }

        SetAnimatorParameter("isStaggered", staggerScriptRef.isStaggered);
        SetAnimatorParameter("isDead", enemyCombatScriptRef.isDead);

        if (enemyCombatScriptRef.isDead == true)
        {

            SetAnimatorParameter("isDead", true);

        }
        if(IsAnimationAtEnd(animatorRef,"SniperDeath"))
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
        if (id.ToUpper().Equals("SHOOT"))
        {
            sniperScriptRef.AnimationEventShoot();
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
