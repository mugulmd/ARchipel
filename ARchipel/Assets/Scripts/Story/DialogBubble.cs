using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogBubble : MonoBehaviour
{
    public TextMeshPro textMesh;
    // Start is called before the first frame update
    void Start()
    {
    }

    //TODO: dynamically display the text
    public void DisplayText(string s)
    {
        textMesh.text = s;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
