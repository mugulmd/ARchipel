using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A piece of story, can be played only once
 * 
 */

public abstract class StoryPiece : MonoBehaviour
{
    public string storyName;
    public bool playOnceOnly = true;
    protected enum PlayState { NotPlayed, IsPlaying, HasPlayed};
    protected PlayState state = PlayState.NotPlayed;
    protected Coroutine coroutine = null;
    protected GameObject game_manager;
    protected StoryText story_text;
    protected StoryManager story_manager;
    protected GameData game_data;
    

    public bool CanPlay
    {
        get
        {
            return 
                (
                    (playOnceOnly && state == PlayState.NotPlayed ) ||
                    (!playOnceOnly && state != PlayState.IsPlaying)
                )
                && JudgeCondition();
        }
    }

    // Check condition and play
    public bool TryPlay()
    {
        if (CanPlay)
        {
            coroutine = StartCoroutine(PlayContentWrap());
            return true;
        }
        else
        {
            return false;
        }
    }

    // play directly, no matter what other states should be
    public void Play()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(PlayContentWrap());
    }

    private IEnumerator PlayContentWrap()
    {
        state = PlayState.IsPlaying;
        yield return PlayContent();
        state = PlayState.HasPlayed;
        story_manager.NotifyPlayIsEnded();
    }

    // override in the subclass
    protected abstract IEnumerator PlayContent();

    //override in the subclass
    protected abstract bool JudgeCondition();

    protected virtual void Start()
    {
        game_manager = GameObject.Find("Game Manager");
        story_text = game_manager.GetComponent<StoryText>();
        story_manager = game_manager.GetComponent<StoryManager>();
        game_data = game_manager.GetComponent<GameData>();
    }

}
