using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sniper_script : MonoBehaviour
{

    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject PlayerDetector;
    [SerializeField] GameObject Player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float ShotDistance;
    [SerializeField] private float BulletSpawnHeightOffset;
    [SerializeField] private SniperAnimationScript sniperAnimationScriptRef;
    [SerializeField] private float repositionDelay = 0.5f;
    [SerializeField] private float MinimumCooldownBetweenShots;
    [SerializeField] private bool canShoot;
    private bool once = true;

    public float Distance; 
    public bool IsShooting = false;
    public bool IsRepositioning = false;
    public bool canMove;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerDetector = gameObject.transform.GetChild(2).gameObject;
        agent = gameObject.GetComponent<NavMeshAgent>();
        sniperAnimationScriptRef = gameObject.transform.GetChild(0).GetComponent<SniperAnimationScript>();
    }
    void Update()
    {


        Distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);

        if (PlayerDetector.GetComponent<enemy_player_detection>().hasSeenPlayer == true && once == true)
        {

            agent.SetDestination(Player.transform.position);
            once = false;

        }

        if (Distance <= ShotDistance && once == false)
        {
            if (canShoot)
            {
                StartCoroutine(shoot());
                MinimumCooldownBetweenShots = 3.0f;
                canShoot = false;
            }
        }
        else if (Distance > ShotDistance && once == false && !IsShooting)
        {


            StartCoroutine(Repositioning());

        }

        if (!IsShooting && MinimumCooldownBetweenShots > 0)
        {
            MinimumCooldownBetweenShots -= Time.deltaTime;
        }
        if (MinimumCooldownBetweenShots <= 0 && !canShoot)
        {
            canShoot = true;
        }

        if (!canMove)
        {
            agent.velocity = Vector3.zero;
        }
    }

    IEnumerator shoot()
    {
        if(IsShooting == false)
        {
            sniperAnimationScriptRef.SetAnimatorTrigger("triggerShoot");

            //start aiming
            IsShooting = true;
            print("aiming");
            canMove = false;
            agent.SetDestination(gameObject.transform.position);
            
            //shot called by an event in the shoot animation
            
            //wait and reposition
            

            yield return new WaitForSeconds (1.1f);
            canMove = true;
            StartCoroutine(Repositioning());

        }

    }

    IEnumerator Repositioning()
    {
        //print("Repositioning");
        yield return new WaitForSeconds(repositionDelay);
        agent.SetDestination(Player.transform.position);
        IsShooting = false;

    }


    public void AnimationEventShoot()
    {
        //print("shoot");
        Instantiate(Bullet, new Vector3(transform.position.x, (transform.position.y + BulletSpawnHeightOffset), transform.position.z), transform.rotation);
    }

    public void InterruptAttack() 
    {
        StopAllCoroutines();
        IsShooting = false;
        MinimumCooldownBetweenShots = 2.0f;
    }
}
