using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTriggerEndGame : MonoBehaviour
{
    public static DialogueTriggerEndGame instance;

    bool dialogueTriggered = false;
    public bool endGame = false;

    [SerializeField] private DialogueObject DialogueObject;

    private void Awake()
    {
        instance = this;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueTriggered && PlayerController.instance.IsGrounded())
        {
            DialogueUI.instance.ShowDialogue(DialogueObject);

            dialogueTriggered = true;
            endGame = true;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueTriggered && PlayerController.instance.IsGrounded())
        {
            DialogueUI.instance.ShowDialogue(DialogueObject);
            dialogueTriggered = true;
            endGame = true;
        }

    }
}