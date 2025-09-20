using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Socket : MonoBehaviour
{

    public bool isConnected = false;

    public UnityEvent<LineSocket> OnConnect;
    public BoxCollider2D boxCollider;
    public LineSocket connectedSocket = null;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ConnectSocket(Vector2 position, LineSocket socket)
    {
        InputController.instance.targetObject = null;
        isConnected = true;
        transform.position = position;
        connectedSocket = socket;

        OnConnect?.Invoke(socket);
    }

    public void OnDrop()
    {
        List<RaycastHit2D> hits = new();
        boxCollider.Cast(Vector2.zero, hits);

        if (hits.Count > 0)
        {

            if (hits[0].transform.CompareTag("LineSocket"))
            {
                LineSocket lineSocket = hits[0].transform.GetComponent<LineSocket>();

                if (!lineSocket.isConnected)
                {
                    lineSocket.ConnectSocket(this);
                    ConnectSocket(lineSocket.transform.position, lineSocket);
                }
            }
        
        }
    }

    public void DisconnectSocket()
    {
        isConnected = false;
        connectedSocket.DisconnectSocket();
    }
}
