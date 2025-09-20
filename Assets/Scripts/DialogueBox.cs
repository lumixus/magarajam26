
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public Image CharacterImage;
    public TextMeshProUGUI CharacterName;
    public TextMeshProUGUI Content;
    public Button button;

    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        button = GetComponent<Button>();
    }

    public void Show()
    {
        anim.Play("Show");
        button.interactable = true;
    }

    public void Hide()
    {
        button.interactable = false;
        anim.Play("Hide");
    }

    public void SetDialogue(Dialogue dialogue)
    {
        CharacterImage.sprite = dialogue.character.Portrait;
        CharacterName.SetText(dialogue.character.displayName);
        Content.SetText(dialogue.content);
    }
}
