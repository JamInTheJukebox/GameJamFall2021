using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class DialogueTyper : MonoBehaviour
{
    [SerializeField] Image CharacterPortrait;
    [SerializeField] TextMeshProUGUI dialogueTextContainer;
    [SerializeField] GameObject NextDialogueIndicator;
    Animator dialogueAnimator;

    private bool skipDialogueTyping;
    private bool proceedToNextDialogue;
    int loadFadeIn, loadFadeOut;
    [SerializeField] float textScrollTime = 0.01f;
    Coroutine currentConversation;      // do not allow more than two conversations to go simultaneously.
    [SerializeField] AudioClip TypingSound;

    private void Awake()
    {
        if(dialogueTextContainer == null)
            dialogueTextContainer = GetComponent<TextMeshProUGUI>();
        dialogueTextContainer.text = "";
        dialogueAnimator = GetComponent<Animator>();
        loadFadeIn = Animator.StringToHash(AnimationTags.DIALOGUE_IN);
        loadFadeOut = Animator.StringToHash(AnimationTags.DIALOGUE_OUT);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SkipDialogue();
        }
    }
    public void TypeText(List<Conversation> typertest)
    {
        if(currentConversation == null)
        {
            dialogueTextContainer.text = "";
            currentConversation = StartCoroutine(typeText(typertest));
        }
    }

    public void TypeText(Conversation typertest)
    {
        if (currentConversation == null)
        {
            dialogueTextContainer.text = "";
            var temp = new List<Conversation>();
            temp.Add(typertest);
            currentConversation = StartCoroutine(typeText(temp));
        }
    }

    IEnumerator typeText(List<Conversation> typertest)
    {
        dialogueAnimator.Play(loadFadeIn);
        float maxTimeToPlaySound = 0.05f;
        float currentTimePlayingSound;
        foreach (Conversation type in typertest)
        {
            currentTimePlayingSound = 0;
            if (type.profile == null)
            {
                CharacterPortrait.gameObject.SetActive(false);
            }
            else
            {
                CharacterPortrait.gameObject.SetActive(true);
                CharacterPortrait.sprite = type.profile.GetCharacterSprite();
            }
            NextDialogueIndicator.SetActive(false);
            dialogueTextContainer.text = "";
            char[] SentenceCharacters = type.line.ToCharArray();
            foreach (char charline in SentenceCharacters)
            {
                if(currentTimePlayingSound <= 0)
                {
                    AudioManager.Instance.PlaySFX(TypingSound, 0.1f);
                    currentTimePlayingSound = maxTimeToPlaySound;
                }

                if (skipDialogueTyping)
                {
                    dialogueTextContainer.text = type.line;     // if we press space, we can end the dialogue early.
                    yield return null;
                    break;
                }
                currentTimePlayingSound -= Time.deltaTime;
                
                dialogueTextContainer.text += charline;
                yield return new WaitForSeconds(textScrollTime); // when a character is done printing, wait for x seconds to print the next character
            }
            yield return null;                                  // do not get the previous space input.
            NextDialogueIndicator.SetActive(true);
            yield return new WaitUntil(() => proceedToNextDialogue);
            skipDialogueTyping = false;
            proceedToNextDialogue = false;
            yield return null;
        }
        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        dialogueAnimator.Play(loadFadeOut);
        currentConversation = null;
    }

    public bool isTyping()
    {
        return currentConversation != null;
    }

    public void SkipDialogue()
    {
        if (MasterUserInterface.instance.isPaused())
            return;
        if (skipDialogueTyping)
            proceedToNextDialogue = true;
        skipDialogueTyping = true;
        
    }
}
