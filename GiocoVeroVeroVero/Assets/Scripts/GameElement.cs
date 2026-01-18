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
        GameManager.Instance.isAnimating = true;
        isActive = true;
        sprite.SetActive(!sprite.activeSelf);
        ExecuteRule();
        button.SetActive(false);
        GameManager.Instance.RegisterElement(this);
    }

    private void ExecuteRule()
    {
        StartCoroutine(ExecuteRuleCoroutine());
    }

    private IEnumerator ExecuteRuleCoroutine()
    {
        foreach (var rule in onActiveRules)
        {
            GameManager.Instance.ruleExecuted = false;
            if (GameManager.Instance.IsElementActive(rule.targetId, rule.targetLevel))
            {
                GameManager.Instance.ruleExecuted = true;
                rule.onPresent.Invoke();
            }
            else
            {
                GameManager.Instance.ruleExecuted = true;
                rule.onAbsent.Invoke();
                
            }
            yield return null;
            if(GameManager.Instance.ruleExecuted==true)
            {
                yield return new WaitForSeconds(GameManager.Instance.currentAnimationSeconds);
            }
        }

        GameManager.Instance.UpdateRules();
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
        GameManager.Instance.isAnimating = false;

    }
}