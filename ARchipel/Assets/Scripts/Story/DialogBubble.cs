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

    private string currentText = "";
    private int displayedCharsIndex = 0;
    enum DisplayState {Idle, Printing, Showing}
    private DisplayState state;
    private Coroutine displayCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        displayCoroutine = null;
        state = DisplayState.Idle;
    }

    public void DisplayTextWhole(string s)
    {
        displayedCharsIndex = s.Length;
        textMesh.text = currentText;
        state = DisplayState.Showing;
    }

    //TODO: dynamically display the text
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

    IEnumerator DisplayCoroutine()
    {
        while(state == DisplayState.Printing)
        {
            textMesh.text = currentText.Substring(0, displayedCharsIndex);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
