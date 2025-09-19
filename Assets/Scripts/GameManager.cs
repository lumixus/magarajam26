using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GameStarted = false;

    public float StartDelay = 10f;
    public int TotalDay = 12;
    public int CurrentDay = 1;

    public List<SO_Call> Calls;
    public SO_Call selectedCall;
    public Line selectedLine;

    public LineWrapper lineWrapper;
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
        Line selectedLine = selectedLineObject.GetComponent<Line>();

        selectedLine.holeLight.LightOn();
    }
}
