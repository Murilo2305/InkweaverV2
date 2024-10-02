using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake_Script : MonoBehaviour
{

    [SerializeField] GameObject Cam;
    [SerializeField] Animator CamAnim;

    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
        CamAnim = Cam.GetComponent<Animator>();
    }

    public void Shake()
    {

        int a = Random.Range(1,4);

        if(a == 1)
        {

            CamAnim.SetTrigger("Shake1");

        }else if(a == 2)
        {

            CamAnim.SetTrigger("Shake2");

        }else if(a == 3)
        {

            CamAnim.SetTrigger("Shake3");

        }

    }
}
