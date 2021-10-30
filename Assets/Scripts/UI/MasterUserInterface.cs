using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterUserInterface : MonoBehaviour
{
    // all calls to any UI object should talk to this script first.
    public static MasterUserInterface instance;
    [SerializeField] TimelineController timelineController;
    [SerializeField] ItemUIController itemUIController;
    [SerializeField] Timer timerController;
    [SerializeField] PauseMenu pauseMenu;

    public TimelineController TimelineController { get => timelineController; set => timelineController = value; }
    public ItemUIController ItemUIController { get => itemUIController; set => itemUIController = value; }
    public Timer TimerController { get => timerController; set => timerController = value; }
    public PauseMenu PauseMenu { get => pauseMenu; set => pauseMenu = value; }

    private void Awake()
    {
        instance = this;
    }

    public void enablePausing(bool newState)
    {
        PauseMenu.enablePausing(newState);
    }
}
