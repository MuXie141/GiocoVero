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
            GameManager.Instance.ruleExecuted = true; //Invoke chiamerà una funzione per falsarlo
            //GameManager.Instance.doNothing = true; // Questo è per dire a TransitionEffect quando può cambiare il variabile wait
            //GameManager.Instance.wait = true;
            if (GameManager.Instance.IsElementActive(rule.targetId, rule.targetLevel))
            {
                rule.onPresent.Invoke();
            }
            else
            {
                rule.onAbsent.Invoke();
                
            }
            yield return null;
            if(GameManager.Instance.ruleExecuted==true)
            {
                //yield return new WaitUntil(() => GameManager.Instance.wait == false);
                //yield return new WaitForSeconds(GameManager.Instance.currentAnimationSeconds);
                //GameManager.Instance.doNothing = false;
                while(GameManager.Instance.transitioning || GameManager.Instance.wait)
                {
                    yield return new WaitForSeconds(1f);
                }
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