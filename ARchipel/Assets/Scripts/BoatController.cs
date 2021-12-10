using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoatController : GameElement
{
    [HideInInspector]
    public bool has_passenger;
    [HideInInspector]
    public List<CharacterElement> passengers;

    [HideInInspector]
    public bool at_dock;
    [HideInInspector]
    public IslandElement island;
    [HideInInspector]
    public int port_idx;

    public enum State { Adrift, PreparingTrip, OnTrip, EndingTrip };
    [HideInInspector]
    public State state;

    private IslandElement destination;
    public float speed;

    public UnityEvent ReachedIsland;

    void Start()
    {
        Init("Target Boat");
        has_passenger = false;
        passengers = new List<CharacterElement>();
        at_dock = false;
        island = null;
        port_idx = -1;
        state = State.Adrift;
        destination = null;
        speed = 0.05F;
        if (ReachedIsland == null)
        {
            ReachedIsland = new UnityEvent();
        }
    }

    void Update()
    {
        if (state == State.Adrift)
        {
            // if at dock
            // check if still close enough to port
            // if not, check also if close enough to another port of same island

            // if not at dock
            // try to detect an island by searching through ports
            // if hit detected
            // update data and trigger reached island event
        }
        else if (state == State.OnTrip)
        {
            // move towards destination

            // if close enough to one of destination island's ports
            // update data and trigger reached island event
        }
    }

    public void WaitForPassenger(CharacterElement elt)
    {
        // TODO
    }

    public void TakePassenger(CharacterElement elt)
    {
        // TODO
    }

    public void SailTo(IslandElement elt)
    {
        // TODO
    }

    public void ReleasePassengers()
    {
        // TODO
    }
}
