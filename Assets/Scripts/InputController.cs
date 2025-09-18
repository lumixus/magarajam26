using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    public static InputController instance;
    public InputSystem_Actions inputActions;
    public LayerMask LineMask;
    public GameObject targetObject;
    public bool leftMouseButton = false;
    public Vector2 mousePos = Vector2.zero;
    void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        inputActions = new InputSystem_Actions();

        inputActions.Player.DragAndMove.started += (ctx) => OnLeftClickStart();
        inputActions.Player.DragAndMove.canceled += (ctx) => OnLeftClickCanceled();
        inputActions.Player.MousePos.performed += (ctx) => mousePos = ctx.ReadValue<Vector2>();


        inputActions.Enable();
    }
    void Start()
    {
        inputActions.Enable();
    }

    void OnLeftClickCanceled()
    {
        if (targetObject)
        {
            if (targetObject.CompareTag("Socket"))
            {
                targetObject.GetComponent<Socket>().OnDrop();
            }
        }
        
        targetObject = null;
        leftMouseButton = false;
    }

    void OnLeftClickStart()
    {
        leftMouseButton = true;
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero, 1f, LineMask);

        if (hit)
        {
            if (hit.transform.CompareTag("Socket"))
            {
                Socket socketScript = hit.transform.GetComponent<Socket>();
                if (socketScript.isConnected)
                {
                    socketScript.DisconnectSocket();
                }
                targetObject = hit.transform.gameObject;
            }
        }

    }


    void Update()
    {
        if (targetObject)
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            targetObject.transform.position = new Vector3(targetPos.x, targetPos.y, 0f);
        }
    }

}
