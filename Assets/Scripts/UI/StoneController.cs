using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoneController : MonoBehaviour
{
    [SerializeField] List<GameObject> Stones;
    private GameObject m_currentStone;
    public GameObject currentStone
    {
        get { return m_currentStone; }
        set
        {
            if(value != m_currentStone)
            {
                m_currentStone = value;
                StonePNG.sprite = currentStone?.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    bool m_isActive = false;
    public bool isActive
    {
        get { return m_isActive; }
        set
        {
            if(m_isActive != value)
            {
                itemController.DisableItem();
            }
        }
    }

    private int m_currentIndex = 0;
    public int currentIndex
    {
        get { return m_currentIndex; }
        set
        {
            if(value != m_currentIndex)
            {
                m_currentIndex = value;
                if (m_currentIndex >= Stones.Count)
                    m_currentIndex = 0;
                else if (m_currentIndex < 0)
                    m_currentIndex = Stones.Count - 1;
                currentStone = Stones[m_currentIndex];
            }
        }
    }

    public Image StonePNG;
    DialogueTyper typer;

    ItemUIController itemController;
    [Header("Fail State")]
    public UnityEvent InvalidActivationAction;      // used in past/future;
    public float ScreenShakeDuration = 0.35f;
    public float ScreenShakePower = 0.17f;
    [SerializeField] Conversation FailConversation;
    public AudioClip TimeDistortionSFX;

    bool DisableStoneActivation;
    private void Start()
    {
        itemController = MasterUserInterface.instance.ItemUIController;
        StonePNG.sprite = currentStone?.GetComponent<SpriteRenderer>().sprite;
        typer = FindObjectOfType<DialogueTyper>();
    }
    public void activateStone()
    {

    }

    public void cycleStones(int delta)
    {
        currentIndex += delta;
    }
    public void useStone()
    {

    }

    public void enableStone()
    {
        if (typer.isTyping() || DisableStoneActivation) { return; }
        if(MasterUserInterface.instance.TimelineController.currentTime != Timeline.Present)
        {
            InvalidActivationAction.Invoke();
            ScreenShakeController.instance.ShakeScreen(0.35f,0.17f);
            Invoke("ShowWarning",0.2f);
            Invoke("EnableStoneActivation", 0.2f);
            AudioManager.Instance.PlaySFX(TimeDistortionSFX,0.9f);
            DisableStoneActivation = true;
            return;
        }
        itemController.DisableItem();
    }

    private void ShowWarning()
    {
        typer.TypeText(FailConversation);
    }
    private void EnableStoneActivation()
    {
        DisableStoneActivation = false;
    }
}
