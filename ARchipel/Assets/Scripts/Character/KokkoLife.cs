using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokkoLife : CharacterElement
{
    public Animator animator;

    void Start()
    {
        Init();

        int spot_idx = ground.AssignSpotIdx();
        ground.TakePassenger(this);
        SetGround(ground, spot_idx);

        Say("Finding...");
    }

    void Update()
    {
        if (activity == Activity.Walk)
        {
            // move towards assigned destination spot
            // if close enough, stop walking and set new ground
            if (Vector3.Distance(transform.position, destination.spots[dest_spot_idx].position) < 0.01F)
            {
                activity = Activity.Idle;
                SetGround(destination, dest_spot_idx);
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
        animator.SetBool("isMoving", false);
        if (game_data.boat_ctrl.name == ground.name)
        {
            // Kokko just reached a spot on boat
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

    public override void OnSail(Activity oldState)
    {
        SayIfSpare("Another day for searching...");
    }

    public override void OnSleep(Activity oldState)
    {
        SayIfSpare("Time for sleep!");
    }

    public override void OnIdle(Activity oldState)
    {
        SayIfSpare("ummm...");
    }

    public override void OnWalk(Activity oldState)
    {
        SayIfSpare("...");
        animator.SetBool("isMoving", true);
    }
} 
