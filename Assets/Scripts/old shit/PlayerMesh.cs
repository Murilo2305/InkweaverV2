using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour
{

    // Update is called once per frame
    //void Update()
    //{
        //Improvised code i tried to make to make player face the direction it was facing before looking up how to properly do it
        //I guess it works but not sure how well it does so
        //found a solution on the forums. for more details look at Player script

//        if (Input.GetAxisRaw("Horizontal") == 1)
//        {
//            if (Input.GetAxisRaw("Vertical") == 1) 
//            {
//                gameObject.transform.rotation = Quaternion.Euler(0, 45, 0);
//            }
//            else if (Input.GetAxisRaw("Vertical") == -1) 
//            {
//               gameObject.transform.rotation = Quaternion.Euler(0, 135, 0);
//            }
//            else gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
//        }
//        else if (Input.GetAxisRaw("Horizontal") == -1) 
//        {
//            if (Input.GetAxisRaw("Vertical") == 1)
//            {
//                gameObject.transform.rotation = Quaternion.Euler(0, 315, 0);
//            }
//            else if (Input.GetAxisRaw("Vertical") == -1)
//            {
//                gameObject.transform.rotation = Quaternion.Euler(0, 225, 0);
//            }
//            else gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
//        }
//        else if (Input.GetAxisRaw("Vertical") == 1)
//        {
//            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
//        }
//        else
//        {
//            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
//        }
    //}
}
