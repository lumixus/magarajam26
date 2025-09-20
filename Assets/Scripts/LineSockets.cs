using System.Collections.Generic;
using UnityEngine;

public class LineSockets : MonoBehaviour
{
    public List<GameObject> LineSocketList;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            LineSocketList.Add(child.gameObject);
        }
    }

    public List<GameObject> GetLineSocketList()
    {
        return LineSocketList;
    }

    public void ResetLineSockets()
    {
        foreach (GameObject lineSocket in LineSocketList)
        {
            LineSocket currentLineSocket = lineSocket.GetComponentInChildren<LineSocket>();
            currentLineSocket.MarkAsDefault();
        }
    }
}
