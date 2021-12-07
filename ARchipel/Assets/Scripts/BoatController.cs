using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoatController : GameElement
{
    private GameObject island;

    public UnityEvent ReachedIsland;
    
    void Start()
    {
        Init();
        island = null;
        if (ReachedIsland == null)
        {
            ReachedIsland = new UnityEvent();
        }
    }

    void Update()
    {
        if (island == null)
        {
            GameObject[] islands = GameObject.FindGameObjectsWithTag("Island");
            foreach (GameObject obj in islands)
            {
                if (Vector3.Distance(transform.position, obj.transform.position) < 0.1F)
                {
                    island = obj;
                    story_log.AddMessage("The boat reached " + island.name + ".");
                    ReachedIsland.Invoke();
                    break;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, island.transform.position) > 0.15F)
            {
                story_log.AddMessage("The boat is somewhere on the water.");
                island = null;
            }
        }
    }

    public GameObject GetIsland()
    {
        return island;
    }
}
