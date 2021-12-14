using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IslandElement : PlatformElement
{
    [HideInInspector]
    public Transform[] ports;

    //[HideInInspector]
    public bool has_boat
    {
        get
        {
            return game_data.boat_ctrl.island.name == name;
        }
    }

    

    protected virtual void Init()
    {
        base.Init();
        List<Transform> lst_ports = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Port"))
            {
                lst_ports.Add(child);
            }
        }
        ports = lst_ports.ToArray();
        //has_boat = false;
    }

    public virtual void OnBoatReachIsland()
    {
        // check if island is this one
        // if yes, update data
        //if (game_data.boat_ctrl.island.name == name)
        //{
            //has_boat = true;
        //}
    }
}
