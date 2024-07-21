using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySpawner : MonoBehaviour
{
    [SerializeField] GameObject DummyPrefab;
    [SerializeField] Transform DummyPosRef;
    [SerializeField] Canvas WorldSpaceCanvas;
    [SerializeField] GameObject playerRef;



    // Start is called before the first frame update
    void Start()
    {
        SpawnDummy();
    }

    void SpawnDummy()
    {
        GameObject dummyInstance = Instantiate(DummyPrefab, DummyPosRef);
        dummyInstance.GetComponent<EnemyCombatScript>().SetupHealthBar(WorldSpaceCanvas);
        dummyInstance.GetComponent<EnemyCombatScript>().playerRef = playerRef;
        dummyInstance.transform.SetParent(null);
    }
}
