using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip tap;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void Tap()
    {
        PlayAudio(tap);
    }

}
