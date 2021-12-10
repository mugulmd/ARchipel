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
    public IslandElement[] islands;

    [HideInInspector]
    public CharacterElement[] characters;

    void Start()
    {
        time_ctrl = GetComponent<TimeController>();
        story_log = GetComponent<StoryLog>();
        boat = GameObject.Find("Boat");
        boat_ctrl = boat.GetComponent<BoatController>();

        List<IslandElement> lst_islands = new List<IslandElement>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Island"))
        {
            lst_islands.Add(obj.GetComponent<IslandElement>());
        }
        islands = lst_islands.ToArray();

        List<CharacterElement> lst_characters = new List<CharacterElement>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Character"))
        {
            lst_characters.Add(obj.GetComponent<CharacterElement>());
        }
        characters = lst_characters.ToArray();
    }
}
