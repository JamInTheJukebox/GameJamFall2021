using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoneController : MonoBehaviour
{
    #region Variables
    List<SpriteRenderer> Stones = new List<SpriteRenderer>();
    private SpriteRenderer m_currentStone;
    public SpriteRenderer currentStone
    {
        get { return m_currentStone; }
        set
        {
            if(value != m_currentStone)
            {
                m_currentStone = value;
                StonePNG.sprite = currentStone.sprite;
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

    public Image StonePNG;
    public Sprite DefaultStonePNG;
    // references
    DialogueTyper typer;
    StoneCollection stoneCollection;
    ItemUIController itemController;
    [SerializeField] Slider CooldownBar;

    float currentCooldownTime;
    float m_currentMaxCooldown = 0;
    float currentMaxCooldown
    {
        get { return m_currentMaxCooldown; }
        set
        {
            m_currentMaxCooldown = value;
            currentCooldownTime = value;
        }
    }
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
            foreach (var stone in stoneCollection.stoneCollection)
            {
                Stones.Add(stone.GetComponent<SpriteRenderer>());
            }
        typer = FindObjectOfType<DialogueTyper>();
        if (Stones.Count == 0)
            StonePNG.sprite = DefaultStonePNG;
        else
            StonePNG.sprite = Stones[currentIndex].sprite;
    }

    private void Update()
    {
        if(currentCooldownTime > 0)
        {
            currentCooldownTime -= Time.deltaTime;
            if(currentMaxCooldown != 0)
                CooldownBar.value = currentCooldownTime / currentMaxCooldown;
        }
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
        if(currentStone != null && currentCooldownTime <= 0)
            currentMaxCooldown = stoneCollection.UseStone(currentStone);
    }

    public void enableStone()
    {
        if (typer.isTyping() || DisableStoneActivation) { return; }
        if(MasterUserInterface.instance.TimelineController.currentTime != Timeline.Present)
        {
            ActivateInvalidUse();
            return;
        }
        itemController.DisableItem();
        useStone();
    }

    private void ActivateInvalidUse()
    {
        InvalidActivationAction?.Invoke();
        ScreenShakeController.instance.ShakeScreen(0.35f, 0.17f);
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
}
