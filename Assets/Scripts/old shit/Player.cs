using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 40.0f;
    [SerializeField] private float horizontalmovement;
    [SerializeField] private float verticalmovement;
    [SerializeField] public Vector3 movement;

    // directions every 45 degrees going clockwise
    // 0 - north (0*45 = 0), 1 - northeast(1*45  = 45), 2 - east(2*45 = 90), 3 - southeast(3*45 = 135), etc
    //public byte facing;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();



    }

    void PlayerMovement()
    {
        horizontalmovement = Input.GetAxisRaw("Horizontal");
        verticalmovement = Input.GetAxisRaw("Vertical");
        movement = new Vector3(horizontalmovement, 0.0f, verticalmovement);

        //test thing i pulled from here https://discussions.unity.com/t/make-the-player-face-his-movement-direction/118568
        //from what i understood, Look.Rotation(Vector) makes the rotation of the gameObject face where the vector is pointing
        //transform.rotation = Quaternion.LookRotation(movement);

        //Space.World fixes what were one of the main roadblocks on my other code, I tried to put the code in the player mesh to fix the fact that moving and rotating the player would
        //mess with the final direction the player was moving in, so Space.World actually makes that not happen
       transform.Translate(movement * Time.deltaTime * speed, Space.World);


    }

}
