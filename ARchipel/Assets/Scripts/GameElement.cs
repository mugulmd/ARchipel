using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameElement : MonoBehaviour
{
    protected GameObject game_manager;
    protected GameData game_data;

    protected virtual void Init()
    {
        game_manager = GameObject.Find("Game Manager");
        game_data = game_manager.GetComponent<GameData>();
    }
}
