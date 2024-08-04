using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{

    [SerializeField] private Vector3 player;
    [SerializeField] private float lifespan = 1.0f;
    [SerializeField] private float timer;

    [SerializeField] private float Speed;

    // Start is called before the first frame update
    void Start()
    {
        timer = lifespan;
        player = GameObject.FindGameObjectWithTag("Player").transform.position;

        transform.LookAt(new Vector3(player.x, transform.position.y, player.z));
    }

    // Update is called once per frame
    void Update()
    {

        // transform.position = Vector3.MoveTowards(transform.position,new Vector3(player.x, transform.position.y, player.z),Speed * Time.deltaTime);

        transform.Translate(Vector3.forward * Speed *Time.deltaTime);


        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter (Collider  other)
    {

        if(other.gameObject.tag == "Player")
        {
            if (!other.gameObject.GetComponent<PlayerCombatScript>().isInvulnerable)
            {
                Destroy(gameObject);
            }
        }

    }

}
