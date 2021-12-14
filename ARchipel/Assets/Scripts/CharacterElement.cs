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
                Debug.Log("Set " + this.name + " to " + value);
                Activity oldActivity_ = activity_;
                activity_ = value;
                StateChanged.Invoke(oldActivity_, activity_);
            }
        }
    }

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

    // block daily speech text when the character is occupied by the story.
    public bool isOccupiedByStory = false;

    public virtual void Init()
    {
        base.Init();
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

    // let the character say something!
    public void Say(string s)
    {
        this.dialogBubble.DisplayText(s);
        game_data.story_log.AddMessage("[" + this.name + "] " + s);
    }

    public void SayIfSpare(string s)
    {
        if (!isOccupiedByStory)
        {
            if (dialogBubble.DisplayTextIfSpare(s))
            {
                game_data.story_log.AddMessage("[" + this.name + "] " + s);
            }
        }
    }

    public bool IsOnThePlatform(string s)
    {
        return this.ground.name == s;
    }

    public void LookAt(GameObject gameObject)
    {
        LookAtUtility comp = GetComponent<LookAtUtility>();
        if (comp != null)
        {
            comp.SetLookAtObject(gameObject);
        }
        else
        {

        }
        
    }
}
