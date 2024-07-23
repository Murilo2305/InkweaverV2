using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class minion_seeking_mechanism : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform destiny;
    [SerializeField] private GameObject PlayerDetector;

    // Start is called before the first frame update
    void awake()
    {

        agent = GetComponent<NavMeshAgent>();

        
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerDetector.GetComponent<enemy_player_detection>().hasSeenPlayer == true)
        {

            destiny = GameObject.FindGameObjectWithTag("Player").transform;
            agent.SetDestination (destiny.transform.position);

        }    
    }
}

