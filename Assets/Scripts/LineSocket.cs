using UnityEngine;

public class LineSocket : MonoBehaviour
{
    public SpriteRenderer LightSR;
    public Sprite UsableLightSprite;
    public Sprite ActiveLightSprite;
    public Sprite DefaultLightSprite;

    public bool isConnected = false;

    void Start()
    {
        LightSR = GetComponent<SpriteRenderer>();
    }

    public void ConnectSocket()
    {
        isConnected = true;
        LightSR.sprite = ActiveLightSprite;
    }

    public void MarkAsUsable()
    {
        LightSR.sprite = UsableLightSprite;
    }

    public void DisconnectSocket()
    {
        isConnected = false;
        LightSR.sprite = DefaultLightSprite;
    }

}
