using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    private LineRenderer lr;

    private bool found_start;
    private bool found_end;

    private Vector3 start;
    private Vector3 end;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;

        found_start = false;
        found_end = false;
    }

    public void FoundStart()
    {
        found_start = true;
        if (found_end)
            lr.enabled = true;
    }

    public void FoundEnd()
    {
        found_end = true;
        if(found_start)
        {
            lr.enabled = true;
        }
    }

    void Update()
    {
        start = GameObject.Find("Island_1").transform.position;
        end = GameObject.Find("Island_2").transform.position;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
