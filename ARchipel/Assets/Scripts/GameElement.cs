using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameElement : MonoBehaviour
{
    [HideInInspector]
    public string name;

    protected GameObject game_manager;
    protected GameData game_data;

    protected GameObject marker;

    protected virtual void Init(string target_name)
    {
        name = gameObject.name;
        game_manager = GameObject.Find("Game Manager");
        game_data = game_manager.GetComponent<GameData>();
        marker = GameObject.Find(target_name);
    }
}
