using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_System_Script : MonoBehaviour
{

    [SerializeField] GameObject PauseSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume()
    {

        PauseSystem.SetActive(false);
        Time.timeScale = 1.0f;

    }
}
