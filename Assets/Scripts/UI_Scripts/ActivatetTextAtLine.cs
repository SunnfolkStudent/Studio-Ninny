using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivatetTextAtLine : MonoBehaviour
{
    public TextAsset theText;

    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;
    public Hitbox theHitbox;
    
    public bool requiredButtonPress;
    private bool waitForPress;
    
    public bool destroyWhenActivated;
    
    void Start()
    {
        theTextBox = FindObjectOfType<TextBoxManager>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (requiredButtonPress)
            {
                waitForPress = true;
                return;
            }
            
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();
        }

        if (destroyWhenActivated)
        {
            Destroy(gameObject);
        }
    }
    void onTriggerExit2D(Collision2D other)
    {
       /* if (other.name == "Player")
        {
            waitForPress 
        } */
    }
}

