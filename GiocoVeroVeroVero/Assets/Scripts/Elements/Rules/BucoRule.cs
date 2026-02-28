using UnityEngine;

public class BucoRule : MonoBehaviour
{
    private ElementAnimation elementAnimation;

    private void Awake()
    {
        elementAnimation = GetComponent<ElementAnimation>();
    }
    public void Primo()
    {
        elementAnimation.PlayAnimation("Buco-Primo");
    }

    public void Secondo()
    {
        elementAnimation.PlayAnimation("Buco-Secondo");
    }

    public void Ultimo()
    {
        elementAnimation.PlayAnimation("Buco-Ultimo");
    }

    public void Penultimo()
    {
        elementAnimation.PlayAnimation("Buco-Penultimo");
    }

    
}
