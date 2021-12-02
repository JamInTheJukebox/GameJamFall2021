using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoneController : MonoBehaviour
{
    #region Variables
    List<StoneBehavior> Stones = new List<StoneBehavior>();
    private StoneBehavior m_currentStone;
    public StoneBehavior currentStone
    {
        get { return m_currentStone; }
        set
        {
            if(value != m_currentStone)
            {
                if(m_currentStone != null)
                {
                    m_currentStone.DisableStone();
                    isUsingStone = false;
                }
                m_currentStone = value;
                UpdateStoneSprite();
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
                if (Stones.Count > 0)
                    currentStone = Stones[m_currentIndex];
                else
                    currentStone = null;
            }
        }
    }

    private bool m_isUsingStone;
    public bool isUsingStone
    {
        get { return m_isUsingStone; }
        set
        {
            if(value != m_isUsingStone)
            {
                m_isUsingStone = value;
                SetStoneButtonColor((m_isUsingStone) ? (StoneInUseButtonColor) : (DefaultButtonColor));
            }
        }
    }
    public Image StonePNG;
    public Sprite DefaultStonePNG;
    // references
    DialogueTyper typer;
    StoneCollection stoneCollection;
    ItemUIController itemController;
    [SerializeField] Slider CooldownBar;
    [Header("Button Attributes")]
    [SerializeField] Image StoneButton;
    [SerializeField] Color DefaultButtonColor;
    [SerializeField] Color StoneInUseButtonColor;

    [Header("Fail State")]
    public UnityEvent InvalidActivationAction;      // used in past/future;
    public float ScreenShakeDuration = 0.35f;
    public float ScreenShakePower = 0.17f;
    [SerializeField] Conversation FailConversation;
    public AudioClip TimeDistortionSFX;
    bool DisableStoneActivation;
#endregion
    private void Start()
    {
        itemController = MasterUserInterface.instance.ItemUIController;
        stoneCollection = FindObjectOfType<StoneCollection>();
        if(stoneCollection)
        {
            foreach (var stone in stoneCollection.stoneCollection)
                Stones.Add(stone);
        }

        if (Stones.Count != 0)
            currentStone = Stones[0];
        typer = FindObjectOfType<DialogueTyper>();
        MasterUserInterface.instance.TimelineController.TimelineChangeListener += SwitchingTimelines;
    }

    private void Update()
    {
        if(currentStone != null)
            CooldownBar.value = currentStone.GetCooldownTimeRatio();
    }

    public void cycleStones(int delta)
    {
        currentIndex += delta;
    }

    public void enableStone()
    {
        if (CannotUseStone()) { return; }
        if(MasterUserInterface.instance.TimelineController.currentTime != Timeline.Present)
        {
            ActivateInvalidUse();
            return;
        }
        itemController.DisableItem();

        currentStone.UseStone();
        isUsingStone = currentStone.IsEnabled();
    }

    public void useStone()
    {
        currentStone.ClickingBehavior();
    }

    private bool CannotUseStone()
    {
        if (currentStone == null)
            return true;
        return (typer.isTyping() || DisableStoneActivation || currentStone.GetCooldownTimeRatio() > 0);
    }

    private void ActivateInvalidUse()
    {
        if(InvalidActivationAction == null) { return; }
        InvalidActivationAction?.Invoke();
        if(ScreenShakeController.instance != null)
        {
            ScreenShakeController.instance.ShakeScreen(0.35f, 0.17f);
        }
        Invoke("ShowWarning", 0.2f);
        Invoke("EnableStoneActivation", 0.2f);
        AudioManager.Instance.PlaySFX(TimeDistortionSFX, 0.9f);
        DisableStoneActivation = true;
    }

    private void ShowWarning()
    {
        typer.TypeText(FailConversation);
    }

    private void EnableStoneActivation()
    {
        DisableStoneActivation = false;
    }

    private void UpdateStoneSprite()
    {
        if (Stones.Count == 0 || currentStone == null)
            StonePNG.sprite = DefaultStonePNG;
        else
        {
            StonePNG.sprite = currentStone.GetSprite();
        }
    }

    private void SwitchingTimelines(bool state)       // disable the stone whenever you switch timelines.
    {
        DisableStone();
    }

    private void SetStoneButtonColor(Color newColor)
    {
        StoneButton.color = newColor;
    }

    public void DisableStone()
    {
        if (currentStone != null)
        {
            currentStone.DisableStone();
            isUsingStone = false;
        }
    }
}
