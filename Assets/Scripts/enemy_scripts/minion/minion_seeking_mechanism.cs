using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class minion_seeking_mechanism : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform destiny;
    [SerializeField] private GameObject PlayerDetector;
    [SerializeField] private StaggerScript staggerScriptRef;

    [SerializeField] private MinionAnimationScript minionAnimationScriptRef;
    [SerializeField] private enemy_damage_radius damageRadiusScriptRef;
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        minionAnimationScriptRef = gameObject.transform.GetChild(0).GetComponent<MinionAnimationScript>();
        damageRadiusScriptRef = gameObject.transform.GetChild(3).GetComponent<enemy_damage_radius>();
        agent = GetComponent<NavMeshAgent>();
        staggerScriptRef = GetComponent<StaggerScript>();
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if  (
            PlayerDetector.GetComponent<enemy_player_detection>().hasSeenPlayer == true
            && !staggerScriptRef.isStaggered
            && !damageRadiusScriptRef.playerIsInRange
            && !damageRadiusScriptRef.Attackingg
            )
        {
            destiny = GameObject.FindGameObjectWithTag("Player").transform;
            agent.SetDestination (destiny.transform.position);

            if (!isMoving)
            {
                isMoving = true;
            }




        }    

        if(destiny != null)
        {
            if (transform.position.x < destiny.transform.position.x)
            {
                minionAnimationScriptRef.SetSpriteFlip(false);
            }
            else if (transform.position.x > destiny.transform.position.x)
            {
                minionAnimationScriptRef.SetSpriteFlip(true);
            }
        }
    }
}

