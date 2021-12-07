using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokkoLife : CharacterElement
{
    public enum Activity { Sleep, Idle, Sail };

    [HideInInspector]
    private Activity activity;

    private Animator animator;

    void Start()
    {
        Init();
        SetSupport(GameObject.Find("Island Kokko"));
        activity = Activity.Sleep;
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
    public void OnBoatReachIsland()
    {
        if (activity == Activity.Idle && game_data.boat_ctrl.island == support)
        {
            transform.position = game_data.boat.transform.position;
            SetSupport(game_data.boat);
            activity = Activity.Sail;

            GameObject[] islands = GameObject.FindGameObjectsWithTag("Island");
            GameObject dest = null;
            foreach (GameObject obj in islands)
            {
                if (obj != support)
                {
                    dest = obj;
                    break;
                }
            }
            game_data.boat_ctrl.SailTo(dest);
        }
        else if (activity == Activity.Sail)
        {
            SetSupport(game_data.boat_ctrl.island);
            activity = Activity.Idle;
        }
    }
} 
