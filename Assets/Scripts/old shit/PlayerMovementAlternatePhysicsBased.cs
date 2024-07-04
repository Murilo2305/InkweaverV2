using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAlternatePhysicsBased : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] Rigidbody rb;

    //private bool isMovingLeft;
    //private bool isMovingRight;
    //private bool isMovingUp;
    //private bool isMovingDown;
    private bool isMovingVertical;
    private bool isMovingHorizontal;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            isMovingVertical = true;
        }
        else
        {
            isMovingVertical = false;
        }
        if(!isMovingVertical)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0.0f);
        }
    }

    private void FixedUpdate()
    {
        if (isMovingVertical)
        {
            VerticalMovement();
        }
        
    }

    private void VerticalMovement()
    {
        rb.AddForce(Vector3.forward * speed * Input.GetAxisRaw("Vertical"));
    }


}
