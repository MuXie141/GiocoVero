using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    
    [SerializeField] private GameElement[] allElements;
    public List<GameElement> History = new List<GameElement>();

    public bool isAnimating = false;

    //public float currentAnimationSeconds;

    public bool ruleExecuted, doNothing, transitioning;
    public bool wait;

    public enum DayState { Normal, Auto, Transitioning}
    private int currentDay = 0;
    public DayState dayState;


    [Header("Impostazione Scena")]
    public List<GameObject> ActiveOnStart;
    public List<GameObject> DisableOnStart;
    

    void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        SetUp();
        currentDay = 1;
        StartDay();
    }   

    private void StartDay()
    {
        if (currentDay != 5 && currentDay != 8)
        {
            PlayerTurn();
        }
        else
        {
            Auto();
        }
    }

    private void PlayerTurn()
    {
        dayState = DayState.Normal;
        
    }

    private void Auto()
    {
        dayState = DayState.Auto;

        StartCoroutine(Instance.UpdateRules());
    }

    public IEnumerator TransitionToNextDay()
    {
        dayState = DayState.Transitioning;
        yield return null;

        currentDay++;
    }


    //______________________________________________________________________________________________________________________________

    private void SetUp()
    {
        foreach (GameObject item in ActiveOnStart)
        {
            item.SetActive(true);
        }

        foreach (GameObject item in DisableOnStart)
        {
            item.SetActive(false);
        }
    }

    public bool checkHistory(string targetId, sbyte targetLevel)
    {
        for (int i = 0; i < History.Count; i++)
        {
            if (History[i].id == targetId && History[i].level == targetLevel)
            {
                return true;
            }
        }
        return false;
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

    public IEnumerator UpdateRules()
    {
        if(History==null)yield break;
        for (int i = 0; i < History.Count-1; i++) //slata l'ultimo elemento aggiunto 
        {
            yield return StartCoroutine(History[i].ExecuteUpdateRule());
        }
        Instance.isAnimating = false;
    }

    public void SwapElements(string element1, string element2)
    {
        int index1 = -1;
        int index2 = -1;

        for (int i = 0; i < History.Count; i++)
        {
            if (History[i].id == element1)
            {
                index1 = i;
            }

            if (History[i].id == element2)
            {
                index2 = i;
            }
        }

        
        if (index1 != -1 && index1 !=-1)
        {
            GameElement temp = History[index1];
            History[index1] = History[index2];
            History[index2] = temp;
        }
    }

    public void ModifyTargetLevel(string targetId, sbyte targetLevel, sbyte level)
    {
        for (int i = 0; i < History.Count; i++)
        {
            if (History[i].id == targetId && History[i].level == targetLevel)
            {
                History[i].level = level;
                Debug.Log(level+ " "+ History[i].level);
            }
        }
    }

    
    


    /*public void RuleNotExecuted()
    {
        ruleExecuted = false;
    }*/



    

}






