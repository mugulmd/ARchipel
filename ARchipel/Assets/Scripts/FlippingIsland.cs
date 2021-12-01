using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingIsland : MonoBehaviour
{
    [SerializeField]
    private Animator animator_ctrl;

    void Flip()
    {
        animator_ctrl.SetTrigger("playFlip");
    }

    void OnEnable()
    {
        TimeController time_ctrl = GameObject.Find("Game Manager").GetComponent<TimeController>();
        time_ctrl.onDayStart += Flip;
        time_ctrl.onNightStart += Flip;
    }

    void OnDisable()
    {
        TimeController time_ctrl = GameObject.Find("Game Manager").GetComponent<TimeController>();
        time_ctrl.onDayStart -= Flip;
        time_ctrl.onNightStart -= Flip;
    }
}
