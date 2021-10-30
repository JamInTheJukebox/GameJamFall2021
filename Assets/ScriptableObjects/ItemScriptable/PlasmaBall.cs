using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInformation", menuName = "ScriptableObjects/ItemScriptable/PlasmaBall", order = 3)]
public class PlasmaBall : ItemScriptable
{
    public GameObject projectile;

    public override void UseItem()
    {
        try
        {
            base.UseItem();
            Instantiate(projectile);
        }
        catch
        {
            Debug.LogWarning("PlasmaBall.cs error: CAST UNSUCCESSFUL");
        }
    }

    public void init(Waterbucket SO)
    {
        base.init(SO);

    }
}
