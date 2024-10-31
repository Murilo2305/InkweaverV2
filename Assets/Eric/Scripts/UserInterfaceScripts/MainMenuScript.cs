using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject PauseSystem;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {

        SceneManager.LoadScene("Tutorial_Stage1");

    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
