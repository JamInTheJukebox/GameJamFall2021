using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueTyper : MonoBehaviour
{
    TextMeshProUGUI dialogueTextContainer;
    public bool CanSkipDialogue;
    private bool skipDialogue;
    [SerializeField] float textScrollTime = 0.01f;
    private void Awake()
    {
        dialogueTextContainer = GetComponent<TextMeshProUGUI>();
        dialogueTextContainer.text = "";
    }

    private void Update()
    {
        if(CanSkipDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            skipDialogue = true;
        }
    }
    public void TypeText(string txt)
    {
        dialogueTextContainer.text = "";
        StartCoroutine(typeText(txt));
    }

    IEnumerator typeText(string txt)
    {
        char[] SentenceCharacters = txt.ToCharArray();
        foreach (char charline in SentenceCharacters)
        {
            if (skipDialogue)
            {
                dialogueTextContainer.text = txt;     // if we press space, we can end the dialogue early.
                Debug.Log("QuittingDialogue");
                yield return null;
                break;
            }
            dialogueTextContainer.text += charline;
            yield return new WaitForSeconds(textScrollTime); // when a character is done printing, wait for x seconds to print the next character
        }
        yield return null;                                  // do not get the previous space input.
        //yield return waitForKeyPress(KeyCode.Space);      // If you want to press spacebar to proceed to the next dialogue, uncomment this and make appropriate adjustments to the set buttons function.
        skipDialogue = false;
        yield return null;
    }

    /*
    public IEnumerator Speak(string dialogueLines, string name)
    {
        DialogueTextContainer.text = "";
        char[] SentenceCharacters = dialogueLines.ToCharArray();
        NameTextContainer.text = name;
        foreach (char charline in SentenceCharacters)
        {
            if (SkipDialogue)
            {
                DialogueTextContainer.text = dialogueLines;     // if we press space, we can end the dialogue early.
                Debug.Log("QuittingDialogue");
                yield return null;
                break;
            }
            DialogueTextContainer.text += charline;
            yield return new WaitForSeconds(TextScrollTime); // when a character is done printing, wait for x seconds to print the next character
        }
        yield return null;                                  // do not get the previous space input.
        //yield return waitForKeyPress(KeyCode.Space);      // If you want to press spacebar to proceed to the next dialogue, uncomment this and make appropriate adjustments to the set buttons function.
        SkipDialogue = false;
        yield return null;
        SetButtons(actionText, actions);        // set up next dialogue Button.
        // spawn next choice button here.
    }*/

}
