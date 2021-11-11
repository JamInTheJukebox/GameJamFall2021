using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehavior : MonoBehaviour
{
    [SerializeField] protected float RefreshTime;            // if 0, keep stone active until we click the panel again!
    Animator stoneAnimator;
    private void Awake()
    {
        InitializeAnimator();
    }

    protected void InitializeAnimator()
    {
        stoneAnimator = GetComponent<Animator>();
    }

    public virtual float UseStone()
    {
        print("Using " + gameObject.name);
        Invoke("DisableStone", 2f);
        return RefreshTime;
    }

    public float GetCooldownTime()
    {
        return RefreshTime;
    }
    
    private void DisableStone()
    {
        gameObject.SetActive(false);
    }
}
