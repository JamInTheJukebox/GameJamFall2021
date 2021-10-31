using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueProfile", menuName = "ScriptableObjects/Dialogue/DialogueProfile", order = 2)]
public class DialogueProfile_SO : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] Sprite CharacterSprite;
    [SerializeField] string CharacterName;

    public Sprite GetCharacterSprite()
    {
        return CharacterSprite;
    }
    public string GetCharacterName()
    {
        return CharacterName;
    }
}
