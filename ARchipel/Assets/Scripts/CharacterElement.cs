using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterElement : GameElement
{
    [HideInInspector]
    public GameObject support;

    protected void SetSupport(GameObject obj)
    {
        support = obj;
        transform.parent = support.transform;
    }
}
