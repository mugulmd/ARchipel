using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CecilLife : CharacterElement
{
    private Animation animation;

    void Start()
    {
        Init("Target Cecil");
        animation = transform.GetChild(0).gameObject.GetComponent<Animation>();
        animation.Play("Idle");

        // TODO : improve code using game_data
        IslandElement island = GameObject.Find("Island Cecil").GetComponent<IslandElement>();
        Transform spot = island.spots[0];
        SetSupport(island, spot);
    }
}
