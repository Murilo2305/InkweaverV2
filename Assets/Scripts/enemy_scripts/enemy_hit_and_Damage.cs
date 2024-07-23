using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_hit_and_Damage : MonoBehaviour
{
    
    
    private NavMeshAgent agent;
    [SerializeField] GameObject enemy;
    [SerializeField] float life;
    [SerializeField] float moveSpeed;
    public bool startedattacking;
    public bool isAttacking;
    [SerializeField] float recoverTime;
    [SerializeField] bool wasHit;
    
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        isAttacking = false;
        wasHit = false;
    
    }

    void Update()
    {
        

        agent.speed = moveSpeed;
    
        if(wasHit == true )
        {

            StartCoroutine("takingDamage");

        }

        if (life == 0.0f)
        {

            Destroy(gameObject);

        }

        

    }

    private IEnumerator takingDamage()
    {

        if(isAttacking == true)
        {

            isAttacking = false;

        }

        
        agent.SetDestination(transform.position);

        yield return new WaitForSeconds(recoverTime);

        isAttacking = true;

        wasHit = false;
        agent.SetDestination(gameObject.GetComponent<enemy_player_detection>().playerRef);

    }

    


}
