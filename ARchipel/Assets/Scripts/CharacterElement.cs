using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterElement : GameElement
{
    public enum Activity { Sleep, Idle, Walk, Talk, Sail };
    [HideInInspector]
    public Activity activity;

    //[HideInInspector]
    public PlatformElement ground;
    [HideInInspector]
    public int ground_spot_idx;

    [HideInInspector]
    public PlatformElement destination;
    [HideInInspector]
    public int dest_spot_idx;

    protected float speed;

    public UnityEvent ReachedPlatform;

    public virtual void Init(string target_name)
    {
        base.Init(target_name);
        activity = Activity.Idle;
        speed = 0.05F;
        if (ReachedPlatform == null)
        {
            ReachedPlatform= new UnityEvent();
        }
    }

    public virtual void SetGround(PlatformElement elt, int spot_idx)
    {
        ground = elt;
        ground_spot_idx = spot_idx;
        transform.position = elt.spots[spot_idx].position;
        transform.SetParent(elt.spots[spot_idx]);
    }

    public virtual void WalkTo(PlatformElement elt, int spot_idx)
    {
        destination = elt;
        dest_spot_idx = spot_idx;
        activity = Activity.Walk;
    }
}
