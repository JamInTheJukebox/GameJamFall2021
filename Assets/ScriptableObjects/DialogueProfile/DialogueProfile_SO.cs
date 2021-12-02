using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueProfile", menuName = "ScriptableObjects/Dialogue/DialogueProfile", order = 2)]
public class DialogueProfile_SO : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] List<Sprite> CharacterSprites = new List<Sprite>();
    [SerializeField] string CharacterName;

    public Sprite GetCharacterSprite(int SpriteIndex = 0)
    {
        return CharacterSprites[SpriteIndex];
    }
    public string GetCharacterName()
    {
        return CharacterName;
    }
}
