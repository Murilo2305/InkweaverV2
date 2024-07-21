using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    [Header(" - References")]
    [SerializeField] Animator animatorRef;
    [SerializeField] GameObject EnemyRef;

    [Header(" - Dummy Parameters")]
    [SerializeField] private float timerMax = 1.0f;
    [SerializeField] private float respawnTimer = 0.0f;
    [SerializeField] private bool isDead;

    [Header("Debug stuff")]
    [SerializeField] EnemyCombatScript combatScriptRef;
    [SerializeField] EnemyColorSystem colorSystemRef;


    private void Start()
    {
        animatorRef.SetBool("isDead", false);
        combatScriptRef = EnemyRef.GetComponent<EnemyCombatScript>();
        colorSystemRef = EnemyRef.GetComponent<EnemyColorSystem>();
    }

    private void Update()
    {
        if (combatScriptRef.healthPoints <= 0.0f && !isDead)
        {
            animatorRef.SetBool("isDead", true);
            isDead = true;
            respawnTimer = timerMax;
            colorSystemRef.debuffTimer = 0.0f;
        }
        
        if (respawnTimer > 0.0f)
        {
            respawnTimer -= Time.deltaTime;
            if(respawnTimer <= 0.0f)
            {
                respawnTimer = 0.0f;
                animatorRef.SetBool("isDead", false);
                isDead = false;
                combatScriptRef.healthPoints = combatScriptRef.maxHealth;
                combatScriptRef.UpdateHealthBar();
            }
        }

    }
}
