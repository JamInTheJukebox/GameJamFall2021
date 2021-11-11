using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCollection : MonoBehaviour
{
    public List<StoneBehavior> stoneCollection = new List<StoneBehavior>();
    private Dictionary<SpriteRenderer, StoneBehavior> stoneDictionary = new Dictionary<SpriteRenderer, StoneBehavior>();
    private void Awake()
    {
        foreach(Transform stone in transform)
        {
            print(stone);
            if(stone.GetComponent<StoneBehavior>() != null)
            {
                var stoneBehavior = stone.GetComponent<StoneBehavior>();
                stoneCollection.Add(stoneBehavior);
                stoneDictionary.Add(stone.GetComponent<SpriteRenderer>(), stoneBehavior);
            }
        } 
    }

    public float UseStone(SpriteRenderer stone)
    {
        stone.gameObject.SetActive(true);
        return stoneDictionary[stone].UseStone();
    }


}
