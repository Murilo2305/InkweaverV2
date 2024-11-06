using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicScript : MonoBehaviour
{
    public static BackgroundMusicScript BGM;

    [SerializeField] AudioSource AudioSourceRef;

    [SerializeField] AudioClip MenuTheme, BattleTheme;
    
    public int oldScene;

    private void Awake()
    {
        if (BGM != null)
        {
            Destroy(gameObject);
        }
        else
        {
            BGM = this;
        }

        

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().buildIndex <= 5)
        {
            AudioSourceRef.clip = MenuTheme;

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                AudioSourceRef.Play();
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex > 5)
        {
            AudioSourceRef.clip = BattleTheme;

            if (SceneManager.GetActiveScene().buildIndex == 6 && oldScene != SceneManager.GetActiveScene().buildIndex)
            {
                AudioSourceRef.Play();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 13)
        {
            AudioSourceRef.loop = false;
        }
        else
        {
            AudioSourceRef.loop = true;
        }

     }

    private void Update()
    {
        if(Time.timeScale != 0)
        {
            AudioSourceRef.volume = 0.4f;
        }
        else if(Time.timeScale == 0)
        {
            AudioSourceRef.volume = 0.2f;
        }
    }

}
