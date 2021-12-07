using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokkoLife : CharacterElement
{
    private Animator animator;

    protected override void Init()
    {
        base.Init();
        SetSupport("Island Kokko");
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        Init(); 
    }
    void OnEnable()
    {
        time_ctrl.onDayStart += WakeUp;
        time_ctrl.onNightStart += GoToSleep;
    }
    void OnDisable()
    {
        time_ctrl.onDayStart -= WakeUp;
        time_ctrl.onNightStart -= GoToSleep;
    }

    void GoToSleep()
    {
        animator.SetBool("isAwake", false);
    }
    void WakeUp()
    {
        animator.SetBool("isAwake", true);
    }
}
