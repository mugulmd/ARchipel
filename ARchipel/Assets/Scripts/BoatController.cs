using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoatController : PlatformElement
{
    [HideInInspector]
    public bool at_dock;
    [HideInInspector]
    public IslandElement island;
    [HideInInspector]
    public int port_idx;

    public enum State { Adrift, PreparingTrip, OnTrip, EndingTrip };
    [HideInInspector]
    public State state;

    private int n_passengers_waiting;
    private bool destination_set;
    private IslandElement destination;
    private float speed;

    public UnityEvent ReachedIsland;

    void Start()
    {
        Init();
        at_dock = false;
        island = null;
        port_idx = -1;
        state = State.Adrift;
        n_passengers_waiting = 0;
        destination_set = false;
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
            // check if still close enough to island marker
            // if not at dock
            // try to detect an island by searching through their marker
            // if hit detected
            // update data and trigger reached island event
            if (at_dock)
            {
                if (Vector3.Distance(marker.transform.position, island.marker.transform.position) > 0.2F)
                {
                    at_dock = false;
                    island = null;
                    port_idx = -1;
                }
            } else
            {
                foreach (IslandElement elt in game_data.islands)
                {
                    if (Vector3.Distance(marker.transform.position, elt.marker.transform.position) < 0.15F)
                    {
                        at_dock = true;
                        island = elt;
                        float min_dist = 100.0F;
                        for (int i = 0; i < island.ports.Length; i++)
                        {
                            float dist = Vector3.Distance(transform.position, island.ports[i].position);
                            if (dist < min_dist)
                            {
                                min_dist = dist;
                                port_idx = i;
                            }
                        }
                        ReachedIsland.Invoke();
                        break;
                    }
                }
            }
        }
        else if (state == State.PreparingTrip)
        {
            // check if all passengers are on board
            // and if destination is set
            // and if destination exists
            // if yes then update state
            if (passengers.Count == n_passengers_waiting && destination_set && destination.exists)
            {
                state = State.OnTrip;
            }
        }
        else if (state == State.OnTrip)
        {
            // move towards destination
            // if close enough to one of destination island's ports
            // update data and trigger reached island event
            float min_dist = 100.0F;
            int closest_port_idx = -1;
            for (int i = 0; i < destination.ports.Length; i++)
            {
                Transform port = destination.ports[i];
                float dist = Vector3.Distance(transform.position, port.position);
                if (dist < min_dist)
                {
                    min_dist = dist;
                    closest_port_idx = i;
                }
            }
            if (min_dist < 0.01F)
            {
                at_dock = true;
                island = destination;
                port_idx = closest_port_idx;
                ReachedIsland.Invoke();
            }
            else if (closest_port_idx >= 0)
            {
                Vector3 dir = (destination.ports[closest_port_idx].position - transform.position).normalized;
                transform.position += dir * speed * Time.deltaTime;
            }
        }
    }

    public void WaitForPassengers()
    {
        state = State.PreparingTrip;
        transform.SetParent(island.ports[port_idx]);
        n_passengers_waiting++;
    }
    public void SailTo(IslandElement elt)
    {
        transform.SetParent(null);
        destination_set = true;
        destination = elt;
        at_dock = false;
        island = null;
        port_idx = -1;
    }
    public void OnReachedIsland()
    {
        if (state == State.Adrift)
        {
            n_passengers_waiting = 0;
            destination_set = false;
        }
        if (state == State.OnTrip)
        {
            destination = null;
            state = State.EndingTrip;
            ReleasePassengers();
        }
    }
    public void ReleasePassengers()
    {
        while (passengers.Count > 0)
        {
            CharacterElement elt = passengers[0];
            int spot_idx = island.AssignSpotIdx();
            ReleasePassenger(elt);
            island.TakePassenger(elt);
            elt.WalkTo(island, spot_idx);
        }
    }
    public void OnReachPlatform()
    {
        if (state == State.EndingTrip)
        {
            if (Vector3.Distance(marker.transform.position, island.marker.transform.position) > 0.2F)
            {
                at_dock = false;
                island = null;
                port_idx = -1;
                transform.position = marker.transform.position;
                transform.SetParent(marker.transform);
                transform.position += Vector3.up * 0.1F;
                state = State.Adrift;
            }
        }
    }
}
