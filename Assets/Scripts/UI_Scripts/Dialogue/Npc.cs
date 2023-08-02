using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Npc : MonoBehaviour
{
    public DialogTrigger trigger, trigger2; // trigger2 is for ferryman
    public DialogManager Manager;
    public Hitbox hitbox;

    public GameObject wall;

    private void OnTriggerStay2D(Collider2D other)
    {
        if ((hitbox.talkUI == true) && DialogManager.isActive == false)
        {
            DialogManager.isActive = true;
            
            if(hitbox.hasFlower && gameObject.name == "Ferryman")
            {
                trigger2.StartDialogue();
                wall.SetActive(false);
            }
            else
                trigger.StartDialogue(); // default
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Interact"))
        {
            DialogManager.isActive = false;
        }
    }
}
