using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition_Script : MonoBehaviour
{

    [SerializeField] GameObject GameManager;
    public string NextScene;
    [SerializeField] bool CanGoOn;


    void Start()
    {
        
        CanGoOn = GameManager.GetComponent<GameManager_Script>().StageCleared;

    }

    void OnTriggerEnter (Collider other)
    {
        
        if(CanGoOn == true)
        {

            if(other.gameObject.tag == "Player")
            {

                SceneManager.LoadScene(NextScene);

            }

        }

    }
}
