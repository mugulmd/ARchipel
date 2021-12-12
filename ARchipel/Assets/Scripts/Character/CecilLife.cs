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

        //IslandElement island = GameObject.Find("Island Cecil").GetComponent<IslandElement>();
        int spot_idx = ground.AssignSpotIdx();
        SetGround(ground, spot_idx);
    }
}
