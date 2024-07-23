using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bull_script : MonoBehaviour
{  

    public bool SeenPlayer;
    public bool once;
    public bool att;
    [SerializeField] float delay1,delay2;
    [SerializeField] private NavMeshAgent agent;
    private bool Attacking = false;
    [SerializeField] GameObject PlayerDetector;

    // Start is called before the first frame update

     void awake()
    {

        agent = GetComponent<NavMeshAgent>();

        
    }

    void Start()
    {
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        SeenPlayer = PlayerDetector.GetComponent<enemy_player_detection>().hasSeenPlayer;
        att = gameObject.GetComponent<enemy_hit_and_Damage>().isAttacking;
        
        if(SeenPlayer == true && once == true)
        {

            gameObject.GetComponent<enemy_hit_and_Damage>().isAttacking = true;
            once = false;

        }  
        
        if (SeenPlayer == true && att == true)
        {

            StartCoroutine("bullRampage");
            gameObject.GetComponent<enemy_hit_and_Damage>().isAttacking = false;

        } 
    }

    IEnumerator bullRampage()
    {

        agent.SetDestination(transform.position);

        yield return new WaitForSeconds(delay1);

        agent.SetDestination(gameObject.GetComponent<navigation>().destiny.transform.position);

        Attacking = true;

        yield return new WaitForSeconds(delay2);

        gameObject.GetComponent<enemy_hit_and_Damage>().isAttacking = true;

        Attacking = false;

    }

    void OnTriggerEnter (Collider other)
    {
        if (Attacking == true)
        {
            if(other.gameObject.tag == "Player")
            {

                print("a");

            }
        }

    }
}
