using UnityEngine;

public class AcquaRule : MonoBehaviour
{
    private ElementAnimation elementAnimation;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private Transform cameraTarget;

    private void Awake()
    {
        elementAnimation = GetComponent<ElementAnimation>();
    }
    public void Primo()
    {
        cameraMovement.MoveCamera();
        elementAnimation.PlayAnimation("Acqua-Primo",0);
    }

    public void Secondo()
    {
        cameraMovement.MoveCamera(cameraTarget, 0.5f, 0.5f);
        elementAnimation.PlayAnimation("Acqua-Secondo");
    }

    public void Terzo()
    {
        elementAnimation.PlayAnimation("Acqua-Terzo",2);
    }
}
