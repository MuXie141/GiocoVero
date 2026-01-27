using UnityEngine;

public class LombrichiRule : MonoBehaviour
{
    private ElementAnimation elementAnimation;

    private void Awake()
    {
        elementAnimation = GetComponent<ElementAnimation>();
    }
    public void Primo()
    {
        elementAnimation.PlayAnimation("Lombrichi-Primo");
    }
}
