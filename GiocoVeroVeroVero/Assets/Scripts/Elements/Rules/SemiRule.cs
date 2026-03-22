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

    public void Secondo()
    {
        elementAnimation.PlayAnimation("Fiori-Primo");
    }

    public void Terzo()
    {
        elementAnimation.PlayAnimation("Fiori-Secondo");
    }

    public void Quarto()
    {
        elementAnimation.PlayAnimation("Fiori-Terzo");
    }

    public void First()
    {
        elementAnimation.PlayAnimation("Semi-First");
    }

    public void Uccello1()
    {
        elementAnimation.PlayAnimation("Semi-Secondo");
    }

    public void Uccello2()
    {
        elementAnimation.PlayAnimation("Semi-Secondo2");
    }
}
