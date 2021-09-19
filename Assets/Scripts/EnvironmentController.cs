using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] GameObject[] timelineEnvironments = new GameObject[3];
    [SerializeField] Image environmentOverlay;
    private Dictionary<Timeline, GameObject> timelineDictionary = new Dictionary<Timeline, GameObject>();
    TimelineController timelineController;
    [Header("Environment Overlay")]
    [SerializeField] float overlayFadeDuration;
    public Color overlayColor;

    private void Awake()
    {
        timelineController = FindObjectOfType<TimelineController>();
        timelineDictionary.Add(Timeline.Past, timelineEnvironments[0]);
        timelineDictionary.Add(Timeline.Present, timelineEnvironments[1]);
        timelineDictionary.Add(Timeline.Future, timelineEnvironments[2]);
    }

    public void StartTransition()
    {
        timelineController.DisableInteractableButtons();
        FadeOverlay();
    }
    void FadeOverlay()
    {
        StartCoroutine(OverlayFadeColor(1));
    }

    public void SetEnvironment(Timeline newTimeline, bool fadeOut = false)
    {
        foreach(GameObject env in timelineEnvironments)
        {
            if(env == timelineDictionary[newTimeline])
            {
                env.SetActive(true);
            }
            else
            {
                env.SetActive(false);
            }
        }
        if (fadeOut)
        {
            StartCoroutine(OverlayFadeColor(0));
        }
    }

    IEnumerator OverlayFadeColor(float endAlpha)
    {
        float t = 0;
        float currentAlpha = environmentOverlay.color.a;

        while (t < 1)
        {
            t += Time.deltaTime/overlayFadeDuration;
            currentAlpha = Mathf.Lerp(currentAlpha, endAlpha, t);
            environmentOverlay.color = new Color(overlayColor.r, overlayColor.g, overlayColor.b, currentAlpha);
            yield return new WaitForEndOfFrame();
        }
        if (endAlpha == 1)
        {
            timelineController.ReadyToChangeBackground();
        }
        else if(endAlpha == 0)
        {
            timelineController.CanChangeTimelinesAgain();
        }
    }
}
