using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class SunStone : StoneBehavior
{
    public Light2D SunStoneLight;

    public float NewIntensity;
    private float OldIntensity;
    private bool stateChanged;

    private void Awake()
    {
        InitializeAnimator();

    }

    public override float UseStone()
    {
        return base.UseStone();         // return the cooldown time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseStone();
        }
    }
}
