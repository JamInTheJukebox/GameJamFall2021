using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchiBehavior : MonoBehaviour
{
    public Transform Kitchi;
    public Transform Cage;

    public float RotateSpeed = 1;
    bool rotating;
    // rotate angle:

    private float rotateAngle = 0;

    public enum KitchiStates { Idle = 0, TurnAround = 1 };

    private KitchiStates m_kitchiStates;
    public KitchiStates kitchiStates
    {
        get { return m_kitchiStates; }
        set {
            print(value);
            if (value != m_kitchiStates)
            {
                m_kitchiStates = value;
                switch (m_kitchiStates)
                {
                    case KitchiStates.TurnAround:
                        int randomFactor = Random.Range(0, 2);
                        rotateAngle = randomFactor * 180;
                        rotating = true;
                        break;
                    default:
                        break;
                }
            }
            Invoke("TransitionToNewState", Random.Range(3, 5));
        }
    }

    private void Awake()
    {
        Invoke("TransitionToNewState", Random.Range(3, 5));
    }
    private void Update()
    {
        switch (kitchiStates)
        {
            case KitchiStates.Idle:
                break;
            case KitchiStates.TurnAround:
                RotateKitchi();
                break;
        }
    }

    private void RotateKitchi()
    {
        if (rotating)
        {
            var newRot = Vector3.up * rotateAngle;
            if (Vector3.Distance(Kitchi.eulerAngles, newRot) > 0.01f)
            {
                Kitchi.eulerAngles = Vector3.Lerp(Kitchi.rotation.eulerAngles, newRot, RotateSpeed * Time.deltaTime);
            }
            else
            {
                Kitchi.eulerAngles = newRot;
                rotating = false;
            }
        }
    }

    private void TransitionToNewState()
    {
        kitchiStates = (KitchiStates)Random.Range(0, 2);
    }
}
