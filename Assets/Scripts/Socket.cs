using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Socket : MonoBehaviour
{

    public bool isConnected = false;

    public UnityEvent<LineSocket> OnConnect;
    public BoxCollider2D boxCollider;
    public LineSocket connectedSocket = null;
    public SpriteRenderer sr;
    public Sprite connectedSprite;
    public Sprite defaultSprite;

    public Vector2 DisconnectedSocketCableHolder;
    public Vector2 ConnectedSocketCableHolder;

    public GameObject SocketCableHolder;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void ConnectSocket(Vector2 position, LineSocket socket)
    {
        InputController.instance.targetObject = null;
        isConnected = true;
        transform.position = position;
        connectedSocket = socket;
        SocketCableHolder.transform.localPosition = ConnectedSocketCableHolder;
        sr.sprite = connectedSprite;
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
            else
            {
                transform.localPosition = Vector2.zero;
            }

        }
        else
        {
            transform.localPosition = Vector2.zero;
        }
    }

    public void DisconnectSocket()
    {
        isConnected = false;
        connectedSocket.DisconnectSocket();
        SocketCableHolder.transform.localPosition = DisconnectedSocketCableHolder;
        sr.sprite = defaultSprite;
    }
}
