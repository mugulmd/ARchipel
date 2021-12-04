using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokkoLife : GameElement
{
    public enum Activity { 
        Sleep, Idle
    };

    private GameObject home_island;
    private Activity activity;

    protected override void Init()
    {
        base.Init();
        home_island = GameObject.Find("Kokko_Island");
        if(time_ctrl.GetDayState() == TimeController.DayState.Night)
        {
            activity = Activity.Sleep;
        } else
        {
            activity = Activity.Idle;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init(); 
    }

    void GoToSleep()
    {
        activity = Activity.Sleep;
    }

    void WakeUp()
    {
        activity = Activity.Idle;
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
}
