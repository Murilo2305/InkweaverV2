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

    [Space]

    public AudioSource paintBallAudioSourceRef;

    private bool SFXControlVariable;

    private void Start()
    {
        despawnTimer = lifespan;
        SFXControlVariable = false;
    }
    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0.0f)
        {
            if(SFXControlVariable == false)
            {
                SFXControlVariable = true;
                paintBallAudioSourceRef.Play();
            }
            Splash();
            
        }

        if (canMove)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }




    public void Splash()
    {
        
            SFXControlVariable = true;
            canMove = false;
            animatorRef.SetTrigger("Splash");

            Destroy(gameObject, 0.35f);
           
        
    }
}
