using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstelleLife : CharacterElement
{
    public Animator animator;

    void Start()
    {
        Init();

        int spot_idx = ground.AssignSpotIdx();
        ground.TakePassenger(this);
        SetGround(ground, spot_idx);
    }

    public void OnBoatReachIsland()
    {
        // check if island is Estelle's ground
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
        if (game_data.boat_ctrl.name == ground.name)
        {
            // Estelle just reached a spot on boat
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
