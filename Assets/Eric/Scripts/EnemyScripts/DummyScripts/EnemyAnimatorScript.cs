using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorScript : MonoBehaviour
{
    [Header(" - References")]
    [SerializeField] Animator animatorRef;
    [SerializeField] EnemyCombatScript combatScriptRef;

    private void Update()
    {
        animatorRef.SetBool("isStaggered", combatScriptRef.isStaggered);
    }
}