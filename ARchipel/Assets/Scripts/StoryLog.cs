using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLog : MonoBehaviour
{
    // Log
    private List<String> messages;

    // GUI stuff
    private Vector2 scroll_pos;

    // Start is called before the first frame update
    void Start()
    {
        messages = new List<String>();
        scroll_pos = Vector2.zero;

        AddMessage("Let the story begin...");
    }

    // Add a new notification to the story log
    public void AddMessage(String msg)
    {
        messages.Add(msg);
        scroll_pos = new Vector2(0, 100);
    }

    // Display log on the right side of application screen in a scrollable panel
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 220, 20, 200, Screen.height - 20));
        scroll_pos = GUILayout.BeginScrollView(scroll_pos, GUILayout.Width(200), GUILayout.Height(100));
        foreach(String msg in messages)
        {
            GUILayout.Label(msg);
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    // Automatic messages
    void SayHello()
    {
        AddMessage("Good morning everyone !");
    }
    void SayBye()
    {
        AddMessage("Good night, see you tomorrow...");
    }

    void OnEnable()
    {
        TimeController time_ctrl = GetComponent<TimeController>();
        time_ctrl.onDayStart += SayHello;
        time_ctrl.onNightStart += SayBye;
    }

    void OnDisable()
    {
        TimeController time_ctrl = GetComponent<TimeController>();
        time_ctrl.onDayStart -= SayHello;
        time_ctrl.onNightStart -= SayBye;
    }
}
