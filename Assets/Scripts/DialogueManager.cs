using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public Queue<Dialogue> DialogueQueue;
    public Dialogue CurrentDialogue;
    public GameObject DialogueBox;

    public bool isDialogueActive = false;


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
        UI_Manager.instance.ShowDialogueBox();
        isDialogueActive = true;
    }

    public void LoadDialogue(Queue<Dialogue> dialogues)
    {
        DialogueQueue = new Queue<Dialogue>(dialogues);
    }

    public void NextDialogue()
    {
        if (CurrentDialogue.isInitial)
        {
            UI_Manager.instance.HideDialogueBox();
            isDialogueActive = false;

            GameManager.instance.ActivateRandomLine();
            DialogueQueue.Clear();
            return;
        }

        if (DialogueQueue.Count == 0)
        {
            UI_Manager.instance.HideDialogueBox();
            isDialogueActive = false;
            GameManager.instance.ApplyCallEffect();
            DialogueQueue.Clear();
            return;
        }

        CurrentDialogue = DialogueQueue.Dequeue();
        UI_Manager.instance.dialogueBox.SetDialogue(CurrentDialogue);
    }
}
