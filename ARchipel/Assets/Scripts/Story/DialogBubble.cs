using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogBubble : MonoBehaviour
{
    public TextMeshPro textMesh;

    public float displayOneCharInterval = 0.12f; // play interval for the printing animation
    public float perCharKeepBeforeDisappear = 0.08f; // extra keep time according to the string length
    public float basicKeepBeforeDisappear = 3.0f; // basic keep time
    public Color backgroundColor = Color.white;
    private string currentText = "";
    private int displayedCharsIndex = 0;
    enum DisplayState {Idle, Printing, Showing}
    private DisplayState state;
    private Coroutine displayCoroutine;


    public bool IsPlaying
    {
        get
        {
            return state != DisplayState.Idle;
        }
    }

    public float LeftPlayingTime
    {
        get
        {
            switch (state)
            {
                case DisplayState.Idle:
                    return 0;
                case DisplayState.Printing:
                    return displayOneCharInterval * (currentText.Length - displayedCharsIndex)
                        +basicKeepBeforeDisappear+currentText.Length*perCharKeepBeforeDisappear;
                case DisplayState.Showing:
                    return basicKeepBeforeDisappear + currentText.Length * perCharKeepBeforeDisappear;
            }
            return 0.0f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        displayCoroutine = null;
        state = DisplayState.Idle;
    }

    //display whole text immediately, skip current printing animation
    public void DisplayTextWhole()
    {
        if (state == DisplayState.Printing)
        {
            displayedCharsIndex = currentText.Length;
            SetText(currentText);
            state = DisplayState.Showing;
        }
    }

    //Display the text, will interrupt the current text! 
    public void DisplayText(string s)
    {
        if (state != DisplayState.Idle)
        {
            StopCoroutine(displayCoroutine);
        }
        currentText = s;
        textMesh.enabled = true;
        textMesh.text = "";
        displayedCharsIndex = 0;
        state = DisplayState.Printing;
        displayCoroutine = StartCoroutine(DisplayCoroutine());
    }

    public bool DisplayTextIfSpare(string s)
    {
        if (!IsPlaying)
        {
            DisplayText(s);
            return true;
        }
        return false;
    }

    IEnumerator DisplayCoroutine()
    {
        while(state == DisplayState.Printing)
        {
            SetText(currentText.Substring(0, displayedCharsIndex));
            displayedCharsIndex++;
            if (displayedCharsIndex >= currentText.Length)
            {
                state = DisplayState.Showing;
            }
            yield return new WaitForSeconds(displayOneCharInterval);
        }

        yield return new WaitForSeconds(basicKeepBeforeDisappear+
            perCharKeepBeforeDisappear * currentText.Length);
        textMesh.enabled = false;
        state = DisplayState.Idle;
        yield break;
    }

    void SetText(string s)
    {
        textMesh.text = "<mark=#" + ColorUtility.ToHtmlStringRGBA(backgroundColor) + " padding=5,5,5,5>" + s + "</mark>";
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //billboard, keep facing to the user
        textMesh.transform.rotation = Camera.main.transform.rotation;
    }
}
