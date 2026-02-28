using UnityEngine;

public class PlayEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    public void Play()
    {
        particleSystem.Play();
    }
}
