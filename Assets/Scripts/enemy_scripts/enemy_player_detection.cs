using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_player_detection : MonoBehaviour
{

    public bool hasSeenPlayer;
    public Vector3 playerRef;
    public GameObject PLAYER;
    public bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        hasSeenPlayer = false;
        playerRef = GameObject.FindGameObjectWithTag("Player").transform.position;
        PLAYER = GameObject.FindGameObjectWithTag("Player");

        hasSeenPlayer = true;


        GetComponent<BoxCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        isMoving = hasSeenPlayer;

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {

            if (hasSeenPlayer == false)
            {

                hasSeenPlayer = true;

            }

        }

    }

}
