using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IslandElement : GameElement
{
    [HideInInspector]
    public Transform[] ports;

    [HideInInspector]
    public Transform[] spots;
    [HideInInspector]
    public bool[] available;

    [HideInInspector]
    public List<CharacterElement> on_island;

    [HideInInspector]
    public bool has_boat;

    protected virtual void Init(string target_name)
    {
        base.Init(target_name);
        List<Transform> lst_ports = new List<Transform>();
        List<Transform> lst_spots = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Port"))
            {
                lst_ports.Add(child);
            }
            else if (child.gameObject.CompareTag("Spot"))
            {
                lst_spots.Add(child);
            }
        }
        ports = lst_ports.ToArray();
        spots = lst_spots.ToArray();
        available = new bool[spots.Length];
        for (int i = 0; i < available.Length; i++)
        {
            available[i] = true;
        }
        on_island = new List<CharacterElement>();
        has_boat = false;
    }

    public void OnBoatReachIsland()
    {
        // check if island is this one
        // if yes, update data
    }
}
