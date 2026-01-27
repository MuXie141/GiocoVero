using UnityEngine;

public class RamiRule : MonoBehaviour
{
    private ElementAnimation elementAnimation;

    private void Awake()
    {
        elementAnimation = GetComponent<ElementAnimation>();
    }
    public void Primo()
    {
        elementAnimation.PlayAnimation("Rami-Primo");
    }
}
