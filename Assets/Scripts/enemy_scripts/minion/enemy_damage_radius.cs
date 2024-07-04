using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_damage_radius : MonoBehaviour
{
    
    public GameObject Nav;
    public bool Attackingg, startAttack;
    [SerializeField] float delay;
    

    void Start()
    {
        
        Attackingg = false;
        startAttack = false;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other)
    {

        
        Attackingg = true;
        if (other.gameObject.tag == "Player" && Attackingg == true && startAttack == false)
        {

            print("b");
            StartCoroutine("Attack");

        }

        

    }

    private void OnTriggerStay (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            StartCoroutine("AttackAgain");


        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "player")
        {

            Attackingg = false;
        }

    }

    IEnumerator Attack()
    {
            Nav.GetComponent<NavMeshAgent>().SetDestination(transform.position);
            if(Attackingg == true)
            {

                startAttack = true;
                yield return new WaitForSeconds(delay);
                if(Attackingg == true)
                {

                    print ("a");
                    startAttack = false;

                }


            }

    }

    IEnumerator AttackAgain()
    {

        yield return new WaitForSeconds(delay);

        if(startAttack == false)
        {

            StartCoroutine("Attack");

        }
        

    }

}
