using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Magic Stone", menuName = "ScriptableObjects/MagicStone/", order = 1)]
public class Stone_SO : ScriptableObject
{
    [SerializeField] SpriteRenderer defaultStonePNG;
    public float coolDown;

    public virtual void UseStone()
    {

    }
}
enum StoneTypes
{
    defaultStone = 0,
    Sun = 1,
    Moon = 2
}