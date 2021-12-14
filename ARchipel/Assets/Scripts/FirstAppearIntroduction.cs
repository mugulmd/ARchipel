using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAppearIntroduction : MonoBehaviour
{
    [Multiline(4)]
    public string displayText="";
    private bool hasDisplayed = false;
    private IntroductionController intro = null;
    public void ShowDisplay()
    {
        if (hasDisplayed)
        {
            return;
        }
        hasDisplayed = true;
        intro.DisplayText(displayText.Replace("\\n", "\n"));
    }

    // Start is called before the first frame update
    void Start()
    {
        intro = GameObject.Find("IntroductionPanel").GetComponent<IntroductionController>();
        DefaultObserverEventHandler ehandle = GetComponent<DefaultObserverEventHandler>();
        ehandle.OnTargetFound.AddListener(ShowDisplay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
