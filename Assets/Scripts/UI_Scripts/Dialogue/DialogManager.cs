using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text MessageText;
    public RectTransform backgroundBox;
    public PlayerInput _input;
    public PlayerAnimator _pAnim;

    private Message[] currentMessages;
    private Actor[] CurrentActors;
    private int activeMessage = 0;
    public static bool isActive = false;
    public Hitbox Hitbox;

    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
        
        if(GameObject.Find("Player"))
            _input = GameObject.Find("Player").GetComponent<PlayerInput>();
    }
    
    void Update()
    {
        if (_input.Interact && isActive == true)
        { NextMessage(); }
    }
    
    
    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        CurrentActors = actors;
        activeMessage = 0;
        isActive = true;
        Debug.Log("Started Conversation! Loaded messages:" + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f);
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        MessageText.text = messageToDisplay.message;

        Actor actorToDisplay = CurrentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.Sprite;
        
        AnimateTextColor();
    }

    public void NextMessage()
    { activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation ended!");
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActive = false;

            // kill me
            Hitbox.talkUI = false;
            _pAnim.isTalking = false;
            _input.characterControl = true;
            //Hitbox.npcInteractUI = true;
        } 
    }

    void AnimateTextColor()
    {
        LeanTween.textAlpha(MessageText.rectTransform, 0, 0);
        LeanTween.textAlpha(MessageText.rectTransform, 1, 0.5f);
    }
}