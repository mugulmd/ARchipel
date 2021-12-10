using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterElement : GameElement
{
    [HideInInspector]
    public GameElement support;

    public void SetSupport(IslandElement island, Transform spot)
    {
        support = island;
        transform.position = spot.position;
        transform.SetParent(spot);
    }
    public void SetSupport(BoatController boat, Transform spot)
    {
        support = boat;
        transform.position = spot.position;
        transform.SetParent(spot);
    }
}
