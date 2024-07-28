using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_damage_radius : MonoBehaviour
{
    [SerializeField] private bool canAttack;

    [SerializeField] float delay;
    [SerializeField] private float windup;
    [SerializeField] GameObject PlayerDetector;
    [SerializeField] private MinionAnimationScript minionAnimationScriptRef;
    [SerializeField] private minion_seeking_mechanism minionSeekingMechanismRef;
    [SerializeField] private StaggerScript staggerScriptRef;
    [SerializeField] private BoxCollider attackHitboxRef;
    [SerializeField] private float timeToDeAggro;

    public float timeBeforeEnemyCanAttack;
    public bool Attackingg;
    public GameObject Nav;
    public bool startAttack;
    public bool playerIsInRange = false;
    
    private Coroutine attackCoroutine;
    

    void Start()
    {
        minionAnimationScriptRef = gameObject.transform.parent.GetChild(0).GetComponent<MinionAnimationScript>();
        minionSeekingMechanismRef = gameObject.transform.parent.GetComponent<minion_seeking_mechanism>();
        staggerScriptRef = gameObject.transform.parent.GetComponent<StaggerScript>();
        attackHitboxRef = gameObject.transform.GetChild(0).GetComponent<BoxCollider>();
        Attackingg = false;
        startAttack = false;
        canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsInRange == true && !canAttack)
        {
            timeBeforeEnemyCanAttack -= Time.deltaTime;
            
            if (timeBeforeEnemyCanAttack <= 0 && !Attackingg && !canAttack)
            {
                timeBeforeEnemyCanAttack = 0;
                canAttack = true;
            }
        }

        if (playerIsInRange == true && canAttack)
        {
            
            if(timeBeforeEnemyCanAttack <= 0.0f)
            {
                attackCoroutine = StartCoroutine(Attack());
                
                Attackingg = true;
                canAttack = false;
                minionSeekingMechanismRef.isMoving = false;
            }
        }


        if (!playerIsInRange && timeToDeAggro > 0)
        {
            timeToDeAggro -= Time.deltaTime;
        }

        if (playerIsInRange && timeToDeAggro <= 0)
        {
            StopAllCoroutines();
            timeBeforeEnemyCanAttack = 0.5f;
        }
    }

    private void OnTriggerEnter (Collider other)
    {

        if(other.gameObject.tag == "Player")
        {

            playerIsInRange = true;
            minionSeekingMechanismRef.isMoving = false;
        }
        timeBeforeEnemyCanAttack = 0.5f;
        timeToDeAggro = 1f;
    }

    
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            playerIsInRange = false;
        
        }

    }

    
    IEnumerator Attack()
    {
        minionAnimationScriptRef.SetAnimatorTrigger("triggerAttackWindup");
        yield return new WaitForSeconds(windup);

        minionAnimationScriptRef.SetAnimatorTrigger("triggerAttack");
        yield return new WaitForSeconds(0.26f);

     
        attackHitboxRef.enabled = true;
        yield return new WaitForSeconds(0.1f);
        attackHitboxRef.enabled = false;
        
        
        
        yield return new WaitForSeconds(0.8f);
        Attackingg = false;
        timeBeforeEnemyCanAttack = delay;
    }
    
    public void InterruptAttack()
    {
        StopAllCoroutines();

        /*
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        */
        Attackingg = false;
        canAttack = false;
    }
}
