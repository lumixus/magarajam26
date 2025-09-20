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
    public int CurrentDay = 0;

    public List<SO_Call> Calls;
    public List<SO_Day> Days;

    public List<SO_Call> EventCalls;
    public List<SO_Call> NormalCalls;
    public SO_Day CurrentDaySO;
    public SO_Call selectedCall;

    public int TotalCallCount = 1;
    public int CallCounter = 1;

    public bool isEventCallPassed = false;


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
        Days = Resources.LoadAll<SO_Day>("Days").ToList();

        EventCalls = Calls.FindAll(call => call.type == CallType.EVENT);
        NormalCalls = Calls.FindAll(call => call.type == CallType.NORMAL);

    }

    public void StartGame()
    {
        if (GameStarted)
        {
            return;
        }
        GameStarted = true;
        CurrentDay = 0;
        CallCounter = 1;
        TotalDay = Days.Count;
        CurrentDaySO = Days[CurrentDay];
        UI_Manager.instance.ShowDayIndicator(CurrentDay + 1);


        StartCoroutine(LoadNextCall());
    }

    public void GetNextDay()
    {
        CurrentDay++;
        if (CurrentDay >= TotalDay)
        {
            // Finish the Game
            return;
        }

        CallCounter = 1;
        isEventCallPassed = false;
        CurrentDaySO = Days[CurrentDay];
        UI_Manager.instance.ShowDayIndicator(CurrentDay + 1);
        if (CurrentDaySO.GetRandomCount)
        {
            TotalCallCount = CurrentDaySO.GetRandomCallCount();
        }
        else
        {
            TotalCallCount = CurrentDaySO.MinimumCallCount;
        }
    }


    IEnumerator LoadNextCall()
    {

    if (CallCounter >= TotalCallCount) {
            yield return null;
    }

        yield return new WaitForSeconds(StartDelay);

        float completionProgress = (float)CallCounter / (float)TotalCallCount;
        float eventPossibility = UnityEngine.Random.Range(0f, 1f);

        bool isEventCall = eventPossibility <= completionProgress;

        if (isEventCall && !isEventCallPassed)
        {
            int eventIndex = UnityEngine.Random.Range(0, EventCalls.Count);
            selectedCall = EventCalls[eventIndex];
            isEventCallPassed = true;
        }
        else
        {
            int randomIndex = UnityEngine.Random.Range(0, NormalCalls.Count);
            selectedCall = NormalCalls[randomIndex];
        }


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

    public void OnCallEnded()
    {
        bool isEventCall = selectedCall.type == CallType.EVENT;
        if (isEventCall)
        {
            EventCalls.Remove(selectedCall);
        }

        phone.ClosePhone();

        CallCounter++;
        if (CallCounter > TotalCallCount)
        {
            GetNextDay();
        }
    }

    public void ApplyCallEffect()
    {
        if (selectedBranch == null)
        {
            StartCoroutine(LoadNextCall());
            OnCallEnded();
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

        OnCallEnded();

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
