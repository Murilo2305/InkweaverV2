using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTofigureOutTheNameOfTheFaceButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            print("Joystick Button 0");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            print("Joystick Button 1");
        }
        if (Input.GetButtonDown("Fire3"))
        {
            print("Joystick Button 2");
        }
        if (Input.GetButtonDown("Jump"))
        {
            print("Joystick Button 3");
        }
    }
}
