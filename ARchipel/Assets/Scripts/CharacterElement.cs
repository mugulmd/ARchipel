using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterElement : GameElement
{
    public enum Activity { Sleep, Idle, Walk, Talk, Sail };

    protected Activity activity_;

    
    public Activity activity
    {
        get
        {
            return activity_;
        }
        set
        {
            if (activity_ != value)
            {
                Activity oldActivity_ = activity_;
                activity_ = value;
                this.OnStateChange(oldActivity_, activity_);
                StateChanged.Invoke(oldActivity_, activity_);
            }
        }
    }

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
    public UnityEvent<Activity,Activity> StateChanged; // <old State, current state>.

    public DialogBubble dialogBubble;

    public virtual void Init(string target_name)
    {
        base.Init(target_name);
        if (dialogBubble == null)
        {
            this.dialogBubble = gameObject.GetComponent<DialogBubble>();
        }
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

    public virtual void OnStateChange(Activity oldState, Activity newState)
    {
        // implement in the sub-class, to process different activities.
        switch (newState)
        {
            case Activity.Sleep:
                OnSleep(oldState);
                break;
            case Activity.Sail:
                OnSail(oldState);
                break;
            case Activity.Idle:
                OnIdle(oldState);
                break;
            case Activity.Talk:
                OnTalk(oldState);
                break;
            case Activity.Walk:
                OnWalk(oldState);
                break;
        }
    }

    public virtual void OnSleep(Activity oldState)
    {

    }

    public virtual void OnSail(Activity oldState)
    {

    }

    public virtual void OnIdle(Activity oldState)
    {

    }

    public virtual void OnTalk(Activity oldState)
    {

    }

    public virtual void OnWalk(Activity oldState)
    {

    }

    public void Say(string s)
    {
        this.dialogBubble.DisplayText(s);
    }
}
