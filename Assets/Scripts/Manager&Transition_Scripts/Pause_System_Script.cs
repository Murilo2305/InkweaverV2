using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;

    }

    public void Quit()
    {

        Application.Quit();
        print("a");

    }

    public void Restart()
    {

        SceneManager.LoadScene("Stage1_Segment1");

    }

}
