using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCall", menuName = "Calls/New Call")]
public class SO_Call : ScriptableObject
{
    public SO_Character caller;

    [TextArea(5, 20)]
    public string InitialText;

    public List<DialogueBranch> DialogueBranches;



}


[Serializable]
public class DialogueBranch
{
    public Entities entity;
    public DialogueEffect dialogueEffect;
    public List<Dialogue> Dialogue;
}

[Serializable]
public class Dialogue
{
    public SO_Character character;
    public bool isInitial;
    [TextArea(5, 20)]
    public string content;
}

[Serializable]
public class DialogueEffect
{
    public CityStats stat;
    public float value;
}
