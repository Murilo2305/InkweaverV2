using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bull_script : MonoBehaviour
{  

    public bool hasSeenPlayer;
    public bool once;
    [SerializeField] float delayBeforeRush,delayInbetweenAttacks;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] GameObject PlayerDetector;
    public bool isAttacking;

    [SerializeField] private BoxCollider attackHitboxRef;
    [SerializeField] private EnemyCombatScript enemyCombatScriptRef;
    [SerializeField] private BullAnimationScript bullAnimationScriptRef;

    [SerializeField] private float postStaggerTimerBeforeCanAttack;
    [SerializeField] private bool isPostStaggerTimerActive;

    [SerializeField] private bool canAttack;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        once = true;
        canAttack = false;
        attackHitboxRef = gameObject.transform.GetChild(2).GetComponent<BoxCollider>();
        enemyCombatScriptRef = gameObject.GetComponent<EnemyCombatScript>();
        bullAnimationScriptRef = gameObject.transform.GetChild(0).GetComponent<BullAnimationScript>();
        
        
        attackHitboxRef.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        hasSeenPlayer = PlayerDetector.GetComponent<enemy_player_detection>().hasSeenPlayer;
        
        
        if(hasSeenPlayer == true && once == true)
        {
            canAttack = true;
            once = false;
        } 
        
        
        if (hasSeenPlayer == true && canAttack == true)
        {

            StartCoroutine(bullRampage());
            canAttack = false;
        }


        if (isPostStaggerTimerActive)
        {
            if(postStaggerTimerBeforeCanAttack > 0)
            {
                postStaggerTimerBeforeCanAttack -= Time.deltaTime;
            }
            if(postStaggerTimerBeforeCanAttack <= 0)
            {
                isPostStaggerTimerActive = false;
                postStaggerTimerBeforeCanAttack = 0;
                canAttack = true;
            }
        }

        //print(agent.velocity.magnitude);

        //this part of the script makes the landing work
        if(isAttacking && agent.velocity.magnitude == 0.0f)
        {
            bullAnimationScriptRef.SetAnimatorParameter("rushStopBool", true);
        }
        else
        {
            BullAnimationScript test = bullAnimationScriptRef;

            test.SetAnimatorParameter("rushStopBool", false);
        }
    }

    IEnumerator bullRampage()
    {
        print("test");

        // Section 1 - Delay before the charge
        agent.SetDestination(transform.position);

        bullAnimationScriptRef.SetAnimatorTrigger("triggerRushBuildup");

        yield return new WaitForSeconds(delayBeforeRush);


        // Section 2 - Bull charges toward the position the player is/was
        isAttacking = true;
        bullAnimationScriptRef.SetAnimatorTrigger("triggerRushStart");
        agent.SetDestination(gameObject.GetComponent<navigation>().destiny.transform.position);

        /*During the rush the hitbox is enabled and the enemy cant be hit - Moved to animation event
        attackHitboxRef.enabled = true;
        enemyCombatScriptRef.isHittable = false;
        */

        //delay before the bull can attack again - This was switched to a timer based on Update() so the "StopAllCoroutines()" from the InterruptAttack() doesnt interfere with the normal delay inbetween attacks
        yield return new WaitForSeconds(delayInbetweenAttacks);

        canAttack = true;

        /* moved to Animation Event
        attackHitboxRef.enabled = false;
        enemyCombatScriptRef.isHittable = true;
        */
    }


    public void InterruptAttack()
    {
        StopAllCoroutines();

        canAttack = false;
        postStaggerTimerBeforeCanAttack = 0.5f;
        isPostStaggerTimerActive = true;
        enemyCombatScriptRef.isHittable = true;
    }

    //Animation Events
    
    public void AnimationEventRushStart()
    {
        print("rush start");
        attackHitboxRef.enabled = true;
        enemyCombatScriptRef.isHittable = false;
    }

    public void AnimationEventRushEnd()
    {
        print("rush end");
        attackHitboxRef.enabled = false;
        enemyCombatScriptRef.isHittable = true;
        isAttacking = false;
        
        /*
        //this substitutes the delay on the coroutine inbetween attacks
        timerBeforeCanAttack = delayInbetweenAttacks;
        */    
    }
}
