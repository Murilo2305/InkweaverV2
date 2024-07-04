using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererDebugThing : MonoBehaviour
{

    public Transform target;
    public LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, target.position);
    }
}
