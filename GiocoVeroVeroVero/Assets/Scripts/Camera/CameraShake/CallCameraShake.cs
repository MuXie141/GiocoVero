using UnityEngine;

public class CallCameraShake : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    public void Shake()
    {
        StartCoroutine(cameraShake.Shake(duration,magnitude));
    }
    public void SmallShake()
    {
        StartCoroutine(cameraShake.Shake(duration/2,magnitude/2));
    }
}
