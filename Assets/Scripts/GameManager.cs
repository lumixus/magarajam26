using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GameStarted = false;

    public float Chaos = 0f;
    public float Authority = 0f;
    public float People = 0f;
    public float Order = 0f;

    public float StartDelay = 10f;
    public int TotalDay = 12;
    public int CurrentDay = 1;

    public List<SO_Call> Calls;
    public SO_Call selectedCall;
    public DialogueBranch selectedBranch;
    public Line selectedLine;

    public LineWrapper lineWrapper;
    public LineSockets lineSocketsWrapper;
    public Phone phone;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Calls = Resources.LoadAll<SO_Call>("Calls").ToList();
    }

    public void StartGame()
    {
        if (GameStarted)
        {
            return;
        }
        GameStarted = true;
        CurrentDay = 1;

        StartCoroutine(LoadNextCall());
    }


    IEnumerator LoadNextCall()
    {
        yield return new WaitForSeconds(StartDelay);

        int randomIndex = UnityEngine.Random.Range(0, Calls.Count);
        selectedCall = Calls[randomIndex];
        phone.RingPhone();

    }

    public void AnswerCall()
    {
        Dialogue InitialDialogue = new() { character = selectedCall.caller, content = selectedCall.InitialText, isInitial = true };
        Queue<Dialogue> DialogueQueue = new();
        DialogueQueue.Enqueue(InitialDialogue);
        DialogueManager.instance.SetInitialDialogue(DialogueQueue);
    }

    public void ActivateRandomLine()
    {
        GameObject selectedLineObject = lineWrapper.GetRandomLine();
        Line randomSelectedLine = selectedLineObject.GetComponent<Line>();

        List<GameObject> lineSockets = lineSocketsWrapper.GetLineSocketList();

        foreach (GameObject lineSocket in lineSockets)
        {
            LineSocket lineSocketScript = lineSocket.GetComponentInChildren<LineSocket>();

            foreach (DialogueBranch branch in selectedCall.DialogueBranches)
            {
                if (branch.entity.Equals(lineSocketScript.entity))
                {
                    lineSocketScript.MarkAsUsable();
                }
            }
        }

        randomSelectedLine.holeLight.LightOn();
        randomSelectedLine.socket.OnConnect.AddListener(OnSocketConnect);

        selectedLine = randomSelectedLine;
    }

    public void OnSocketConnect(LineSocket targetSocket)
    {
        if (!targetSocket.isUsable)
        {
            return;
        }

        DialogueBranch currentBranch = selectedCall.DialogueBranches.Find(branch => branch.entity == targetSocket.entity);

        selectedBranch = currentBranch;
        DialogueManager.instance.SetInitialDialogue(new Queue<Dialogue>(currentBranch.Dialogue));
    }

    public void ApplyCallEffect()
    {
        if (selectedBranch == null)
        {
            StartCoroutine(LoadNextCall());
            return;
        }

        switch (selectedBranch.dialogueEffect.stat)
        {
            case CityStats.ORDER:
                Order += selectedBranch.dialogueEffect.value;
                break;
            case CityStats.PEOPLE:
                People += selectedBranch.dialogueEffect.value;
                break;
            case CityStats.CHAOS:
                Chaos += selectedBranch.dialogueEffect.value;
                break;
            case CityStats.AUTHORITY:
                Authority += selectedBranch.dialogueEffect.value;
                break;
            default:
                break;
        }

        ResetLines();

        StartCoroutine(LoadNextCall());
    }

    public void ResetLines()
    {
        lineSocketsWrapper.ResetLineSockets();
        selectedLine.holeLight.LightOff();
        selectedLine.socket.OnConnect.RemoveAllListeners();
        selectedBranch = null;
        selectedLine = null;
        selectedCall = null;
    }
}
