using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoatController : GameElement
{
    public enum State { Free, Busy };

    [HideInInspector]
    public State state;

    [HideInInspector]
    public GameObject island;

    public UnityEvent ReachedIsland;

    private GameObject destination;
    private float speed;
    
    void Start()
    {
        Init();
        state = State.Free;
        island = null;
        if (ReachedIsland == null)
        {
            ReachedIsland = new UnityEvent();
        }
        destination = null;
        speed = 0.05F;
    }

    void Update()
    {
        if (state == State.Free) UpdateFree();
        else UpdateBusy();
    }

    public void SailTo(GameObject obj)
    {
        state = State.Busy;
        transform.parent = null;
        destination = obj;
    }

    void UpdateFree()
    {
        if (island == null)
        {
            GameObject[] islands = GameObject.FindGameObjectsWithTag("Island");
            foreach (GameObject obj in islands)
            {
                if (Vector3.Distance(transform.position, obj.transform.position) < 0.1F)
                {
                    island = obj;
                    game_data.story_log.AddMessage("The boat reached " + island.name + ".");
                    ReachedIsland.Invoke();
                    break;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, island.transform.position) > 0.15F)
            {
                game_data.story_log.AddMessage("The boat is somewhere on the water.");
                island = null;
            }
        }
    }

    void UpdateBusy()
    {
        if (Vector3.Distance(transform.position, destination.transform.position) < 0.1F)
        {
            ReachedIsland.Invoke();
            state = State.Free;
            island = destination;
            destination = null;
        } else
        {
            Vector3 dir = (destination.transform.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
        }
    }
}
