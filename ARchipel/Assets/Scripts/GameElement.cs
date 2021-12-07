using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameElement : MonoBehaviour
{
    protected GameObject game_manager;
    protected TimeController time_ctrl;
    protected StoryLog story_log;
    protected GameObject boat;

    protected virtual void Init()
    {
        game_manager = GameObject.Find("Game Manager");
        time_ctrl = game_manager.GetComponent<TimeController>();
        story_log = game_manager.GetComponent<StoryLog>();
        boat = GameObject.Find("Boat");
    }
}
