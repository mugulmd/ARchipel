using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CecilLife : CharacterElement
{
    private Animation animation;

    void Start()
    {
        Init();
        SetSupport(GameObject.Find("Island Cecil"));
        animation = transform.GetChild(0).gameObject.GetComponent<Animation>();
    }

    public void WakeUp()
    {
        animation.Play("Idle");
    }
    public void GoToSleep()
    {
        animation.Stop();
    }
}
