using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorSystem : MonoBehaviour
{
    [Header(" - Active color")]
    public bool red;
    public bool green;
    public bool blue;
    [Header(" - Colorburst parameteres")]
    [SerializeField] private float cooldown;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (red)
            {
                red = false;
                green = true;
                print("Green");
            }
            else if (green)
            {
                green = false;
                blue = true;
                print("Blue");
            }
            else if (blue)
            {
                blue = false;
                red = true;
                print("Red");
            }
            else
            {
                red = true;
                print("Red");
            }
        }
    }
}
