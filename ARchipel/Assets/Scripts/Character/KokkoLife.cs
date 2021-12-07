using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokkoLife : CharacterElement
{
    public enum Activity { 
        Sleep, Idle, Sail
    };
    private Activity activity;

    private Animator animator;

    void Start()
    {
        Init();
        SetSupport("Island Kokko");
        if (time_ctrl.GetDayState() == TimeController.DayState.Night) GoToSleep();
        else WakeUp();
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    public void WakeUp()
    {
        activity = Activity.Idle;
        animator.SetBool("isAwake", true);
    }
    public void GoToSleep()
    {
        activity = Activity.Sleep;
        animator.SetBool("isAwake", false);
    }
    public void GoToBoat()
    {
        if (activity == Activity.Idle && boat.GetComponent<BoatController>().GetIsland() == support)
        {
            transform.position = boat.transform.position;
            SetSupport("Boat");
            activity = Activity.Sail;
        }
    }
}
