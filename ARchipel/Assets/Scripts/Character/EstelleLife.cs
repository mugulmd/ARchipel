using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstelleLife : CharacterElement
{
    public Animator animator;

    void Start()
    {
        Init();

        int spot_idx = ground.AssignSpotIdx();
        ground.TakePassenger(this);
        SetGround(ground, spot_idx);

        Say("Finding...");
    }

    void Update()
    {
        // TODO
    }
}
