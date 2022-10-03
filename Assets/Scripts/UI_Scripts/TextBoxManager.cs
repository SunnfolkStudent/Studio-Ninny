using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBoxManager : MonoBehaviour
{
    public GameObject textBox;

    public TMP_Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerInput player;

    public bool isActive;
    
    void Start()
    {
        player = FindObjectOfType<PlayerInput>();
        
        if (textFile != null)
        { textLines = (textFile.text.Split('\n')); }

        if (endAtLine == 0)
        { endAtLine = textLines.Length - 1; }

        if (isActive)
        { EnableTextBox(); }
        else
        { DisableTextBox(); }
    }

    void Update()
    {
        if (!isActive)
        { return; }
        theText.text = textLines[currentLine];

        if (Input.GetKeyDown((KeyCode.F)))
        { currentLine += 1; }

        if (currentLine > endAtLine)
        { DisableTextBox(); }
    }

    public void EnableTextBox()
    { textBox.SetActive(true);
        isActive = true; }

    public void DisableTextBox()
    { textBox.SetActive(false);
        isActive = false; }

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
        { textLines = new String[1];
            textLines = (theText.text.Split('\n')); }
    }
}