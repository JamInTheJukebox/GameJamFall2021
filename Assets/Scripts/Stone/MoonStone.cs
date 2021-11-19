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

    public override void StoneInit()        // awake function
    {
        base.StoneInit();
        OldIntensity = MoonLight.intensity;
    }

    public override void UseStone()
    {
        base.UseStone();
        gameObject.SetActive(true);
        if (stateChanged)
            MoonLight.intensity = OldIntensity;     // handle any changes here to sprites
        else
            MoonLight.intensity = NewIntensity;
        stateChanged = !stateChanged;
    }
}
