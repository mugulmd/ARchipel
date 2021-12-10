using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokkoLife : CharacterElement
{
    private Animator animator;

    void Start()
    {
        Init("Target Kokko");
        activity = Activity.Sleep;
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();

        IslandElement island = GameObject.Find("Island Kokko").GetComponent<IslandElement>();
        int spot_idx = island.AssignSpotIdx();
        island.TakePassenger(this);
        SetGround(island, spot_idx);
    }

    void Update()
    {
        if (activity == Activity.Walk)
        {
            // move towards assigned destination spot
            // if close enough, stop walking and set new ground
            if (Vector3.Distance(transform.position, destination.spots[dest_spot_idx].position) < 0.01F)
            {
                ReachedPlatform.Invoke();
            } else
            {
                Vector3 dir = (destination.spots[dest_spot_idx].position - transform.position).normalized;
                transform.position += dir * speed * Time.deltaTime;
            }
        }
    }

    public void OnDayStart()
    {
        activity = Activity.Idle;
        animator.SetBool("isAwake", true);
    }
    public void OnNightStart()
    {
        activity = Activity.Sleep;
        animator.SetBool("isAwake", false);
    }
    public void OnBoatReachIsland()
    {
        // check if island is Kokko's ground
        // if so, go on an adventure
        if (game_data.boat_ctrl.island.name == ground.name)
        {
            game_data.boat_ctrl.WaitForPassengers();
            int spot_idx = game_data.boat_ctrl.AssignSpotIdx();
            ground.ReleasePassenger(this);
            game_data.boat_ctrl.TakePassenger(this);
            WalkTo(game_data.boat_ctrl, spot_idx);
        }
    }
    public void OnReachedPlatform()
    {
        activity = Activity.Idle;
        SetGround(destination, dest_spot_idx);
        if (game_data.boat_ctrl.name == ground.name)
        {
            // determine an island to go to
            // start sailing
            IslandElement dest_island = null;
            foreach (IslandElement elt in game_data.islands)
            {
                if (elt.name != game_data.boat_ctrl.island.name)
                {
                    dest_island = elt;
                    break;
                }
            }
            game_data.boat_ctrl.SailTo(dest_island);
        }
    }
} 
