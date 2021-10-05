using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePrompt : Interactable
{
    DialogueTyper dialogueTyper;
    [TextArea(15, 5)] [SerializeField] string Dialogue;

    private void Awake()
    {
        dialogueTyper = GetComponent<DialogueTyper>();
        Interact(transform.position);
    }

    public override bool Interact(Vector2 targetPos)
    {
        dialogueTyper.TypeText("I ate a pizza today");
        return true;
    }
}
