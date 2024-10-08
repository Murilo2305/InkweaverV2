using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StaggerScript : MonoBehaviour
{
    //baseado no script enemy_hit_and_Damage

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float recoverTime;

    public bool isStaggered = false;
    public string enemyType;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void OnStagger()
    {
        isStaggered = true;

        // Stops enemy movement during the stagger duration
        agent.SetDestination(transform.position);

        //sets "isMoving" in the enemy's seeking mechanism to false so the walking animation stops during the stagger
        if (enemyType.Equals("MELEE"))
        {
            //melee enemy == minion
            GetComponent<minion_seeking_mechanism>().isMoving = false;

            // interupts the enemy's attack
            gameObject.transform.GetComponentInChildren<enemy_damage_radius>().InterruptAttack();
        }
        if(enemyType.Equals("RANGEDPROJECTILE"))
        {
            Sniper_script sniperScriptRef = gameObject.GetComponent<Sniper_script>();

            sniperScriptRef.canMove = false;

            sniperScriptRef.InterruptAttack();
        }
        if(enemyType.Equals("CONTACTDAMAGE"))
        {
            bull_script bullScriptRef = gameObject.GetComponent<bull_script>();

            bullScriptRef.InterruptAttack();
        }
    }

    public void StaggerEnd()
    {
        //Makes the enemy start moving again
        isStaggered = false;

        print("isNoLongerStaggered");


        if (enemyType.Equals("MELEE"))
        {
            //melee enemy == minion
            gameObject.transform.GetComponentInChildren<enemy_damage_radius>().timeBeforeEnemyCanAttack = 1f;
        }
        if (enemyType.Equals("RANGED PROJECTILE"))
        {
            Sniper_script sniperScriptRef = gameObject.GetComponent<Sniper_script>();

            sniperScriptRef.canMove = true;
        }
    }




}
