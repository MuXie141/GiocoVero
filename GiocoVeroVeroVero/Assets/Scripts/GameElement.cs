using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public class GameElement : MonoBehaviour
{
    public string id; 
    public bool isActive = false;
    public sbyte level = 0;
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject button;

    public InteractionRule[] onActiveRules;
    public InteractionRule[] onUpdateRules;

    [Serializable]
    public class InteractionRule
    {
        public string targetId; 
        public sbyte targetLevel;
        public UnityEvent onPresent;    
        public UnityEvent onAbsent;     
    }

    
    public void Activate()
    {
        if (GameManager.Instance.isAnimating) return;
        isActive = true;
        sprite.SetActive(!sprite.activeSelf);
        ExecuteRule();
        button.SetActive(false);
        GameManager.Instance.RegisterElement(this);
        GameManager.Instance.UpdateRules();
    }

    private void ExecuteRule()
    {
        StartCoroutine(ExecuteRuleCoroutine());
    }

    private IEnumerator ExecuteRuleCoroutine()
    {
        yield return null;
        foreach (var rule in onActiveRules)
        {
           
            if (GameManager.Instance.IsElementActive(rule.targetId, rule.targetLevel))
            {
                rule.onPresent.Invoke();
                
            }
            else
            {
                rule.onAbsent.Invoke();
                
            }
            yield return new WaitForSeconds(3.0f);
            yield return new WaitForSeconds(GameManager.Instance.currentAnimationSeconds+2f);
        }
    }

    

    public void ExecuteUpdateRule()
    {
        foreach (var rule in onUpdateRules)
        {
           
            if (GameManager.Instance.IsElementActive(rule.targetId, rule.targetLevel))
            {
                rule.onPresent.Invoke();
            }
            else
            {
                rule.onAbsent.Invoke();
            }
        }
    }
}