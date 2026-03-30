using UnityEngine;
using System.Collections;

public class BucoRule : Rule
{
    public void Primo()
    {
        
        elementAnimation.PlayAnimation("Buco-Primo");
        if(GameManager.Instance.checkHistory("Rami",0)) StartCoroutine(DestroyRami());
    }

    public void Secondo()
    {
        elementAnimation.PlayAnimation("Buco-Secondo");
    }

    public void Ultimo()
    {
        elementAnimation.PlayAnimation("Buco-Ultimo");
        if(GameManager.Instance.checkHistory("Rami",0)) StartCoroutine(DestroyRami());
    }

    public void Penultimo()
    {
        elementAnimation.PlayAnimation("Buco-Penultimo");
    }

    public void Terzo()
    {
        if(GameManager.Instance.allElements[0].level == 50|| GameManager.Instance.allElements[0].level == 51)
        {
            return;
        } 
        if(GameManager.Instance.checkHistory("Semi",-128))return;
        elementAnimation.PlayAnimation("Buco-Terzo");
    }

    IEnumerator DestroyRami()
    {
        yield return new WaitForSeconds(1f);
        //PlayOtherRuleAnimation("Rami", "Rami-Ultimo");
        GameManager.Instance.allElements[4].gameObject.GetComponentInChildren<ElementAnimation>().PlayAnimation("Rami-Ultimo");
        GameManager.Instance.allElements[4].level = -128;
    }
}
