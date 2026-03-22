using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameElement : MonoBehaviour
{
    public string id; 
    public bool isActive = false;
    public sbyte level = 0;
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject button;

    public List<InteractionRule> onActiveRules;
    public List<InteractionRule> onUpdateRules;

    [Serializable]
    public class InteractionRule
    {
        public string targetId; 
        public sbyte targetLevel;
        public UnityEvent onPresent;    
        public UnityEvent onAbsent;    
        [Header("Present")]
        public bool modifyLevel;
        public Target[] target;
        [Header("Absent")]
        public bool modifyLvl;
        public Target[] trget;

    }
    [Serializable]
    public class Target
    {
        public string targetId; 
        public sbyte targetLevel;  
        public sbyte toLevel;  
    }

    

    public void Activate()
    {
        if (GameManager.Instance.isAnimating || GameManager.Instance.dayState == GameManager.DayState.Auto|| GameManager.Instance.dayState == GameManager.DayState.Transitioning) return;
        GameManager.Instance.isAnimating = true;
        isActive = true;
        SoundFXManager.Instance.Tap();
        sprite.SetActive(true);
        GameManager.Instance.RegisterElement(this);
        ExecuteRule();
        button.SetActive(false);
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
            if (GameManager.Instance.checkHistory(rule.targetId, rule.targetLevel))
            {
                if (rule.modifyLevel == true) Modify(rule.target);
                if (rule.onPresent.GetPersistentEventCount() != 0) rule.onPresent.Invoke(); //Debug.Log("onPresent");
                
            }
            else
            {
                if (rule.modifyLvl == true) Modify(rule.trget);
                if (rule.onAbsent.GetPersistentEventCount() != 0) rule.onAbsent.Invoke(); // Debug.Log("onAbsent");
                
            }
            yield return null;

            while(GameManager.Instance.transitioning || GameManager.Instance.wait)
            {
                yield return new WaitForSeconds(1f);
            }
            /*if(GameManager.Instance.ruleExecuted==true)
            {
                //yield return new WaitUntil(() => GameManager.Instance.wait == false);
                //yield return new WaitForSeconds(GameManager.Instance.currentAnimationSeconds);
                //GameManager.Instance.doNothing = false;
                while(GameManager.Instance.transitioning || GameManager.Instance.wait)
                {
                    yield return new WaitForSeconds(1f);
                }
            }*/
        }
        StartCoroutine(GameManager.Instance.UpdateRules());
    }

    

    public IEnumerator ExecuteUpdateRule()
    {

        for (int i = onUpdateRules.Count - 1; i >= 0; i--)
        {
            InteractionRule rule = onUpdateRules[i];
            if (GameManager.Instance.checkHistory(rule.targetId, rule.targetLevel))
            {
                if (rule.modifyLevel == true) Modify(rule.target);
                if (rule.onPresent.GetPersistentEventCount() != 0) {rule.onPresent.Invoke(); onUpdateRules.Remove(rule);}  // Debug.Log("onPresent");
            }
            else
            {
                if (rule.modifyLvl == true) Modify(rule.trget);
                if (rule.onAbsent.GetPersistentEventCount() != 0) {rule.onAbsent.Invoke(); onUpdateRules.Remove(rule);}  // Debug.Log("onAbsent");
            }

            yield return null;

            while(GameManager.Instance.transitioning || GameManager.Instance.wait)
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void Modify(Target[] target)
    {
        for (int i = 0; i < target.Length; i++)
        {
            GameManager.Instance.ModifyTargetLevel(target[i].targetId, target[i].targetLevel, target[i].toLevel);
        }
    }
}