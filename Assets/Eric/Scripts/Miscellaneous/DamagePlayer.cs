using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject player;

    private bool hasDamagedPlayer = false;

    void Update()
    {
       if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        else
        {
            if (!hasDamagedPlayer)
            {
                player.GetComponent<PlayerCombatScript>().DamagePlayer(damage);
                hasDamagedPlayer = true;
            }
        }

    }
}
