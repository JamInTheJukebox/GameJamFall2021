using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
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
                    print("GAME WIN!!");
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

}
