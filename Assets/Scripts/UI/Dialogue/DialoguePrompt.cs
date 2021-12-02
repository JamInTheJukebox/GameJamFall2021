using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class DialoguePrompt : Interactable
{
    [SerializeField] DialogueTyper dialogueTyper;
    [SerializeField] List<Conversation> conversation = new List<Conversation>();
    public UnityAction<List<Conversation>, UnityAction> onSendText;

    private void Start()
    {
        if(dialogueTyper == null)
        {
            dialogueTyper = MasterUserInterface.instance.DialogueTyper;
        }
        if (dialogueTyper != null)
            onSendText += dialogueTyper.TypeText;
        else
            Destroy(gameObject);
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
        onSendText?.Invoke(conversation, null);
        onInteract?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!TouchToInteract) { return; }

        if (collision.gameObject.tag == Tags.PLAYER)
        {
            executeInteractable();
        }
    }
}
[Serializable]
public struct Conversation
{
    public DialogueProfile_SO profile;
    public int pictureIndex;
    public string line;
    // text animation?
}
