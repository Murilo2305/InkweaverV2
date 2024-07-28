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

    private void Start()
    {
        animatorRef = gameObject.GetComponent<Animator>();
        spriteRendererRef = gameObject.GetComponent<SpriteRenderer>();
        navMeshAgentRef = gameObject.transform.parent.GetComponent<NavMeshAgent>();
        bullScriptRef = gameObject.transform.parent.GetComponent<bull_script>();
        staggerScriptRef = gameObject.transform.parent.GetComponent<StaggerScript>();
    }

    private void Update()
    {
        //flips the sprite in the direction the enemy is moving
        if (navMeshAgentRef.velocity.x < 0)
        {
            spriteRendererRef.flipX = true;
        }
        else if (navMeshAgentRef.velocity.x > 0)
        {
            spriteRendererRef.flipX = false;
        }

        SetAnimatorParameter("isStaggered", staggerScriptRef.isStaggered);
        SetAnimatorParameter("isAttacking", bullScriptRef.isAttacking);
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
    
}
