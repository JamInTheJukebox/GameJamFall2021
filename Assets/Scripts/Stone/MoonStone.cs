using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class MoonStone : StoneBehavior
{
    public Light2D MoonLight;

    public float NewIntensity;
    private float OldIntensity;
    private bool stateChanged;

    public UnityEvent moonStoneCallback;

    public override void StoneInit()        // awake function
    {
        base.StoneInit();
        if (MoonLight != null)
            OldIntensity = MoonLight.intensity;
        else
            Debug.LogWarning("MoonStone.cs: Moonlight entity Missing.");
    }

    public override void UseStone()
    {
        if(MoonLight == null) { return; }
        base.UseStone();
        gameObject.SetActive(true);
        if (stateChanged)
            MoonLight.intensity = OldIntensity;     // handle any changes here to sprites
        else
            MoonLight.intensity = NewIntensity;
        stateChanged = !stateChanged;
        moonStoneCallback?.Invoke();
    }
    public bool getStateChanged()
    {
        bool copy_stateChanged = stateChanged;
        return copy_stateChanged;
    }
}
