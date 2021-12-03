using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCollection : MonoBehaviour
{
    public List<StoneBehavior> stoneCollection = new List<StoneBehavior>();

    private void Awake()
    {
        foreach(Transform stone in transform)
        {
            if (!stone.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(stone.GetComponent<StoneBehavior>() != null)
            {
                var stoneBehavior = stone.GetComponent<StoneBehavior>();
                stoneBehavior.StoneInit();
                stoneCollection.Add(stoneBehavior);
            }
        } 
    }

    private void Update()
    {
        foreach(StoneBehavior stone in stoneCollection)
        {
            stone.DecrementRefreshTime();
        }
    }
}
