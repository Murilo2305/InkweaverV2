using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_damage_radius : MonoBehaviour
{
    
    public GameObject Nav;
    public bool Attackingg, startAttack;
    [SerializeField] float delay;
    private bool player = false;
    [SerializeField] GameObject PlayerDetector;
    private GameObject PlayerRef;
    

    void Start()
    {
        
        Attackingg = false;
        startAttack = false;


    }

    // Update is called once per frame
    void Update()
    {
        
        if (player == true && Attackingg == false)
        {

            StartCoroutine("Attack");
            Attackingg = true;

        }

    }

    private void OnTriggerEnter (Collider other)
    {

        if(other.gameObject.tag == "Player")
        {

            player = true;

        }      

    }

    
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            player = false;
        
        }

    }

    IEnumerator Attack()
    {
            
        PlayerRef = PlayerDetector.GetComponent<enemy_player_detection>().PLAYER;
        print("a");
        yield return new WaitForSeconds(delay);
        Attackingg = false;

    }



}
