using UnityEngine;

public class FirstMiniGame2 : MonoBehaviour
{
    public static FirstMiniGame2 Instance;
    public bool canInput= false;
    public bool isAnimating;
    public bool early;
    public AudioClip beat;
    public AudioClip correct;
    public AudioClip wrong;
    public AudioClip miss;
    void OnEnable()
    {
        Instance = this;
    }

    void PlayBeatAudio()
    {
        SoundFXManager.Instance.PlayAudio(beat);
    }

    public void PlayCorrectAudio()
    {
        SoundFXManager.Instance.PlayAudio(correct);
    }
    public void PlayWrongAudio()
    {
        SoundFXManager.Instance.PlayAudio(wrong);
    }

    public void PlayMissAudio()
    {
        SoundFXManager.Instance.PlayAudio(miss);
    }

    void Miss()
    {
        if(FirstMiniGame.Instance.pressed)
        {
            FirstMiniGame.Instance.pressed = false;
            return;
        }
        FirstMiniGame.Instance.Miss();
    }

    public void SetCanInputFalse() 
    {
        canInput = false;
    } 

    public void SetCanInputTrue() 
    {
        canInput = true;
    } 


}
