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
        pos = new Vector3(playerref.transform.position.x, playerref.transform.position.y + 6, playerref.transform.position.z - 10);
        transform.SetPositionAndRotation(pos, rot);
    }
}
