using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderScript : MonoBehaviour
{
    public const string LAYER_NAME = "Player";
    public int sortingOrder = 0;
    private SpriteRenderer player;

    void Awake()
    {
        player = GetComponent<SpriteRenderer>();
        SetSortingLayer(LAYER_NAME);
    }

    public void SetSortingLayer(string newLayer)
    {
        player.sortingOrder = sortingOrder;
        player.sortingLayerName = newLayer;
    }
}
