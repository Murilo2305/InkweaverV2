using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_player_detection : MonoBehaviour
{

    public bool hasSeenPlayer;
    GameObject playerRef;

    // Start is called before the first frame update
    void Start()
    {
        hasSeenPlayer = false;
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
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
