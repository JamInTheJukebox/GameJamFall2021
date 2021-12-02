using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Moonstone_lvl6 : MonoBehaviour
{
    public GameObject cracked_sun_block;
    public UnityEvent MyUnityEvent;
    public MoonStone moonstone;

    public bool complete;
    private void Awake()
    {
        moonstone = FindObjectOfType<MoonStone>();
    }

    private void Update()
    {
        bool hot_block_active = cracked_sun_block.activeInHierarchy;
        bool current_state = moonstone.getStateChanged();
        if (current_state && hot_block_active && !complete){
            complete = true;
            MyUnityEvent?.Invoke();
        }
    }
}
