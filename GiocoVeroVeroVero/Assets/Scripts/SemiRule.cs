using UnityEngine;

public class SemiRule : MonoBehaviour
{
    private ElementAnimation elementAnimation;
     private void Awake()
    {
        elementAnimation = GetComponent<ElementAnimation>();
    }
    public void Primo()
    {
        elementAnimation.PlayAnimation("Semi-Primo");
    }

    public void First()
    {
        elementAnimation.PlayAnimation("Semi-First");
    }
}
