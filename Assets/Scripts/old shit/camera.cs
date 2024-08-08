using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private Quaternion rot = Quaternion.Euler(40, 0, 0);
    public GameObject playerref;
    private Vector3 pos;

    //Adicionado pelo Murilo
    void Start()
    {

        playerref = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if(playerref == null)
        {
            playerref = GameObject.FindGameObjectWithTag("Player");
        }

        pos = new Vector3(playerref.transform.position.x, playerref.transform.position.y + 6.78f, playerref.transform.position.z - 8.28f);
        transform.SetPositionAndRotation(pos, rot);
    }
}
