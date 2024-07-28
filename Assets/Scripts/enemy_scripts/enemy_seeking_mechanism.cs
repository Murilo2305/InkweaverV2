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
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        once = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        destiny = GameObject.FindGameObjectWithTag("Player").transform;

        /*
        if(PlayerDetector.GetComponent<enemy_player_detection>().hasSeenPlayer == true && once == true)
        {
            

            agent.SetDestination (destiny.transform.position);


            Seek();
            once = false;

        }    
        */
    }

    private void Seek()
    {
        agent.SetDestination (new Vector3(destiny.transform.position.x, transform.position.y, destiny.transform.position.z));
    }

}