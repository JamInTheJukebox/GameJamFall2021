using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehavior : MonoBehaviour
{
    [SerializeField] protected float MaxRefreshTime;            // if 0, keep stone active until we click the panel again!
    private float CurrentRefreshTime;
    private bool UseRefreshTime;
    private Animator stoneAnimator;
    private SpriteRenderer stoneSprite;
    protected bool StoneActive;           // for stones that have no cooldown.
    public StoneType TypeOfStone;
    public LayerMask InteractableLayer;

    private void Awake()
    {
        StoneInit();
    }

    public virtual void StoneInit()
    {
        stoneAnimator = GetComponent<Animator>();
        stoneSprite = GetComponent<SpriteRenderer>();
        CurrentRefreshTime = 0;
        UseRefreshTime = MaxRefreshTime != 0;
    }

    public virtual void UseStone()
    {
        print("Using " + gameObject.name);
        stoneAnimator.Play("StoneGlow2");
        CurrentRefreshTime = MaxRefreshTime;
    }

    public virtual void ClickingBehavior()
    {

    }

    public float GetCooldownTimeRatio()
    {
        if (!UseRefreshTime || MaxRefreshTime == 0)
            return 0;

        return CurrentRefreshTime / MaxRefreshTime;
    }
    
    public void EnableStone()
    {
        StoneActive = true;
    }

    public void DisableStone()
    {
        StoneActive = false;
    }

    public bool IsEnabled()
    {
        return StoneActive;
    }

    public Sprite GetSprite()
    {
        print(stoneSprite);
        return stoneSprite.sprite;
    }

    public void DecrementRefreshTime()
    {
        if (CurrentRefreshTime > 0)
        {
            CurrentRefreshTime -= Time.deltaTime;
        }
    }
}
