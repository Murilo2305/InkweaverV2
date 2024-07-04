using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirectionalIndicator : MonoBehaviour
{
    [SerializeField] GameObject playerRef;
    [SerializeField] Vector3 movement;
    [SerializeField] private GameObject Dash;
    [SerializeField] private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement = playerRef.GetComponent<PlayerMovement>().movement;
        //print(movement);
        if (movement != new Vector3 (0, 0, 0))
        {
            //test thing i pulled from here https://discussions.unity.com/t/make-the-player-face-his-movement-direction/118568
            //from what i understood, Look.Rotation(Vector) makes the rotation of the gameObject face where the vector is pointing
            transform.rotation = Quaternion.LookRotation(movement);
            //print(Quaternion.LookRotation(movement));

            Dash.transform.position = transform.TransformPoint(pos);
        }
    }
}