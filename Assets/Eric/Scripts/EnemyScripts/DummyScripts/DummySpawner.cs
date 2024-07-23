using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySpawner : MonoBehaviour
{
    [Header(" - Refs")]
    [SerializeField] GameObject DummyPrefab;
    [SerializeField] GameObject playerRef;
    [SerializeField] Canvas WorldSpaceCanvas;
    [Header(" - Enemies")]
    [SerializeField] Transform[] DummyPosRefs;



    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < DummyPosRefs.Length; i++)
        {
            SpawnDummy(i);
        }
    }

    void SpawnDummy(int i)
    {
        GameObject dummyInstance = Instantiate(DummyPrefab, DummyPosRefs[i]);
        dummyInstance.GetComponent<EnemyCombatScript>().SetupHealthBar(WorldSpaceCanvas);
        dummyInstance.GetComponent<EnemyCombatScript>().playerRef = playerRef;
        dummyInstance.name = "Dummy" + i;
        dummyInstance.transform.SetParent(null);
    }
}
