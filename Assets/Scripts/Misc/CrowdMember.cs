using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMember : MonoBehaviour
{
    [Tooltip("1 == idle animation, 2 == head bob, else nothing")] public int CrowdState;
    Animator CrowdAnimator;
    [Header("hat animation")]
    [SerializeField] Transform AnimateHat;      // if not null, animate the hat
    [SerializeField] Vector2 HatFrequency;
    [SerializeField] Vector2 HatAmplitude;
    float t;
    Vector3 initialScale;
    public void Awake()
    {
        CrowdAnimator = GetComponent<Animator>();
        if(AnimateHat)
            initialScale = AnimateHat.localScale;
    }

    private void OnEnable()
    {
        switch (CrowdState)
        {
            case 1:
                CrowdAnimator.Play("IdleCrowd");
                break;
            case 2:
                CrowdAnimator.Play("HeadBob");
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (AnimateHat)
        {
            t += Time.deltaTime;

            var xScale = initialScale.x * HatAmplitude.x*(Mathf.Cos(HatFrequency.x * t) + 1);
            var yScale = initialScale.y * HatAmplitude.y * (Mathf.Sin(HatFrequency.y * t) + 1);
            AnimateHat.localScale = initialScale + new Vector3(xScale, yScale, 0);
        }
    }
}
