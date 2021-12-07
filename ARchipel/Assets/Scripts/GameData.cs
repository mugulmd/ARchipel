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

    [HideInInspector]
    public Dictionary<string, GameObject> markers;

    void Start()
    {
        time_ctrl = GetComponent<TimeController>();
        story_log = GetComponent<StoryLog>();
        boat = GameObject.Find("Boat");
        boat_ctrl = boat.GetComponent<BoatController>();
        markers = new Dictionary<string, GameObject>();
        markers.Add("Kokko", GameObject.Find("Target Kokko"));
        markers.Add("Cecil", GameObject.Find("Target Cecil"));
        markers.Add("Boat", GameObject.Find("Target Boat"));
    }
}
