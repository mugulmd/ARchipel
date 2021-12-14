using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CecilLife : CharacterElement
{
    public Animation animation;

    private bool has_aquaphobia;

    void Start()
    {
        Init();
        animation.Play("Idle");
        has_aquaphobia = true;

        int spot_idx = ground.AssignSpotIdx();
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
            }
            else
            {
                Vector3 dir = (destination.spots[dest_spot_idx].position - transform.position).normalized;
                transform.position += dir * speed * Time.deltaTime;
            }
        }
    }

    public void OvercomeAquaphobia()
    {
        has_aquaphobia = false;
    }

    public override void OnSail(Activity oldState)
    {
        SayIfSpare("Time for treasures");
    }

    public override void OnSleep(Activity oldState)
    {
        SayIfSpare("Time for dreaming treasures");
    }

    public void OnBoatReachIsland()
    {
        // check if island is Cecil's ground
        // and if Cecil is not scared of water
        // if so, go on an adventure
        if (game_data.boat_ctrl.island.name == ground.name && !has_aquaphobia)
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
        if (game_data.boat_ctrl.name == ground.name)
        {
            // Cecil just reached a spot on boat
            // determine an island to go to
            // start sailing
            if (game_data.boat_ctrl.island.name == "Island Estelle")
            {
                game_data.boat_ctrl.SailTo(game_data.islandDict["Island Cecil"]);
            }
            else if (game_data.boat_ctrl.island.name == "Island Cecil")
            {
                game_data.boat_ctrl.SailTo(game_data.islandDict["Island Estelle"]);
            }
        }
    }
}
