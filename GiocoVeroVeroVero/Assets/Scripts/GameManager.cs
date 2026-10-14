using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    
    public GameElement[] allElements;
    public List<GameElement> History = new List<GameElement>();

    public bool isAnimating = false;

    //public float currentAnimationSeconds;

    public bool ruleExecuted, doNothing, transitioning;
    public bool wait;

    public enum DayState { Normal, Auto, Transitioning}
    private int currentDay = 0;
    public DayState dayState;
    public bool win= false;
    public GameObject winObject;
    public GameObject endObject;
    public GameObject rami;
    public GameObject face;


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
        currentDay = 0;
    }   

    private void StartDay()
    {
        if (currentDay > 8)
        {
            Debug.Log("Ciao");
            GameOver();
            return;
        }
        else if (currentDay != 6 && currentDay != 8)
        {
            PlayerTurn();
        }
        else
        {
            StartCoroutine(RunAutoDay());
        }
    }



    private void PlayerTurn()
    {
        dayState = DayState.Normal;

        
    }

    private IEnumerator RunAutoDay()
    {
        dayState = DayState.Auto;
        isAnimating = true;

        yield return new WaitForSeconds(autoTurnDelay);
        yield return StartCoroutine(UpdateRules());
        yield return new WaitUntil(() => !isAnimating);
        yield return StartCoroutine(TransitionToNextDay());
    }
    


    public IEnumerator TransitionToNextDay()
    {
        dayState = DayState.Transitioning;

        yield return StartCoroutine(DayUI.Instance.Advance(currentDay));
        yield return null;      


        currentDay++;
        Debug.Log(currentDay);
        StartDay();           
    }

    private void GameOver()
    {
        dayState = DayState.Transitioning;
        if(win)
        {
            winObject.SetActive(true);
            rami.SetActive(false);
            face.SetActive(true);
        }
        endObject.SetActive(true);
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

    public bool checkHistory2(string targetId)
    {
        for (int i = 0; i < History.Count; i++)
        {
            if (History[i].id == targetId )
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

    int lastLastIndx = 0;
    public IEnumerator UpdateRules()
    {
        if(History==null)yield break;
        int num1 = (lastLastIndx == History.Count)? History.Count:History.Count-1;
        for (int i = 0; i < num1; i++) //salata l'ultimo elemento aggiunto 
        {
            yield return StartCoroutine(History[i].ExecuteUpdateRule());
        }
        lastLastIndx = History.Count;
        Instance.isAnimating = false;
        if(dayState!=DayState.Auto)StartCoroutine(TransitionToNextDay());
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
    public void MoveElementToPosition(string elementId, int targetPosition)
    {
        if (targetPosition < 0 || targetPosition >= History.Count) return;

        int currentIndex = -1;
        for (int i = 0; i < History.Count; i++)
        {
            if (History[i].id == elementId)
            {
                currentIndex = i;
                break;
            }
        }

        if (currentIndex == -1) return;

        GameElement element = History[currentIndex];
        History.RemoveAt(currentIndex);
        History.Insert(targetPosition, element);
    }

    public void ModifyTargetLevel(string targetId, sbyte targetLevel, sbyte level)
    {
        for (int i = 0; i < History.Count; i++)
        {
            if (History[i].id == targetId && History[i].level == targetLevel)
            {
                History[i].level = level;
            }
        }
    }

    public void ModifyTargetLevel2(string targetId, sbyte level)
    {
        for (int i = 0; i < History.Count; i++)
        {
            if (History[i].id == targetId )
            {
                History[i].level = level;
            }
        }
    }

    public void RemoveElementFromHistory(string s)
    {
        for (int i = 0; i < History.Count; i++)
        {
            if (History[i].id == s)
            {
                History.RemoveAt(i);
                break;
            }
        }
    }

    
    


    /*public void RuleNotExecuted()
    {
        ruleExecuted = false;
    }*/
    [Header("Turn Settings")]
    [SerializeField] private float autoTurnDelay = 2f;    
    

    

}






