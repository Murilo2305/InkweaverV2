using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInboundsReferencePointScript : MonoBehaviour
{
    [SerializeField] private GameObject playerRef;
    [SerializeField] private NavMeshAgent agentRef;

    private void Start()
    {
       

        agentRef = GetComponent<NavMeshAgent>();
        agentRef.Warp(Vector3.zero);
    }


    void Update()
    {
        if (playerRef == null)
        {
            playerRef = GameObject.FindWithTag("Player");
        }
        if (agentRef.destination != new Vector3(playerRef.transform.position.x, transform.position.y, playerRef.transform.position.z))
        {
            agentRef.destination = new Vector3(playerRef.transform.position.x, transform.position.y, playerRef.transform.position.z);
        }

    }
}
