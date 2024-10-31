using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportBackToArena : MonoBehaviour
{
    [SerializeField] private Transform playerInboundsReferencePoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = playerInboundsReferencePoint.position;
            other.GetComponent<PlayerCharacterControlerMovement>().InterruptDash();
            other.GetComponent<PlayerCharacterControlerMovement>().playerIsOutOfBounds = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = playerInboundsReferencePoint.position;
            other.GetComponent<PlayerCharacterControlerMovement>().InterruptDash();
            other.GetComponent<PlayerCharacterControlerMovement>().playerIsOutOfBounds = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCharacterControlerMovement>().playerIsOutOfBounds = false;
        }
    }



}
