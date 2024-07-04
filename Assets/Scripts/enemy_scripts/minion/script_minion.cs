using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_minion : MonoBehaviour
{
    
    [SerializeField] GameObject DamageRadius;
    public bool Attacking;
    [SerializeField] float AttackDelay;
    GameObject target;
    bool startAttackk;
    bool delay;
    
    

    void Start()
    {

        Attacking = GetComponent<enemy_hit_and_Damage>().isAttacking;
        delay = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        startAttackk = DamageRadius.GetComponent<enemy_damage_radius>().startAttack;

        if(startAttackk == true && delay == true)
        {

            StartCoroutine("Attack");

        }

    }


    IEnumerator Attack()
    {

        if (Attacking == true)
        {

            delay = false;
            yield return new WaitForSeconds(AttackDelay);

            if(Attacking == true )
            {

                print("A");
                GetComponent<enemy_damage_radius>().startAttack = false;
                delay = true;

            }else
            {

                delay= true;

            }

        }

    }

}

