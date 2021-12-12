using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformElement : GameElement
{
    [HideInInspector]
    public Transform[] spots;
    [HideInInspector]
    public bool[] available;
    [HideInInspector]
    public List<CharacterElement> passengers;

    private bool isInitialize = false;

    public virtual void Init(string target_name)
    {
        base.Init(target_name);
    }

    // some gameobject init before invoking AssignSpotIdx, so we pre-initialize when we need 
    public void InitIsland()
    {
        if (isInitialize)
        {
            return;
        }
        List<Transform> lst_spots = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Spot"))
            {
                lst_spots.Add(child);
            }
        }
        spots = lst_spots.ToArray();
        available = new bool[spots.Length];
        for (int i = 0; i < available.Length; i++)
        {
            available[i] = true;
        }
        passengers = new List<CharacterElement>();
        isInitialize = true;
    }

    public int AssignSpotIdx()
    {
        if (!isInitialize)
        {
            InitIsland();
        }
        for (int i = 0; i < available.Length; i++)
        {
            if (available[i])
            {
                available[i] = false;
                return i;
            }
        }
        return -1;
    }

    public void TakePassenger(CharacterElement elt)
    {
        passengers.Add(elt);
        elt.storyTags.Add("Visited " + this.name);
    }

    public void ReleasePassenger(CharacterElement elt)
    {
        passengers.Remove(elt);
        available[elt.ground_spot_idx] = true;
    }
}
