using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition_Script : MonoBehaviour
{

    [SerializeField] GameObject GameManager;
    public string NextScene;
    [SerializeField] bool CanGoOn;

    [SerializeField] private SpriteRenderer ActiveSprite, DeactiveSprite;

    private void Start()
    {
        ActiveSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        DeactiveSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        CanGoOn = GameManager.GetComponent<GameManager_Script>().StageCleared;

        if (CanGoOn)
        {
            ActiveSprite.enabled = true;
            DeactiveSprite.enabled = false;
        }
        else
        {
            ActiveSprite.enabled = false;
            DeactiveSprite.enabled = true;
        }

    }

    void OnTriggerEnter (Collider other)
    {
        
        if(CanGoOn == true)
        {

            if(other.gameObject.tag == "Player")
            {

                SceneManager.LoadScene(NextScene);
                print("next Stage");

            }

        }

    }
}
