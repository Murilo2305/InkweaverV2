using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bull_script : MonoBehaviour
{  

    public bool hasSeenPlayer;
    public bool once;
    [SerializeField] float minDelayBeforeRush,maxDelayBeforeRush,delayInbetweenAttacks;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] GameObject PlayerDetector;
    public bool isAttacking;

    [SerializeField] private BoxCollider attackHitboxRef;
    [SerializeField] private EnemyCombatScript enemyCombatScriptRef;
    [SerializeField] private BullAnimationScript bullAnimationScriptRef;

    [SerializeField] private float postStaggerTimerBeforeCanAttack;
    [SerializeField] private bool isPostStaggerTimerActive;

    [SerializeField] private bool canAttack;

    //By Murilo
    [SerializeField] float dashSpeed;
    [SerializeField] EnemyColorSystem SelfColorSystem;
    public Vector3 targetpos;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        once = true;
        canAttack = false;
        attackHitboxRef = gameObject.transform.GetChild(2).GetComponent<BoxCollider>();
        enemyCombatScriptRef = gameObject.GetComponent<EnemyCombatScript>();
        bullAnimationScriptRef = gameObject.transform.GetChild(0).GetComponent<BullAnimationScript>();
        
        //By MUrilo
        SelfColorSystem = gameObject.GetComponent<EnemyColorSystem>();
        
        
        attackHitboxRef.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

//        print(agent.speed);

        hasSeenPlayer = PlayerDetector.GetComponent<enemy_player_detection>().hasSeenPlayer;
        
        
        if(hasSeenPlayer == true && once == true)
        {
            canAttack = true;
            once = false;
        }

        Vector3 directionToPlayer = targetpos - transform.position;

        Debug.DrawRay(transform.position,directionToPlayer, Color.red);
        
    
        if (hasSeenPlayer == true && canAttack == true)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, directionToPlayer);
            


            if (Physics.Raycast(ray, out hit, 5f, LayerMask.GetMask("Wall")) && hit.collider != null)
            {
                

                StartCoroutine(bullRampage());
                canAttack = false;
            }
            else
            {

                agent.SetDestination(transform.position);
                print("test");
            }

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

        //byMurilo

        targetpos = gameObject.GetComponent<navigation>().destiny.transform.position;

    }

    IEnumerator bullRampage()
    {
        agent.speed = dashSpeed;
        print("test");

        // Section 1 - Delay before the charge
        agent.SetDestination(transform.position);

        bullAnimationScriptRef.SetAnimatorTrigger("triggerRushBuildup");

        
        yield return new WaitForSeconds (Random.Range(minDelayBeforeRush,maxDelayBeforeRush));

        // Section 2 - Bull charges toward the position the player is/was
        print("a");
        isAttacking = true;
        bullAnimationScriptRef.SetAnimatorTrigger("triggerRushStart");
        agent.SetDestination(targetpos); 

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
