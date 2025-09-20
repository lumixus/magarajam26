using UnityEngine;

public class SwitchButton : MonoBehaviour, IInteractable
{
    public bool isPressed = false;
    public Sprite pressedSprite;
    public Sprite defaultSprite;
    public SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void Interact()
    {
        isPressed = true;
        sr.sprite = pressedSprite;
        GameManager.instance.StartGame();
    }
}
