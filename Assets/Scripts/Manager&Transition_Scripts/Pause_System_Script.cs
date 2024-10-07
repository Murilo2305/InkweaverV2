using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_System_Script : MonoBehaviour
{

    [SerializeField] GameObject PauseSystem;

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

    public void Again()
    {

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }

    public void Restart()
    {

        SceneManager.LoadScene("Tutorial_Stage1");

    }

}
