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
    public DecisionController decision_ctrl;

    [HideInInspector]
    public GameObject boat;
    [HideInInspector]
    public BoatController boat_ctrl;

    [HideInInspector]
    public IslandElement[] islands;

    [HideInInspector]
    public CharacterElement[] characters;

    //[HideInInspector]
    public Dictionary<string, CharacterElement> characterDict;
    //[HideInInspector]
    public Dictionary<string, IslandElement> islandDict;

    void Awake()
    {
        time_ctrl = GetComponent<TimeController>();
        story_log = GetComponent<StoryLog>();
        decision_ctrl = GameObject.Find("DecisionPannel").GetComponent<DecisionController>();
        boat = GameObject.Find("Boat");
        boat_ctrl = boat.GetComponent<BoatController>();

        characterDict = new Dictionary<string, CharacterElement>();
        islandDict = new Dictionary<string, IslandElement>();

        List<IslandElement> lst_islands = new List<IslandElement>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Island"))
        {
            lst_islands.Add(obj.GetComponent<IslandElement>());
            islandDict[obj.name] = obj.GetComponent<IslandElement>();
            Debug.Log(obj.name + " |island");
        }
        islands = lst_islands.ToArray();

        List<CharacterElement> lst_characters = new List<CharacterElement>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Character"))
        {
            lst_characters.Add(obj.GetComponent<CharacterElement>());
            characterDict[obj.name] = obj.GetComponent<CharacterElement>();
            Debug.Log(obj.name + " |character");
        }
        characters = lst_characters.ToArray();
    }
}
