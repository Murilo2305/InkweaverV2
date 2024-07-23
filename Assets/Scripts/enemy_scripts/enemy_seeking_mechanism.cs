using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navigation : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    public Transform destiny;
    [SerializeField] private GameObject PlayerDetector;
    private bool once;

    // Start is called before the first frame update
    void awake()
    {

        agent = GetComponent<NavMeshAgent>();
        once = true;

        
    }

    // Update is called once per frame
    void Update()
    {

        destiny = GameObject.FindGameObjectWithTag("Player").transform;

        if(PlayerDetector.GetComponent<enemy_player_detection>().hasSeenPlayer == true && once == true)
        {

            agent.SetDestination (destiny.transform.position);
            StartCoroutine("Seek");
            once = false;

        }    
    }

    IEnumerator Seek()
    {

        agent.SetDestination (destiny.transform.position);
        yield return null;

    }

}