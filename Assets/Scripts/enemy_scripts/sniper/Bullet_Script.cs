using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{

    private Vector3 player;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position,player,Speed * Time.deltaTime);
        if(transform.position == player)
        {

            Destroy(gameObject);
            
        }

    }

    void OnTriggerEnter (Collider  other)
    {

        if(other.gameObject.tag == "Player")
        {

            print('a');

        }

    }

}
