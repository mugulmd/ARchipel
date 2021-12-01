using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public enum DayState { 
        Morning, Afternoon, Evening, Night
    };

    // Ratio between one day in the game and one day in real life
    private float speed;
    // Game time
    private float time;
    private int day;
    private int hour;
    private DayState state;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2000;
        time = 0;
        day = 0;
        state = DayState.Night;
    }

    // Update is called once per frame
    void Update()
    {
        time += speed * Time.deltaTime;

        if(time >= 24 * 3600)
        {
            day++;
            time -= 24 * 3600;
        }

        hour = Mathf.FloorToInt(time / 3600);

        if(state == DayState.Night && hour >= 7 && hour < 21)
        {
            state = DayState.Morning;
            DayStart();
        }
        else if(state == DayState.Morning && hour >= 12)
        {
            state = DayState.Afternoon;
        }
        else if(state == DayState.Afternoon && hour >= 18)
        {
            state = DayState.Evening;
        }
        else if(state == DayState.Evening && hour >= 21)
        {
            state = DayState.Night;
            NightStart();
        }
    }

    // Event system
    public event Action onDayStart;
    void DayStart()
    {
        if(onDayStart != null)
        {
            onDayStart();
        }
    }
    public event Action onNightStart;
    void NightStart()
    {
        if(onNightStart != null)
        {
            onNightStart();
        }
    }

    // Basic display on top-left of the application screen
    void OnGUI()
    {
        String labelText = "Day: " + day + ", hour: " + hour + ", " + state.ToString();

        GUILayout.BeginArea(new Rect(20, 20, 200, 20));
        GUILayout.Label(labelText);
        GUILayout.EndArea();
    }

    // Getters
    public float GetTime()
    {
        return time;
    }
    public int GetDay()
    {
        return day;
    }
    public int GetHour()
    {
        return hour;
    }
    public DayState GetDayState()
    {
        return state;
    }
}
