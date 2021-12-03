using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public List<GameObject> LevelCracks = new List<GameObject>();

    private int m_Health = 4;
    public int Health
    {
        get { return m_Health; }

        set
        {
            if(value != m_Health)
            {
                m_Health = value;
                if(m_Health <= 0)
                {
                    FindObjectOfType<ScreenShakeController>().ShakeScreen(10, 0.2f);
                    Invoke("ChangeLevel",4f);
                }
            }
        }
    }

    private bool m_KitchiInPossession;
    public bool KitchiInPossession
    {
        get { return m_KitchiInPossession; }
        set
        {
            if(value != m_KitchiInPossession)
            {
                m_KitchiInPossession = value;
                Kitchi.SetActive(m_KitchiInPossession);
            }
        }
    }

    bool taughtPlayer = false;

    public GameObject Kitchi;


    public void ChangeKitchiState(bool state)
    {
        KitchiInPossession = state;
    }
    public void DecrementHealth()
    {
        Health--;
    }

    public void TurnOnCracks()
    {
        if(LevelCracks.Count > 0)
        {
            int index = Random.Range(0, LevelCracks.Count);
            LevelCracks[index].SetActive(true);
            LevelCracks.Remove(LevelCracks[index]);
            if (LevelCracks.Count > 0)
            {
                index = Random.Range(0, LevelCracks.Count);
                LevelCracks[index].SetActive(true);
                LevelCracks.Remove(LevelCracks[index]);
            }
        }
    }

    private void ChangeLevel()
    {
        GameManager.instance.ChangeScene(SceneNames.MAIN_MENU);
    }

}
