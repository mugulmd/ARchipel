using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokkoLife : CharacterElement
{
    private Animator animator;

    void Start()
    {
        Init();
        SetSupport("Island Kokko");
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    public void WakeUp()
    {
        animator.SetBool("isAwake", true);
    }
    public void GoToSleep()
    {
        animator.SetBool("isAwake", false);
    }
}
