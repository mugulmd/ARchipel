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
