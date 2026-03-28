using UnityEngine;

public class RifiutiRule : MonoBehaviour
{
    private ElementAnimation elementAnimation;

    private void Awake()
    {
        elementAnimation = GetComponent<ElementAnimation>();
    }
    public void Primo()
    {
        if(GameManager.Instance.checkHistory("Semi",50))
        {
            Ultimo();
            return;
        } 
        elementAnimation.PlayAnimation("Rifiuti-Primo");
    }

    public void Ultimo()
    {
        elementAnimation.PlayAnimation("Rifiuti-Ultimo");
    }
}
