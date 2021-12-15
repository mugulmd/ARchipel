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
        if (ground.name == game_data.boat_ctrl.name)
        {
            // Kokko just reached a spot on boat
            // determine an island to go to
            // start sailing
            if (HasStoryTag("Know about floating island") && !HasStoryTag("Visited floating island"))
            {
                game_data.boat_ctrl.SailTo(game_data.islandDict["Floating Island"]);
            }
            else if (game_data.boat_ctrl.island.name == "Island Kokko")
            {
                game_data.boat_ctrl.SailTo(game_data.islandDict["Island Cecil"]);
            }
            else if (game_data.boat_ctrl.island.name == "Island Cecil")
            {
                game_data.boat_ctrl.SailTo(game_data.islandDict["Island Kokko"]);
            }
        } 
        else if (ground.name == "Floating Island")
        {
            AddStoryTag("Visited floating island");
        }
    }

    public override void OnWalk(Activity oldState)
    {
        animator.SetBool("isMoving", true);
    }
} 
