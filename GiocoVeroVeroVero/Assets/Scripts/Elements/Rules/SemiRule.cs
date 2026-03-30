using UnityEngine;

public class SemiRule : Rule
{

    [SerializeField] private GameElement gameElement;
    [SerializeField] private GameObject gameObject;
    [SerializeField] private GameObject lombrichi;
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
        GameManager.Instance.win= true;
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
        if(gameElement.level == 100)
        {
            elementAnimation.PlayAnimation("Semi-Secondo3");
        }
        else
        {
            if(lombrichi.activeSelf)
            {
                elementAnimation.PlayAnimation("Semi-Secondo4");
                GameManager.Instance.ModifyTargetLevel2("Semi",50);
                GameManager.Instance.ModifyTargetLevel2("Buco",50);
                GameManager.Instance.RemoveElementFromHistory("Lombrichi");
            }
            else
            {
                elementAnimation.PlayAnimation("Semi-Secondo2");
            }
            
        }
        
    }

    public void SemiSparsiPrimo()
    {
        if (gameObject.activeSelf)
        {
            elementAnimation.PlayAnimation("Semi-Sparsi2");
        }
        else
        {
           
        }
    }

    public void SemiSparsiSecondo()
    {
        if (gameObject.activeSelf)
        {
            elementAnimation.PlayAnimation("Semi-Sparsi3");
        }
        else
        {
            elementAnimation.PlayAnimation("Semi-Sparsi-2");
        }
    }

    public void SemiSparsiTerzo()
    {
        if (gameObject.activeSelf)
        {
            elementAnimation.PlayAnimation("Semi-Sparsi4");
        }
        else
        {
            elementAnimation.PlayAnimation("Semi-Sparsi-3");
        }
    }

    public void SemiSparsiQuarto()
    {
        if (gameObject.activeSelf)
        {
            elementAnimation.PlayAnimation("Semi-Sparsi5");
        }
        else
        {
            elementAnimation.PlayAnimation("Semi-Sparsi-4");
        }
    }

    public void SemiSparsiUltimo()
    {
        if (gameObject.activeSelf)
        {
            elementAnimation.PlayAnimation("Semi-SparsiMale1");
        }
        else
        {
           elementAnimation.PlayAnimation("Semi-SparsiMale-1");
        }
    }

    public void SemiSparsiPenultimo()
    {
        if (gameObject.activeSelf)
        {
            elementAnimation.PlayAnimation("Semi-SparsiMale2");
        }
        else
        {
           elementAnimation.PlayAnimation("Semi-SparsiMale-2");
        }
    }

    public void SemiSparsiTerzultimo()
    {
        if (gameObject.activeSelf)
        {
            elementAnimation.PlayAnimation("Semi-SparsiMale3");
        }
        else
        {
           elementAnimation.PlayAnimation("Semi-SparsiMale-3");
        }
    }
    
}
