using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : GameElement
{
    private LineRenderer lr;

    private GameObject marker_left, marker_right;
    private GameObject island_left, island_right;

    void Start()
    {
        Init();

        lr = GetComponent<LineRenderer>();
        lr.enabled = false;

        marker_left = null;
        marker_right = null;
        island_left = null;
        island_right = null;
    }

    public void FoundLeft(GameObject marker)
    {
        marker_left = marker;
        Debug.Log("Detected left endpoint");
    }

    public void FoundRight(GameObject marker)
    {
        marker_right = marker;
        Debug.Log("Detected right endpoint");
    }

    void Update()
    {
        if (marker_left == null || marker_right == null)
            return;

        if (island_left == null)
        {
            GameObject[] islands = GameObject.FindGameObjectsWithTag("Island");
            foreach (GameObject island in islands)
            {
                if (Vector3.Distance(marker_left.transform.position, island.transform.position) < 0.1F)
                {
                    island_left = island;
                    Debug.Log("Detected left island");
                }
            }
        }
        if (island_right == null)
        {
            GameObject[] islands = GameObject.FindGameObjectsWithTag("Island");
            foreach (GameObject island in islands)
            {
                if (Vector3.Distance(marker_right.transform.position, island.transform.position) < 0.1F)
                {
                    island_right = island;
                    Debug.Log("Detected right island");
                }
            }
        }

        if (island_left == null || island_right == null)
            return;

        lr.enabled = true;
        lr.SetPosition(0, island_left.transform.position);
        lr.SetPosition(1, island_right.transform.position);
    }
}
