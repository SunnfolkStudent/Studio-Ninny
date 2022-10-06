using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Npc : MonoBehaviour
{
    public DialogTrigger trigger;
    public DialogManager Manager;
    public Hitbox hitbox;

    private void OnTriggerStay2D(Collider2D other)
    {
        if ((hitbox.talkUI == true) && DialogManager.isActive == false)
        {
            DialogManager.isActive = true;
            trigger.StartDialogue();
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
