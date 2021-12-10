using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokkoLife : CharacterElement
{
    public enum Activity { Sleep, Idle, Sail };
    [HideInInspector]
    public Activity activity;

    private Animator animator;

    void Start()
    {
        Init("Target Kokko");
        activity = Activity.Sleep;
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();

        // TODO : improve code using game_data
        IslandElement island = GameObject.Find("Island Kokko").GetComponent<IslandElement>();
        Transform spot = island.spots[0];
        SetSupport(island, spot);
    }

    public void OnDayStart()
    {
        activity = Activity.Idle;
        animator.SetBool("isAwake", true);
    }
    public void OnNightStart()
    {
        activity = Activity.Sleep;
        animator.SetBool("isAwake", false);
    }
    public void OnBoatReachIsland()
    {
        // check if island is Kokko's support
        // if so, go an adventure
    }
} 
