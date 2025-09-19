using UnityEngine;

public class Phone : MonoBehaviour, IInteractable
{
    public bool isRinging = false;
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!isRinging)
        {
            return;
        }

        AnswerPhone();
    }

    public void RingPhone()
    {
        isRinging = true;
        anim.SetBool("Ringing", isRinging);
    }

    public void AnswerPhone()
    {
        isRinging = false;
        anim.SetBool("Ringing", isRinging);
        GameManager.instance.AnswerCall();
    }
}
