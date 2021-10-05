using UnityEngine;

// the purpose of this script is so when you click on something that is interactable, a dialogue box will popup and use the properties set by this object
[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/DialogueOption", order = 1)]
public class DialogueOptions_SO : ScriptableObject
{
    public Color textColor;
    public float TextSize;
    public bool shakingText;
    public bool hoveringText;
}
