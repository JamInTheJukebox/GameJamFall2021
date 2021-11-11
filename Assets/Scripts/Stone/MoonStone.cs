using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class MoonStone : StoneBehavior
{
    public Light2D MoonLight;

    public float NewIntensity;
    private float OldIntensity;
    private bool stateChanged;

    private void Awake()
    {
        InitializeAnimator();
        OldIntensity = MoonLight.intensity;
    }

    public override float UseStone()
    {
        if (stateChanged)
            MoonLight.intensity = OldIntensity;     // handle any changes here to sprites
        else
            MoonLight.intensity = NewIntensity;
        stateChanged = !stateChanged;
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
