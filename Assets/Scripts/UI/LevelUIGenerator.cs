using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIGenerator : MonoBehaviour
{
    [SerializeField] GameObject LevelPrefab;
    int LevelsSoFar = 30;
    private void Awake()
    {
        for(int i = 0; i < LevelsSoFar; i++)
            Instantiate(LevelPrefab, transform);
    }

}
