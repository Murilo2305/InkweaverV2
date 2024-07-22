using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaintballScript : MonoBehaviour
{
    [Header(" - Projectile Parameters")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float lifespan = 1.0f;

    [Header(" - Refs")]
    [SerializeField] private Animator animatorRef;

    [Header(" - Debug")]
    [SerializeField] private float despawnTimer = 0.0f;
    [SerializeField] private bool canMove = true;

    private void Start()
    {
        despawnTimer = lifespan;
    }
    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0.0f)
        {
            Splash();
        }

        if (canMove)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }




    public void Splash()
    {
        canMove = false;
        animatorRef.SetTrigger("Splash");
        Destroy(gameObject, 0.3f);
    }
}
