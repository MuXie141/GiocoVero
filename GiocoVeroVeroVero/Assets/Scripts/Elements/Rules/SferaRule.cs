using UnityEngine;

public class SferaRule : Rule
{
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject gameobject;
    [SerializeField] private ElementAnimation rifiutiAnimation;



    public void Primo()
    {
        buttons.SetActive(true);
        gameobject.SetActive(true);
        elementAnimation.PlayAnimation("Sfera-Zero");
    }

    public void PrimoMezzo()
    {
        elementAnimation.PlayAnimation("Sfera-PrimoMezzo");
    }


    public void Secondo()
    {
        elementAnimation.PlayAnimation("Sfera-Secondo");
    }

    public void Terzo()
    {
        elementAnimation.PlayAnimation("Sfera-Terzo");
    }
    public void Swap()
    {
        GameManager.Instance.SwapElements("Buco","Sfera");
        Debug.Log("Swap");
    }

    public void Quarto()
    {
        elementAnimation.PlayAnimation("Sfera-Quarto");
    }

    public void Quinto()
    {
        elementAnimation.PlayAnimation("Sfera-Quinto");
    }

    public void Sesto()
    {
        elementAnimation.PlayAnimation("Sfera-Sesto");
    }

    public void Settimo()
    {
        elementAnimation.PlayAnimation("Sfera-Settimo");
    }

    public void Ottavo()
    {
        elementAnimation.PlayAnimation("Sfera-Ottavo");
    }
    //___________________________
    public void Ultimo()
    {
        elementAnimation.PlayAnimation("Sfera-Ultimo");
    }

    public void Penultimo()
    {
        elementAnimation.PlayAnimation("Sfera-Penultimo");
    }
    public void Terzultimo()
    {
        elementAnimation.PlayAnimation("Sfera-Terzultimo");
    }
    public void Quadrultimo()
    {
        elementAnimation.PlayAnimation("Sfera-Quadrultimo");
    }

    public void CheckSeedPresence()
    {
        if(GameManager.Instance.checkHistory("Semi", -128))
        {
            GameManager.Instance.ModifyTargetLevel2("Sfera", -10);
        }
    }


}
