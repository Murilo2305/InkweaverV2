using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 40.0f;
    [SerializeField] private float horizontalmovement;
    [SerializeField] private float verticalmovement;
    [SerializeField] public Vector3 movement;
    [SerializeField] private Transform DITransform;
    [SerializeField] private Vector3 movespeed;
    [SerializeField] private Transform dash;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool collision;

    // directions every 45 degrees going clockwise
    // 0 - north (0*45 = 0), 1 - northeast(1*45  = 45), 2 - east(2*45 = 90), 3 - southeast(3*45 = 135), etc
    //public byte facing;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //M�todo Raycast para detectar colis�es (obsoleto)
        //tirado de https://stackoverflow.com/questions/73059401/how-to-make-collision-work-with-translate-unity
        //Usando esse m�todo principalmente pois eu estou usando o m�todo transform.Translate para movimentar ao inv�s de um m�todo que use f�sica

        //Ray ray = new Ray(transform.position, DITransform.forward);

        PlayerMove();
    }

    private void FixedUpdate()
    {
        movespeed = gameObject.GetComponent<Rigidbody>().velocity;
        gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + (movement*Time.deltaTime*speed));

        if(Input.GetButtonDown("Jump"))
        {

            StartCoroutine ("Dash");

        }
    }


    void PlayerMove()
    {
        horizontalmovement = Input.GetAxisRaw("Horizontal");
        verticalmovement = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontalmovement, 0.0f, verticalmovement);

        //Space.World fixes what were one of the main roadblocks on my other code, I tried to put the code in the player mesh to fix the fact that moving and rotating the player would
        //mess with the final direction the player was moving in, so Space.World actually makes that not happen
        //transform.Translate(movement * Time.deltaTime * speed, Space.World);
    }

    IEnumerator Dash()
    {

        rb.velocity = new Vector3 (dash.transform.position.x,dash.transform.position.y,dash.transform.position.z) * 3.0f;
        yield return new WaitForSeconds (0.5f);
        rb.velocity = new Vector3 (0.0f,0.0f,0.0f);

    }

}
