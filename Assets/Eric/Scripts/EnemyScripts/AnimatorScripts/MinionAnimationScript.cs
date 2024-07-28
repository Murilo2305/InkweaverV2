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

    private void Start()
    {
        animatorRef = GetComponent<Animator>();
        combatScriptRef = gameObject.transform.parent.GetComponent<EnemyCombatScript>();
        spriteRendererRef = gameObject.GetComponent<SpriteRenderer>();
        damageRadiusScriptRef = gameObject.transform.parent.GetChild(3).GetComponent<enemy_damage_radius>();
        enemyColorSystemRef = gameObject.transform.parent.GetComponent<EnemyColorSystem>();
    }


    private void Update()
    {
        animatorRef.SetBool("isStaggered", combatScriptRef.isStaggered);

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
}
