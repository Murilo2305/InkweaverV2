using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition_Script : MonoBehaviour
{

    [SerializeField] GameObject GameManager;
    public string NextScene;
    [SerializeField] bool CanGoOn;

    // Start is called before the first frame update
    void Start()
    {
        
        CanGoOn = GameManager.GetComponent<GameManager_Script>().StageCleared;

    }

    // Update is called once per frame
    void Update()
    {
        


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
