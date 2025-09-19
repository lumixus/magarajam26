using UnityEngine;

public class SwitchButton : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.instance.StartGame();
    }
}
