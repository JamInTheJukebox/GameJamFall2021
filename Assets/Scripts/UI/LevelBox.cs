using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LevelTextIndicator;        // what number level it is
    public void Start()
    {
        int index = transform.GetSiblingIndex() + 1;
        LevelTextIndicator.text = index.ToString();
        if (GameManager.instance != null)
            GetComponent<Button>().onClick.AddListener(() => GameManager.instance.ChangeScene("Level_" + index));
        else
            Debug.LogWarning("LevelBox.cs error: GameManager instance not present! Button listener not created!");
    }


}
