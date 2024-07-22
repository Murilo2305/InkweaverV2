using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class PlayerColorSystem : MonoBehaviour
{

    [Header(" - Active color")]
    public bool red;
    public bool green;
    public bool blue;

    [Header(" - Colorburst parameteres")]
    [SerializeField] private float cooldown;

    [Header(" - References")]
    [SerializeField] private RectTransform UIColorwheelRef;

    [Header(" - Rotatation Values")]
    [SerializeField] int rotationController;
    [SerializeField] float rotationTarget;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SwitchColor();
        }
    }


    private void SwitchColor()
    {
        if (red)
        {
            red = false;
            green = true;
            rotationTarget = 240f;
        }
        else if (green)
        {
            green = false;
            blue = true;
            rotationTarget = 0f;
        }
        else if (blue)
        {
            blue = false;
            red = true;
            rotationTarget = 120f;
        }
        else
        {
            red = true;
            rotationTarget = 120f;
        }
        StartRotationCoroutine();
    }

    private void StartRotationCoroutine()
    {
        float i = 0;
        StartCoroutine(RotationCoroutine(i));
    }

    private IEnumerator RotationCoroutine(float i)
    {
        i += 0.1f;

        if (rotationTarget == 0.0f)
        {
            UIColorwheelRef.Rotate(0, 0, Mathf.Lerp(UIColorwheelRef.rotation.eulerAngles.z, 360f, i) - UIColorwheelRef.rotation.eulerAngles.z);
        }
        else
        {
            UIColorwheelRef.Rotate(0, 0, Mathf.Lerp(UIColorwheelRef.rotation.eulerAngles.z, rotationTarget, i) - UIColorwheelRef.rotation.eulerAngles.z);
        }



        yield return new WaitForSeconds(0.01f);
        if (i < 1)
        {
            StartCoroutine(RotationCoroutine(i));   
        }
    }
}
