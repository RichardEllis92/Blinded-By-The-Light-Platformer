using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    bool dialogueTriggered = false;

    [SerializeField] private DialogueObject DialogueObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueTriggered && PlayerController.instance.IsGrounded())
        {
            DialogueUI.instance.ShowDialogue(DialogueObject);
            dialogueTriggered = true;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueTriggered && PlayerController.instance.IsGrounded())
        {
            DialogueUI.instance.ShowDialogue(DialogueObject);
            dialogueTriggered = true;
        }

    }
}
