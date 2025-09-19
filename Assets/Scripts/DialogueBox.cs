
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public Image CharacterImage;
    public TextMeshProUGUI CharacterName;
    public TextMeshProUGUI Content;
    public void SetDialogue(Dialogue dialogue)
    {
        CharacterImage.sprite = dialogue.character.Portrait;
        CharacterName.SetText(dialogue.character.displayName);
        Content.SetText(dialogue.content);
    }
}
