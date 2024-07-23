using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sniper_script : MonoBehaviour
{

    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject PlayerDetector;
    [SerializeField] GameObject Player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float Delay;
    [SerializeField] float ShotDistance;
    public float Distance; 
    private bool once = true;
    public bool IsShooting = false;

    void Start()
    {
        
    }
    void Update()
    {

        Player = GameObject.FindGameObjectWithTag("Player");

        Distance = Vector3.Distance(gameObject.transform.position,Player.transform.position);

        if(PlayerDetector.GetComponent<enemy_player_detection>().hasSeenPlayer == true && once == true)
        {

            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
            once = false;

        }

        if(Distance <= ShotDistance && once == false)
        {
            
            
            StartCoroutine("shoot");

        }

    }

    IEnumerator shoot()
    {
        if(IsShooting == false)
        {
            IsShooting = true;
            print("shoot");
            agent.SetDestination(gameObject.transform.position);
            yield return new WaitForSeconds(Delay);
            Instantiate(Bullet,transform.position,transform.rotation);
            yield return new WaitForSeconds (0.5f);
            StartCoroutine("Repositioning");

        }

    }

    IEnumerator Repositioning()
    {

        print("Repositioning");
        yield return new WaitForSeconds(1.5f);
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        IsShooting = false;

    }

}
