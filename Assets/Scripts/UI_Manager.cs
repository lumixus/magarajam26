using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public GameObject DialogueBoxObject;
    public DialogueBox dialogueBox;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ShowDialogueBox()
    {
        dialogueBox.Show();
    }
    
    public void HideDialogueBox()
    {
        dialogueBox.Hide();
    }
}
