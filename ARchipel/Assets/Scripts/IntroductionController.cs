using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionController : MonoBehaviour
{
    public Canvas canvas;
    public Text text;
    public float displayTimeBasic = 3.0f;
    public float displayTimePerChar = 0.2f;
    public float displayTimeLeft = 0.01f;

    public void DisplayText(string s)
    {
        text.text = s;
        displayTimeLeft = displayTimeBasic + displayTimePerChar * s.Length;
        canvas.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (displayTimeLeft > 0)
        {
            displayTimeLeft -= Time.deltaTime;
            if (displayTimeLeft <= 0)
            {
                canvas.enabled = false;
            }
        }
    }
}
