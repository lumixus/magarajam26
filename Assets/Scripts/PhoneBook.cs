using System;
using UnityEngine;

public class PhoneBook : MonoBehaviour, IInteractable
{
    public Animator anim;
    public GameObject PhoneBookObject;
    public bool isOpened = false;

    public AudioSource audioSource;


    public AudioClip[] OpenSounds;
    public AudioClip[] CloseSounds;


    void Awake()
    {
        anim = PhoneBookObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        isOpened = !isOpened;
        if (isOpened)
        {
            anim.Play("FadeIn");
            audioSource.PlayOneShot(PickRandomSound(OpenSounds));
        }
        else
        {
            anim.Play("FadeOut");
            audioSource.PlayOneShot(PickRandomSound(CloseSounds));
        }
    }

    public AudioClip PickRandomSound(AudioClip[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
