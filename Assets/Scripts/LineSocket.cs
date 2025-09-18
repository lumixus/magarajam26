using UnityEngine;

public class LineSocket : MonoBehaviour
{
    public SpriteRenderer sr;

    public bool isConnected = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ConnectSocket()
    {
        isConnected = true;
        sr.color = Color.black;
    }

    public void DisconnectSocket()
    {
        isConnected = false;
        sr.color = Color.white;
    }

}
