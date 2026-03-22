using UnityEngine;

public class Rule : MonoBehaviour
{
    protected ElementAnimation elementAnimation;

    private void Awake()
    {
        elementAnimation = GetComponent<ElementAnimation>();
    }
    public void PlayAnimation(string animation)
    {
        if (elementAnimation == null)
        {
            elementAnimation = GetComponent<ElementAnimation>();
        }
        elementAnimation.PlayAnimation(animation);
    }

    /*public void PlayOtherRuleAnimation(string element,string animation)
    {
        for(int i=0; i<GameManager.Instance.History.Count; i++)
        {
            if(GameManager.Instance.History[i].id== element)
            {
                GameManager.Instance.History[i].gameObject.GetComponentInChildren<ElementAnimation>().PlayAnimation("animation");
            }
        }
    }*/
}
