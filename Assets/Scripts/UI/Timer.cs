using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    float m_elapsedTime;
    public float elapsedTime
    {
        get {return m_elapsedTime; }
        set
        {
            if(m_elapsedTime != value)
            {

                m_elapsedTime = Mathf.Clamp(value, 0, MaxTime);
                if(timerText != null)
                {
                    int minutes = (int)m_elapsedTime / 60;
                    int seconds = (int)m_elapsedTime - 60 * minutes;
                    timerText.text =  string.Format("{0:00}:{1:00}", minutes, seconds);
                }
            }
        }
    }
    const float MaxTime = 5999;

    private TextMeshProUGUI timerText;
    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.instance.enablePausing(true);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;    
    }
}
