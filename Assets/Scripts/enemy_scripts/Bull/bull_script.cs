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

    [SerializeField] private GameObject playerRef;
    [SerializeField] private Vector3 dashTargetPositionReference;
    public GameObject Self;


    void Start()
    {
        Self = gameObject;  
        agent = GetComponent<NavMeshAgent>();
        once = true;
        canAttack = false;
        attackHitboxRef = gameObject.transform.GetChild(2).GetComponent<BoxCollider>();
        enemyCombatScriptRef = gameObject.GetComponent<EnemyCombatScript>();
        bullAnimationScriptRef = gameObject.transform.GetChild(0).GetComponent<BullAnimationScript>();
        
        //By MUrilo
        SelfColorSystem = gameObject.GetComponent<EnemyColorSystem>();

        playerRef = GameObject.FindGameObjectWithTag("Player");

        attackHitboxRef.enabled = false;
    }


    void Update()
    {

        hasSeenPlayer = PlayerDetector.GetComponent<enemy_player_detection>().hasSeenPlayer;
        
        if(hasSeenPlayer == true && once == true)
        {
            canAttack = true;
            once = false;
        }

        Vector3 directionToPlayer = targetpos - transform.position;

        Debug.DrawRay(transform.position,directionToPlayer, Color.red);
        Debug.DrawRay(transform.position, Vector3.forward * 10f, Color.green);
        
    
        if (hasSeenPlayer == true && canAttack == true)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, directionToPlayer);
            


            if (Physics.Raycast(ray, out hit, 5f, LayerMask.GetMask("Wall")) == false && Vector3.Distance(transform.position, targetpos) <= 5f)
            {
                StartCoroutine(bullRampage());
                canAttack = false;
            }
            else
            {

                agent.SetDestination(targetpos);
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



        //byMurilo

        targetpos = gameObject.GetComponent<navigation>().destiny.transform.position;


        if (isAttacking)
        {
            transform.Translate(dashTargetPositionReference * dashSpeed * Time.deltaTime, Space.World);
            agent.SetDestination(transform.position);
        }
    }

    IEnumerator bullRampage()
    {

        agent.speed = 0;
        agent.SetDestination(transform.position);

        bullAnimationScriptRef.SetAnimatorTrigger("triggerRushBuildup");

        yield return new WaitForSeconds(Random.Range(minDelayBeforeRush, maxDelayBeforeRush));

        bullAnimationScriptRef.SetAnimatorTrigger("triggerRushStart");
        dashTargetPositionReference = Vector3.Normalize(playerRef.transform.position - transform.position);

        yield return new WaitForSeconds(0.75f);
        
        bullAnimationScriptRef.SetAnimatorParameter("rushStopBool", true);
    
        

        yield return new WaitForSeconds(delayInbetweenAttacks);
        

        canAttack = true;
        agent.speed = SelfColorSystem.enemyDefaultSpeed;




    }


    public void InterruptAttack()
    {
        StopAllCoroutines();

        canAttack = false;
        postStaggerTimerBeforeCanAttack = 0.5f;
        isPostStaggerTimerActive = true;
        enemyCombatScriptRef.isHittable = true;

        attackHitboxRef.enabled = false;
        enemyCombatScriptRef.isHittable = true;
        isAttacking = false;
    }


    //Animation Events
    
    public void AnimationEventRushStart()
    {
        print("rush start");
        attackHitboxRef.enabled = true;
        enemyCombatScriptRef.isHittable = false;
        isAttacking = true;

        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    public void AnimationEventRushEnd()
    {
        print("rush end");
        attackHitboxRef.enabled = false;
        enemyCombatScriptRef.isHittable = true;
        isAttacking = false;
        bullAnimationScriptRef.SetAnimatorParameter("rushStopBool", false);

        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        agent.SetDestination(targetpos);
    }
}
