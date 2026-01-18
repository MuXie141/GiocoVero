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
}
