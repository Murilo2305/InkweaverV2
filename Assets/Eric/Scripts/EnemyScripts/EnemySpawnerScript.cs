using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [Header(" - Refs")]
    [SerializeField] GameObject MinionPrefab;
    [SerializeField] GameObject SniperPrefab;
    [SerializeField] GameObject BullPrefab;
    [SerializeField] GameObject DummyPrefab;
    [Header(" - Enemies")]
    [SerializeField] List<GameObject> EnemyPosRefs;
    [Header(" - Debug stuff")]
    public GameObject playerRef;
    public Canvas WorldSpaceCanvas;

    void Start()
    {
        
    }

    private void SpawnEnemiesInEnemyPosRef()
    {
        for (int i = 0; i < EnemyPosRefs.Count; i++)
        {
            SpawnEnemy(i);
            //print(i);
        }
    }


    void SpawnEnemy(int i)
    {
        string enemyType;
        GameObject enemyInstance;

        if (EnemyPosRefs[i].gameObject.GetComponent<EnemySpawnPosRefScript>().enemyType.ToString().ToLower().Equals("melee"))
        {
            enemyType = "Minion";
            enemyInstance = Instantiate(MinionPrefab, EnemyPosRefs[i].transform);
        }
        else if (EnemyPosRefs[i].gameObject.GetComponent<EnemySpawnPosRefScript>().enemyType.ToString().ToLower().Equals("contactdamage"))
        {
            enemyType = "Bull";
            enemyInstance = Instantiate(BullPrefab, EnemyPosRefs[i].transform);
        }
        else if (EnemyPosRefs[i].gameObject.GetComponent<EnemySpawnPosRefScript>().enemyType.ToString().ToLower().Equals("rangedprojectile"))
        {
            enemyType = "Sniper";
            enemyInstance = Instantiate(SniperPrefab, EnemyPosRefs[i].transform);
        }
        else
        {
            enemyType = "Dummy";
            enemyInstance = Instantiate(DummyPrefab, EnemyPosRefs[i].transform);
        }

        enemyInstance.GetComponent<EnemyCombatScript>().SetupHealthBar(WorldSpaceCanvas);
        enemyInstance.GetComponent<EnemyCombatScript>().playerRef = playerRef;
        enemyInstance.name = enemyType + (i + 1);
        enemyInstance.transform.SetParent(null);
    }

    public void SpawnEnemies()
    {
        EnemyPosRefs.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawnPositionRef"));
      
            
        playerRef = GameObject.FindGameObjectWithTag("Player");
        WorldSpaceCanvas = GameObject.FindGameObjectWithTag("CanvasParentObject").transform.GetChild(0).GetComponent<Canvas>();

        SpawnEnemiesInEnemyPosRef();
        
    }
}
