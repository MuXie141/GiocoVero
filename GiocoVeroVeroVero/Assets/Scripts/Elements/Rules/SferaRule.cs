using UnityEngine;

public class SferaRule : MonoBehaviour
{
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject gameobject;
    [SerializeField] private ElementAnimation rifiutiAnimation;


    private ElementAnimation elementAnimation;
    

    private void Awake()
    {
        elementAnimation = GetComponent<ElementAnimation>();
    }

    public void Primo()
    {
        buttons.SetActive(true);
        gameobject.SetActive(true);
        elementAnimation.PlayAnimation("Sfera-Zero");
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
}
