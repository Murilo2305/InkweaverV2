using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Script : MonoBehaviour
{

    [SerializeField] GameObject PlayerRef;
    [SerializeField] Camera MainCamera;
    [SerializeField] Vector3 PlayerStartPos;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] EnemySpawnerScript enemySpawnerScriptRef;
    [SerializeField] GameObject PauseSystem;
    [SerializeField] GameObject DeathCanvas;
    public bool StageCleared;

    // Start is called before the first frame update

    void Awake()
    {

        Time.timeScale = 1.0f;

    }

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Instantiate(PlayerRef,PlayerStartPos,PlayerRef.transform.rotation);
        enemySpawnerScriptRef.SpawnEnemies();

        DeathCanvas = PauseSystem.transform.parent.parent.GetChild(2).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        print(Cursor.lockState);

        if (Input.GetButtonDown("PauseButton") && !DeathCanvas.activeInHierarchy)
        {

            PauseGame();

        }
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length > 0)
        {

            StageCleared = false;

        }else
        {

            StageCleared = true;

        }

    }


    private void PauseGame()
    {
        PauseSystem.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
}

