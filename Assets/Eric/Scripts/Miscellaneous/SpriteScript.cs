using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

        transform.rotation =new Quaternion(0.0f, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z, Camera.main.transform.rotation.w);
            
        
    }
}
