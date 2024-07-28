using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaScript : MonoBehaviour
{
    [SerializeField] float damagePerSecond = 10f;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCombatScript>().DamagePlayer(damagePerSecond * Time.deltaTime);
        }
    }
}
