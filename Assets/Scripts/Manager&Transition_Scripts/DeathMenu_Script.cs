using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu_Script : MonoBehaviour
{
    [SerializeField] AudioSource BGMRef;

    
    private void OnDisable()
    {
        BGMRef.pitch = 1f;
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;

        BGMRef.pitch = 0.6f;
    }

    public void retry()
    {

        string currentSceneName = SceneManager.GetActiveScene().name;

        BGMRef.gameObject.GetComponent<BackgroundMusicScript>().oldScene = SceneManager.GetActiveScene().buildIndex;

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

    public void SetupDeathCanvasMusic()
    {
        BGMRef = GameObject.FindWithTag("BackgroundMusic").GetComponent<AudioSource>();
        print("sigma");
    }
}
