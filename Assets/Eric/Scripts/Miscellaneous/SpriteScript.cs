using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScript : MonoBehaviour
{
    /*
    private void Start()
    {
        transform.rotation = Quaternion.Euler(12, 0, 0);
    }
    */
    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
