using UnityEngine;

public class Line : MonoBehaviour
{

    public Vector2 CableOffset;
    public bool isConnected = false;
    public GameObject SocketStart;
    public LineRenderer cable;
    void Start()
    {
        cable = SocketStart.GetComponent<LineRenderer>();
        cable.SetPosition(0, new Vector3(SocketStart.transform.position.x, CableOffset.y, 0f));
    }

    void Update()
    {
        cable.SetPosition(1, SocketStart.transform.position);
    }


}
