using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerDirectionalIndicator : MonoBehaviour
{
    [SerializeField] GameObject playerRef;
    [SerializeField] Vector3 movement;
    [SerializeField] private GameObject Dash;
    [SerializeField] private float dashPosOffset;

    // Start is called before the first frame update
    void Start()
    {
        Dash.transform.position = playerRef.transform.position + (new Vector3(0.0f, 0.0f, 1.0f) * dashPosOffset);
    }

    // Update is called once per frame
    void Update()
    {
        movement = playerRef.GetComponent<PlayerCharacterControlerMovement>().movement;

        if (movement != new Vector3(0, 0, 0) && !playerRef.GetComponent<PlayerCharacterControlerMovement>().isDashing)
        {
            //test thing i pulled from here https://discussions.unity.com/t/make-the-player-face-his-movement-direction/118568
            //from what i understood, Look.Rotation(Vector) makes the rotation of the gameObject face where the vector is pointing
            //print(Quaternion.LookRotation(movement));

            transform.rotation = Quaternion.LookRotation(movement);

            Dash.transform.position = playerRef.transform.position + (movement * dashPosOffset);

        }
    }
}
