using UnityEngine;

public class LombrichiRule : Rule
{

    public void Primo()
    {
        
        elementAnimation.PlayAnimation("Lombrichi-Primo");
    }
    public void Move()
    {
        GameManager.Instance.MoveElementToPosition("Lombrichi", 0);
    }
    public void Penultimo()
    {
        
        elementAnimation.PlayAnimation("Lombrichi-Penultimo");
    }


}
