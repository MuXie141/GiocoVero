using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public void PlayClip(AudioClip audioClip)
    {
        SoundFXManager.Instance.PlayAudio(audioClip);
    
    }
}
