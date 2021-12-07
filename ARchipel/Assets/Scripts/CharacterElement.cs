using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterElement : GameElement
{
    protected GameObject support;

    protected void SetSupport(string go_name)
    {
        support = GameObject.Find(go_name);
        transform.parent = support.transform;
    }
}
