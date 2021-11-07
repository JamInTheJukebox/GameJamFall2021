using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// test 2
public class TimelineController : MonoBehaviour
{
    [SerializeField] Timeline m_currentTime = Timeline.Present;

    public Timeline currentTime
    {
        get
        {
            return m_currentTime;
        }
        set
        {
            if(value != m_currentTime)
            {
                m_currentTime = value;
                // set timer to avoid changing time again during transition.
                isChangingTime = true;
                TimelineChangeListener.Invoke(false);
                SetActiveButtonColor();
                //testPlayerAnim.StartTransition();
                envController.StartTransition();
                StartCoroutine(ClockFadeColor(GetActiveButtonColor().pressedColor, 0, true));
            }
        }
    }


    [SerializeField] Button[] TimelineButtons = new Button[3];
    private Dictionary<Timeline, Button> TimelineDictionary = new Dictionary<Timeline,Button>();
    private float TestTimer = 4;
    [SerializeField] Color buttonDisabledColor;
    [Header("Clock Image")]
    [SerializeField] Image ClockPNG;
    public float DefaultClockFadeSpeed;
    // references
    EnvironmentController envController;
    Test_AnimationTimeline testPlayerAnim;

    public delegate void changingTimelineListener(bool state);     // false = changing time, true = done.
    public changingTimelineListener TimelineChangeListener;
    bool isChangingTime = false;
    // Start is called before the first frame update
    void Awake()
    {
        TimelineButtons[0].onClick.AddListener(() => { SwitchTimelines(Timeline.Past); });
        TimelineButtons[1].onClick.AddListener(() => { SwitchTimelines(Timeline.Present); });
        TimelineButtons[2].onClick.AddListener(() => { SwitchTimelines(Timeline.Future); });

        TimelineDictionary.Add(Timeline.Past, TimelineButtons[0]);
        TimelineDictionary.Add(Timeline.Present, TimelineButtons[1]);
        TimelineDictionary.Add(Timeline.Future, TimelineButtons[2]);

        SetActiveButtonColor();
        EnableInteractableButtons();
        //references
        envController = FindObjectOfType<EnvironmentController>();
        testPlayerAnim = FindObjectOfType<Test_AnimationTimeline>();
    }

    private void Start()
    {
        envController.SetEnvironment(currentTime);
    }

    void SetActiveButtonColor()
    {
        Color tempColor = TimelineDictionary[currentTime].colors.pressedColor * 0.8f;
        Color newTimeColor = new Color(tempColor.r, tempColor.g, tempColor.b, 1f);
        var buttonColor = TimelineDictionary[currentTime].colors;
        buttonColor.normalColor = newTimeColor;
        buttonColor.disabledColor = newTimeColor;
        TimelineDictionary[currentTime].colors = buttonColor;
    }

    private void SwitchTimelines(Timeline newTimeline)      // made this a function incase we ever want keybinds(press 1 for past, 2 for present, 3 for future)
    {
        currentTime = newTimeline;
    }

    public void EnableInteractableButtons()
    {
        foreach (Button button in TimelineButtons)
        {
            if (button != TimelineDictionary[currentTime])
            {
                button.interactable = true;        // prevent the player from switching to the same timeline.
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    public void DisableInteractableButtons()
    {
        foreach (Button button in TimelineButtons)
        {
            button.interactable = false;        // prevent the player from switching timelines too quickly(Wait for the animation transition to play).
            var _color = button.colors;
            _color.normalColor = Color.white;
            if (button != TimelineDictionary[currentTime])
                _color.disabledColor = buttonDisabledColor;
            button.colors = _color;
        }
    }

    ColorBlock GetActiveButtonColor()
    {
        return TimelineDictionary[currentTime].colors;
    }

    IEnumerator ClockFadeColor(Color endColor, float speed = 0,  bool instant = false)
    {
        if(speed == 0)
        {
            speed = DefaultClockFadeSpeed;
        }
        if (instant)
        {
            ClockPNG.color = endColor;
        }
        Color startColor = ClockPNG.color;
        float additive = 0;

        while (ClockPNG.color != endColor)
        {
            additive += speed * Time.deltaTime;
            var newColor = Color.Lerp(startColor, endColor, additive);
            ClockPNG.color = newColor;
            yield return null;
        }
    }

    public void ReadyToChangeBackground()
    {
        envController.SetEnvironment(currentTime,true);
    }

    public void CanChangeTimelinesAgain()
    {
        EnableInteractableButtons();
        StartCoroutine(ClockFadeColor(Color.white, 20f));
        isChangingTime = false;
        TimelineChangeListener.Invoke(true);                             // timeline transition complete!
    }
}
