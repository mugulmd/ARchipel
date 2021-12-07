using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [HideInInspector]
    public TimeController time_ctrl;
    [HideInInspector]
    public StoryLog story_log;
    [HideInInspector]
    public GameObject boat;
    [HideInInspector]
    public BoatController boat_ctrl;

    void Start()
    {
        time_ctrl = GetComponent<TimeController>();
        story_log = GetComponent<StoryLog>();
        boat = GameObject.Find("Boat");
        boat_ctrl = boat.GetComponent<BoatController>();
    }
}
