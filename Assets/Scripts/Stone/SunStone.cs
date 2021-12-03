using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class SunStone : StoneBehavior
{
    public Light2D SunStoneLight;
    public GameObject BurningParticle;
    private ParticleSystem burningParticle;
    public GameObject SunStoneColliderPrefab;
    private GameObject SunStoneCollider;
    private void Awake()
    {
        burningParticle = Instantiate(BurningParticle).GetComponent<ParticleSystem>();
        burningParticle.Stop();
        SunStoneCollider = Instantiate(SunStoneColliderPrefab, burningParticle.transform);
    }

    public override void StoneInit()        // awake function
    {
        base.StoneInit();
    }

    public override void UseStone()
    {
        StoneActive = !StoneActive;

        base.UseStone();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            burningParticle.Stop();
            SunStoneCollider.SetActive(false);
        }
    }

    public override void ClickingBehavior()
    {
        //print("BURNING!!");
        // accept a collision
        // run a function in the collion(UNtil its event is complete);
        // if you collide with something, play this sound.
        SunStoneCollider.SetActive(true);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        burningParticle.transform.position = mousePos;
        burningParticle.Play();
    }
}
public enum StoneType
{
    SunStone = 0,
    MoonStone = 1,
}
