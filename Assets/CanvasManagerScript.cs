using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagerScript : MonoBehaviour
{
    [SerializeField] DeathMenu_Script DeathMenuRef;
    void Start()
    {
        DeathMenuRef = gameObject.transform.GetChild(2).GetChild(0).GetComponent<DeathMenu_Script>();

        DeathMenuRef.SetupDeathCanvasMusic();
    }
}
