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
    public bool StageCleared;

    // Start is called before the first frame update

    void Start()
    {
        Instantiate(PlayerRef,PlayerStartPos,PlayerRef.transform.rotation);
        enemySpawnerScriptRef.SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length > 0)
        {

            StageCleared = false;

        }else
        {

            StageCleared = true;

        }

    }

}

