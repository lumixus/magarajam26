using UnityEngine;

public class LineSocket : MonoBehaviour
{
    public SpriteRenderer LightSR;
    public Sprite UsableLightSprite;
    public Sprite ActiveLightSprite;
    public Sprite DefaultLightSprite;
    public Sprite WrongLightSprite;

    public bool isUsable = false;

    public Entities entity;

    public bool isConnected = false;

    public void ConnectSocket(Socket sourceSocket)
    {
        if (isUsable)
        {
            if (GameManager.instance.selectedLine != null && GameManager.instance.selectedLine?.socket == sourceSocket)
            {
                LightSR.sprite = ActiveLightSprite;
            }
            else
            {
                LightSR.sprite = WrongLightSprite;
            }
        }
        else
        {
            LightSR.sprite = WrongLightSprite;
        }

        isConnected = true;
    }

    public void MarkAsUsable()
    {
        isUsable = true;
        LightSR.sprite = UsableLightSprite;
    }

    public void MarkAsDefault()
    {
        isUsable = false;
        LightSR.sprite = DefaultLightSprite;
    }

    public void DisconnectSocket()
    {
        isConnected = false;
        if (!isUsable)
        {
            LightSR.sprite = DefaultLightSprite;
        }
        else
        {
            LightSR.sprite = UsableLightSprite;
        }
    }

}
