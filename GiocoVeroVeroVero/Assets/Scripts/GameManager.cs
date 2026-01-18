using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    
    [SerializeField] private GameElement[] allElements;
    public List<GameElement> History = new List<GameElement>();

    public bool isAnimating = false;

    public float currentAnimationSeconds;

    public bool ruleExecuted;
   

    void Awake()
    {
        Instance = this;
    }

    public bool IsElementActive(string targetId, sbyte targetLevel)
    {
        for (int i = 0; i < allElements.Length; i++)
        {
            if (allElements[i].id == targetId && allElements[i].level == targetLevel)
            {
                return allElements[i].isActive;
            }
        }
        return false;
    }

    public void RegisterElement(GameElement newElement)
    {
        History.Add(newElement);
    }

    public void UpdateRules()
    {
        if(History==null)return;
        for (int i = 0; i < History.Count; i++)
        {
            History[i].ExecuteUpdateRule();
        }
    }

    //Non so se è meglio fare così o controllare se onabsent o onpresent list.count != 0 
    public void RuleNotExecuted()
    {
        ruleExecuted = false;
    }

    

}






