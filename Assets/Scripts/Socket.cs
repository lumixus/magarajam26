using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{

    public bool isConnected = false;
    public CircleCollider2D circleCollider;
    public LineSocket connectedSocket = null;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void ConnectSocket(Vector2 position, LineSocket socket)
    {
        InputController.instance.targetObject = null;
        isConnected = true;
        transform.position = position;
        connectedSocket = socket;
    }

    public void OnDrop()
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        circleCollider.Cast(Vector2.zero, hits);

        if (hits.Count > 0)
        {

            if (hits[0].transform.CompareTag("LineSocket"))
            {
                LineSocket lineSocket = hits[0].transform.GetComponent<LineSocket>();

                if (!lineSocket.isConnected)
                {
                    lineSocket.ConnectSocket();
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
