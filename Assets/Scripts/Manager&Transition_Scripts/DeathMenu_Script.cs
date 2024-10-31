using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu_Script : MonoBehaviour
{
    
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void retry()
    {

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);


    }

    public void ReturnToMainMenu()
    {

        SceneManager.LoadScene("TitleScreen");

    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
