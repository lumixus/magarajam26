using UnityEngine;

public class Phone : MonoBehaviour, IInteractable
{
    public bool isRinging = false;
    public AudioSource audioSource;

    public AudioClip ringingAudio;
    public AudioClip closePhoneAudio;

    public AudioClip[] answerAudioClips;

    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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

        audioSource.clip = ringingAudio;

        audioSource.Play();
    }

    public void AnswerPhone()
    {
        isRinging = false;
        anim.SetBool("Ringing", isRinging);
        audioSource.Stop();
        audioSource.PlayOneShot(answerAudioClips[UnityEngine.Random.Range(0, answerAudioClips.Length)]);
        GameManager.instance.AnswerCall();
    }

    public void ClosePhone()
    {
        audioSource.PlayOneShot(closePhoneAudio);
    }
}
