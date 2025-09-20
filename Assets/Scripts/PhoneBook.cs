using UnityEngine;

public class PhoneBook : MonoBehaviour, IInteractable
{
    public Animator anim;
    public GameObject PhoneBookObject;
    public bool isOpened = false;

    void Awake()
    {
        anim = PhoneBookObject.GetComponent<Animator>();
    }
    public void Interact()
    {
        isOpened = !isOpened;
        if (isOpened)
        {
            anim.Play("FadeIn");
        }
        else
        {
            anim.Play("FadeOut");
        }
    }
}
