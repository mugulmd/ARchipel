using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CecilLife : CharacterElement
{
    public Animation animation;

    void Start()
    {
        Init();

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

    public void OnBoatReachIsland()
    {
        // check if island is Cecil's ground
        // and if Cecil is not scared of water
        // if so, go on an adventure
        // TODO : also requires kokko to be here
        if (game_data.boat_ctrl.island.name == ground.name && HasStoryTag("No Aquaphobia"))
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
        
    }

    public override void OnIdle(Activity oldState)
    {
        animation.Play("Idle");
    }

    public virtual void OnWalk(Activity oldState)
    {
        animation.Play("Run");
    }
}
