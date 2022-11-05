using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerDialogueTrigger : MonoBehaviour
{
    bool dialogueTriggered = false;

    [SerializeField] private DialogueObject DialogueBurger2a;
    [SerializeField] private DialogueObject DialogueBurger2b;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueTriggered && PlayerController.instance.IsGrounded())
        {
            if(LevelManager.instance.currentFood > 0)
            {
                DialogueUI.instance.ShowDialogue(DialogueBurger2a);
                dialogueTriggered = true;
            }
            else
            {
                DialogueUI.instance.ShowDialogue(DialogueBurger2b);
                dialogueTriggered = true;
            }
            
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueTriggered && PlayerController.instance.IsGrounded())
        {
            if (LevelManager.instance.currentFood > 0)
            {
                DialogueUI.instance.ShowDialogue(DialogueBurger2a);
                dialogueTriggered = true;
            }
            else
            {
                DialogueUI.instance.ShowDialogue(DialogueBurger2b);
                dialogueTriggered = true;
            }
        }
    }
}
