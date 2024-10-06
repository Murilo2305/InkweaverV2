using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAnimationScript : MonoBehaviour
{
    [SerializeField] private Animator animatorRef;
    [SerializeField] private EnemyCombatScript combatScriptRef;
    [SerializeField] private SpriteRenderer spriteRendererRef;
    [SerializeField] private enemy_damage_radius damageRadiusScriptRef;
    [SerializeField] private EnemyColorSystem enemyColorSystemRef;
    [SerializeField] private EnemyCombatScript enemyCombatScriptRef;
    [SerializeField] enemy_player_detection PlayerDetection;
    [SerializeField] GameObject Self;
    public bool CanDestroySelf;

    private void Start()
    {
        CanDestroySelf = false;
        animatorRef = GetComponent<Animator>();
        combatScriptRef = gameObject.transform.parent.GetComponent<EnemyCombatScript>();
        spriteRendererRef = gameObject.GetComponent<SpriteRenderer>();
        damageRadiusScriptRef = gameObject.transform.parent.GetComponentInChildren<enemy_damage_radius>();
        enemyColorSystemRef = gameObject.transform.parent.GetComponent<EnemyColorSystem>();
        enemyCombatScriptRef = gameObject.transform.parent.GetComponent<EnemyCombatScript>();
        Self = gameObject.transform.parent.gameObject;
        PlayerDetection = Self.transform.GetChild(1).gameObject.GetComponent<enemy_player_detection>();
    }

    private void Update()
    {
        SetAnimatorParameter("isDead", enemyCombatScriptRef.isDead);
        SetAnimatorParameter("isStaggered", combatScriptRef.isStaggered);

        if (enemyColorSystemRef.isRooted)
        {
            animatorRef.SetBool("isMoving", false);
        }
        else
        {
            if(combatScriptRef.enemyType.ToString().ToUpper().Equals("MELEE"))
            {
                animatorRef.SetBool("isMoving", gameObject.transform.parent.GetComponent<minion_seeking_mechanism>().isMoving);
            }
        }

        if (enemyCombatScriptRef.isDead == true)
        {

            SetAnimatorParameter("isDead", true);

        }
        if(IsAnimationAtEnd(animatorRef,"Death"))
        {

            CanDestroySelf = true;

        }


        
    }

    public void SetSpriteFlip(bool value)
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX != value)
        {
            spriteRendererRef.flipX = value;
        }
    }

    public void SetAnimatorTrigger(string id)
    {
        animatorRef.SetTrigger(id);
    }

    public void SetAnimatorParameter(string id, bool value)
    {
        animatorRef.SetBool(id, value);
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

