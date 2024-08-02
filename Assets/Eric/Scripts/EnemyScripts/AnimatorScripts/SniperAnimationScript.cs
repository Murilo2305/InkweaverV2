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

    private void Start()
    {
        animatorRef = gameObject.GetComponent<Animator>();
        spriteRendererRef = gameObject.GetComponent<SpriteRenderer>();
        navMeshAgentRef = gameObject.transform.parent.GetComponent<NavMeshAgent>();
        sniperScriptRef = gameObject.transform.parent.GetComponent<Sniper_script>();
        staggerScriptRef = gameObject.transform.parent.GetComponent<StaggerScript>();
        enemyCombatScriptRef = gameObject.transform.parent.GetComponent<EnemyCombatScript>();
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

        if (navMeshAgentRef.velocity.x < 0)
        {
            spriteRendererRef.flipX = true;
        }
        else if (navMeshAgentRef.velocity.x > 0)
        {
            spriteRendererRef.flipX = false;
        }

        SetAnimatorParameter("isStaggered", staggerScriptRef.isStaggered);
        SetAnimatorParameter("isDead", enemyCombatScriptRef.isDead);
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
}
