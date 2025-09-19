using System.Collections.Generic;
using UnityEngine;

public class LineWrapper : MonoBehaviour
{
    public List<GameObject> Lines;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            Lines.Add(child.gameObject);
        }
    }

    public GameObject GetRandomLine()
    {
        int randomIndex = Random.Range(0, Lines.Count);

        return Lines[randomIndex];
    }
}
