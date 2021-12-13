using System;
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

        int spot_idx = ground.AssignSpotIdx();
        SetGround(ground, spot_idx);
    }

    public override void OnSail(Activity oldState)
    {
        SayIfSpare("Time for treasures");
    }

    public override void OnSleep(Activity oldState)
    {
        SayIfSpare("Time for dreaming treasures");
    }
}
