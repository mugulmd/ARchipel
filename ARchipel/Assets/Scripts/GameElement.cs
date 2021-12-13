using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameElement : MonoBehaviour
{
    [HideInInspector]
    public string name;

    [HideInInspector]
    public GameObject marker;

    protected GameObject game_manager;
    protected GameData game_data;


    protected virtual void Init(string target_name)
    {
        name = gameObject.name;
        marker = GameObject.Find(target_name);
        game_manager = GameObject.Find("Game Manager");
        game_data = game_manager.GetComponent<GameData>();
    }

    // a set to record characters' information, like where they have been,
    // who they have met, what they know about the world, what is the state of the island
    public HashSet<string> storyTags = new HashSet<string>();

    public bool HasStoryTag(string s)
    {
        return storyTags.Contains(s);
    }

    public void AddStoryTag(string s)
    {
        storyTags.Add(s);
    }
}
