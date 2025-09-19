using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public Queue<Dialogue> DialogueQueue;
    public Dialogue CurrentDialogue;
    public GameObject DialogueBox;

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

    public void SetInitialDialogue(Queue<Dialogue> dialogues)
    {
        DialogueQueue = new Queue<Dialogue>(dialogues);
        CurrentDialogue = DialogueQueue.Dequeue();

        UI_Manager.instance.dialogueBox.SetDialogue(CurrentDialogue);
        UI_Manager.instance.DialogueBoxObject.SetActive(true);
    }

    public void LoadDialogue(Queue<Dialogue> dialogues)
    {
        DialogueQueue = new Queue<Dialogue>(dialogues);
    }

    public void NextDialogue()
    {
        if (CurrentDialogue.isInitial)
        {
            UI_Manager.instance.DialogueBoxObject.SetActive(false);
            GameManager.instance.ActivateRandomLine();
            return;
        }

        if (DialogueQueue.Count == 0)
        {
            UI_Manager.instance.DialogueBoxObject.SetActive(false);
            return;
        }

        CurrentDialogue = DialogueQueue.Dequeue();
        UI_Manager.instance.dialogueBox.SetDialogue(CurrentDialogue);
    }
}
