using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingIsland : GameElement
{
    private Animator animator_ctrl;

    protected override void Init()
    {
        base.Init();
        animator_ctrl = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Flip()
    {
        animator_ctrl.SetTrigger("playFlip");
    }

    void OnEnable()
    {
        time_ctrl.onDayStart += Flip;
        time_ctrl.onNightStart += Flip;
    }

    void OnDisable()
    {
        time_ctrl.onDayStart -= Flip;
        time_ctrl.onNightStart -= Flip;
    }
}
