using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public GameObject DialogueBoxObject;
    public DialogueBox dialogueBox;
    public GameObject DayIndicator;

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

    public void ShowDayIndicator(int day)
    {
        DayIndicator.GetComponentInChildren<TextMeshProUGUI>().SetText($"Gün {day}");
        DayIndicator.GetComponent<Animator>().Play("SHOW");
    }
}
