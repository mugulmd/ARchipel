using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionController : MonoBehaviour
{
    public Text question;

    [HideInInspector]
    public bool decision_taken;

    [HideInInspector]
    public bool chose_yes;

    void Awake()
    {
        decision_taken = false;
        chose_yes = false;
        gameObject.SetActive(false);
    }

    public void AskUser(string s)
    {
        question.text = s;
        decision_taken = false;
        gameObject.SetActive(true);
    }

    public void OnClickYes()
    {
        chose_yes = true;
        decision_taken = true;
        gameObject.SetActive(false);
    }

    public void OnClickNo()
    {
        chose_yes = false;
        decision_taken = true;
        gameObject.SetActive(false);
    }
}
