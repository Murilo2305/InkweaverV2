using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_System_Script : MonoBehaviour
{

    [SerializeField] GameObject PauseSystem;

    private void Update()
    {
        if (PauseSystem.activeSelf)
        {
            if (Input.GetButtonDown("PauseButton"))
            {
                Resume();    
            }
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
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
