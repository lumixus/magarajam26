using UnityEngine;

public class Line : MonoBehaviour
{

    public Vector2 CableOffset;
    public bool isConnected = false;
    public GameObject SocketStart;
    public Socket socket;
    public HoleLight holeLight;
    public GameObject SocketCableHolder;
    public LineRenderer cable;
    void Start()
    {
        cable = SocketStart.GetComponent<LineRenderer>();
        cable.SetPosition(0, new Vector3(SocketCableHolder.transform.position.x, SocketCableHolder.transform.position.y, 0f));
    }

    void Update()
    {
        cable.SetPosition(1, SocketCableHolder.transform.position);
    }


}
