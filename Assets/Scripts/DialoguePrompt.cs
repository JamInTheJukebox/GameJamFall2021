using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialoguePrompt : Interactable
{
    [SerializeField] DialogueTyper typer;
    [TextArea(15, 5)] [SerializeField] string Dialogue;
    public UnityAction<string> onSendText;

    private void Awake()
    {
        if (typer != null)
            onSendText += typer.TypeText;
    }
    public override bool Interact(Vector2 targetPos)
    {
        if (!TouchToInteract && base.Interact(targetPos))
        {
            return true; // failed to get the item
        }

        // failed to get the item   
        return false;
    }

    public override void executeInteractable()
    {
        print("executing");
        onSendText?.Invoke(Dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!TouchToInteract) { return; }

        if (collision.gameObject.tag == Tags.PLAYER)
        {
            print("sending");
            executeInteractable();
        }
    }
}
