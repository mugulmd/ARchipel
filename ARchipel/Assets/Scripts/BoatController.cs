using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : GameElement
{
    private GameObject island;
    
    void Start()
    {
        Init();
        island = null;
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
                    story_log.AddMessage("The boat now belongs to " + island.name + ".");
                    break;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, island.transform.position) > 0.15F)
            {
                story_log.AddMessage("The boat on " + island.name + " is now vacant.");
                island = null;
            }
        }
    }
}
