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
        if (ground.name == game_data.boat_ctrl.name)
        {
            // Estelle just reached a spot on boat
            // determine an island to go to
            // start sailing
            if (game_data.boat_ctrl.island.name == "Island Estelle")
            {
                game_data.boat_ctrl.SailTo(game_data.islandDict["Island Cecil"]);
            }
            else
            {
                game_data.boat_ctrl.SailTo(game_data.islandDict["Island Estelle"]);
            }
        }
    }

    public override void OnIdle(Activity oldState)
    {
        animator.SetBool("isMoving", false);
    }

    public override void OnWalk(Activity oldState)
    {
        animator.SetBool("isMoving", true);
    }
}
